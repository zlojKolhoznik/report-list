using Networking.DataViews;
using Networking.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System;
using System.Windows;
using System.Linq;

namespace ClientApp.MVVM.Model
{
    internal class TeachersHomeworksModel
    {
        private App app;
        private TeacherDataView teacher;

        public TeachersHomeworksModel()
        {
            app = (App)Application.Current;
            teacher = GetTeacher(app.User!.Id);
        }

        public TeacherDataView Teacher => teacher;

        private TeacherDataView GetTeacher(int userId)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetTeacher, UserId = userId };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response.Teacher!;
        }

        public List<GroupDataView> GetGroups()
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetGroups, TeacherId = teacher.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            return response.Groups!;
        }

        public List<SubjectDataView> GetSubjects(GroupDataView group)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetSubjects, GroupId = group.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            return response.Subjects!.Where(s => teacher.SubjectsIds.Contains((int)s.Id!)).ToList();
        }

        public List<HomeworkDataView> GetHomeworks(int? subjectId, int? groupId, DateTime? date)
        {
            List<HomeworkDataView> homeworks = new List<HomeworkDataView>();
            if (subjectId == null && date == null && groupId == null)
            {
                List<GroupDataView> groups = GetGroups();
                List<SubjectDataView> subjects = new List<SubjectDataView>();
                foreach (var group in groups)
                {
                    var temp = GetSubjects(group);
                    subjects.AddRange(temp.ExceptBy(subjects.Select(s => s.Id), s => s.Id));
                }
                foreach (var subject in subjects)
                {
                    RequestOptions request = new RequestOptions() { RequestType = RequestType.GetHomeworks, TeacherId = teacher.Id, SubjectId = subject.Id };
                    string json = JsonConvert.SerializeObject(request);
                    byte[] bytes = Encoding.UTF8.GetBytes(json);
                    bytes = app.SendRequestAndReceiveResponse(bytes);
                    json = Encoding.UTF8.GetString(bytes);
                    ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                    if (!response.Success)
                    {
                        throw new Exception(response.ErrorMessage);
                    }
                    homeworks.AddRange(response.Homeworks!.ExceptBy(homeworks.Select(hw => hw.Id), hw => hw.Id));
                }
            }
            else if (subjectId == null && date == null)
            {
                List<HomeworkDataView> allHomeworks = GetHomeworks(null, null, null);
                homeworks.AddRange(allHomeworks.Where(hw => hw.GroupId == groupId!));
            }
            else if (date == null && groupId == null)
            {
                List<HomeworkDataView> allHomeworks = GetHomeworks(null, null, null);
                homeworks.AddRange(allHomeworks.Where(hw => hw.SubjectId == subjectId!));
            }
            else if (groupId == null && subjectId == null)
            {
                List<HomeworkDataView> allHomeworks = GetHomeworks(null, null, null);
                homeworks.AddRange(allHomeworks.Where(hw => new DateTime(hw.DueDate).Date == date!.Value.Date));
            }
            else if (groupId == null)
            {
                List<HomeworkDataView> homeworksSubject = GetHomeworks(subjectId, null, null);
                homeworks.AddRange(homeworksSubject.Where(hw => new DateTime(hw.DueDate).Date == date!.Value.Date));
            }
            else if (subjectId == null)
            {
                List<HomeworkDataView> homeworksGroup = GetHomeworks(null, groupId, null);
                homeworks.AddRange(homeworksGroup.Where(hw => new DateTime(hw.DueDate).Date == date!.Value.Date));
            }
            else if (date == null)
            {
                List<HomeworkDataView> homeworksSubject = GetHomeworks(subjectId, null, null);
                List<HomeworkDataView> homeworksGroup = GetHomeworks(null, groupId, null);
                homeworks.AddRange(homeworksSubject.IntersectBy(homeworksGroup.Select(hw => hw.Id), hw => hw.Id));
            }
            else
            {
                List<HomeworkDataView> allHomeworks = GetHomeworks(null, null, null);
                homeworks.AddRange(allHomeworks.Where(hw => hw.GroupId == groupId! && hw.SubjectId == subjectId! && new DateTime(hw.DueDate).Date == date.Value.Date));
            }
            return homeworks;
        }

        public Dictionary<int, string> GetHomeworksView(List<HomeworkDataView> homeworks)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            foreach (var homework in homeworks)
            {
                result.Add(homework.Id, $"Завдання з {homework.Subject} до {new DateTime(homework.DueDate):dd.MM HH:mm}");
            }
            return result;
        }

        public (byte[], string) DownloadHomeworkFileBytes(int homeworkId)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetHomeworkFile, HomeworkId = homeworkId };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            return app.DownloadFile(bytes);
        }

        public void AddHomework(int subjectId, int groupId, DateTime dueDate, byte[] fileBytes, string ext)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.AddHomework, SubjectId = subjectId, GroupId = groupId, HomeworkDueDate = dueDate.Ticks, TeacherId = teacher.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(json);
            byte[] responseBytes;
            try
            {
                responseBytes = app.AddHomework(requestBytes, fileBytes, ext);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            json = Encoding.UTF8.GetString(responseBytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
    }
}