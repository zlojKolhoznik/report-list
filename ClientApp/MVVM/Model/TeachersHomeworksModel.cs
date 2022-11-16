using Networking.DataViews;
using Networking.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.MVVM.Model
{
    internal class TeachersHomeworksModel : TeachersModel
    {
        public TeachersHomeworksModel() : base()
        {
            
        }

        public async Task<List<HomeworkDataView>> GetHomeworksAsync(int? subjectId, int? groupId, DateTime? date)
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
                    bytes = await Task.Run(() => app.SendRequestAndReceiveResponse(bytes));
                    json = Encoding.UTF8.GetString(bytes);
                    ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                    if (!response.Success)
                    {
                        throw new Exception(response.ErrorMessage);
                    }
                    if (!response.Success)
                    {
                        throw new Exception(response.ErrorMessage);
                    }
                    homeworks.AddRange(response.Homeworks!.ExceptBy(homeworks.Select(hw => hw.Id), hw => hw.Id));
                }
            }
            else if (subjectId == null && date == null)
            {
                List<HomeworkDataView> allHomeworks = await GetHomeworksAsync(null, null, null);
                homeworks.AddRange(allHomeworks.Where(hw => hw.GroupId == groupId!));
            }
            else if (date == null && groupId == null)
            {
                List<HomeworkDataView> allHomeworks = await GetHomeworksAsync(null, null, null);
                homeworks.AddRange(allHomeworks.Where(hw => hw.SubjectId == subjectId!));
            }
            else if (groupId == null && subjectId == null)
            {
                List<HomeworkDataView> allHomeworks = await GetHomeworksAsync(null, null, null);
                homeworks.AddRange(allHomeworks.Where(hw => new DateTime(hw.DueDate).Date == date!.Value.Date));
            }
            else if (groupId == null)
            {
                List<HomeworkDataView> homeworksSubject = await GetHomeworksAsync(subjectId, null, null);
                homeworks.AddRange(homeworksSubject.Where(hw => new DateTime(hw.DueDate).Date == date!.Value.Date));
            }
            else if (subjectId == null)
            {
                List<HomeworkDataView> homeworksGroup = await GetHomeworksAsync(null, groupId, null);
                homeworks.AddRange(homeworksGroup.Where(hw => new DateTime(hw.DueDate).Date == date!.Value.Date));
            }
            else if (date == null)
            {
                List<HomeworkDataView> homeworksSubject = await GetHomeworksAsync(subjectId, null, null);
                List<HomeworkDataView> homeworksGroup = await GetHomeworksAsync(null, groupId, null);
                homeworks.AddRange(homeworksSubject.IntersectBy(homeworksGroup.Select(hw => hw.Id), hw => hw.Id));
            }
            else
            {
                List<HomeworkDataView> allHomeworks = await GetHomeworksAsync(null, null, null);
                homeworks.AddRange(allHomeworks.Where(hw => hw.GroupId == groupId! && hw.SubjectId == subjectId! && new DateTime(hw.DueDate).Date == date.Value.Date));
            }
            return homeworks;
        }

        public async Task<Dictionary<int, string>> GetHomeworksViewAsync(List<HomeworkDataView> homeworks)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            List<GroupDataView> groups = await Task.Run(() => GetGroups());
            foreach (var homework in homeworks)
            {
                result.Add(homework.Id, $"Завдання для {groups.First(g => g.Id == homework.Id).Name} з {homework.Subject} до {new DateTime(homework.DueDate):dd.MM HH:mm}");
            }
            return result;
        }

        public async Task<(byte[], string)> DownloadHomeworkFileBytesAsync(int homeworkId)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetHomeworkFile, HomeworkId = homeworkId };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            return await Task.Run(() => app.DownloadFile(bytes));
        }

        public async Task AddHomeworkAsync(int subjectId, int groupId, DateTime dueDate, byte[] fileBytes, string ext)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.AddHomework, SubjectId = subjectId, GroupId = groupId, HomeworkDueDate = dueDate.Ticks, TeacherId = teacher.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(json);
            byte[] responseBytes;
            try
            {
                responseBytes = await Task.Run(() => app.AddHomework(requestBytes, fileBytes, ext));
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