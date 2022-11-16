using Networking.DataViews;
using Networking.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ClientApp.MVVM.Model
{
    internal class TeachersMarksModel
    {
        private TeacherDataView teacher;
        private App app;

        public TeachersMarksModel()
        {
            app = (App)Application.Current;
            teacher = GetTeacher(app.User!.Id);
        }

        public TeacherDataView Teacher => teacher;

        private TeacherDataView GetTeacher(int userId)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetTeacher, UserId = userId};
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
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
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
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response.Subjects!.Where(s=>teacher.SubjectsIds.Contains((int)s.Id!)).ToList();
        }

        public List<StudentDataView> GetStudents()
        {
            List<GroupDataView> groups = GetGroups();
            List<StudentDataView> students = new List<StudentDataView>();
            foreach (var group in groups)
            {
                RequestOptions request = new RequestOptions() { RequestType = RequestType.GetStudents, GroupId = group.Id };
                string json = JsonConvert.SerializeObject(request);
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                bytes = app.SendRequestAndReceiveResponse(bytes);
                json = Encoding.UTF8.GetString(bytes);
                ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                if (!response.Success)
                {
                    throw new Exception(response.ErrorMessage);
                }
                students.AddRange(response.Students!);
            }
            return students;
        }

        public List<LessonDataView> GetLessons()
        {
            List<GroupDataView> groups = GetGroups();
            List<LessonDataView> result = new List<LessonDataView>();
            foreach (var group in groups)
            {
                RequestOptions request = new RequestOptions() { RequestType = RequestType.GetLessons, GroupId = group.Id, TeacherId = teacher.Id };
                string json = JsonConvert.SerializeObject(request);
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                bytes = app.SendRequestAndReceiveResponse(bytes);
                json = Encoding.UTF8.GetString(bytes);
                ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                if (!response.Success)
                {
                    throw new Exception(response.ErrorMessage);
                }
                result.AddRange(response.Lessons!.ExceptBy(result.Select(l => l.Id), l => l.Id));
            }
            return result;
        }

        public List<HomeworkDataView> GetHomeworks()
        {
            List<GroupDataView> groups = GetGroups();
            List<HomeworkDataView> result = new List<HomeworkDataView>();
            foreach (var group in groups)
            {
                RequestOptions request = new RequestOptions() { RequestType = RequestType.GetHomeworks, GroupId = group.Id, TeacherId = teacher.Id };
                string json = JsonConvert.SerializeObject(request);
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                bytes = app.SendRequestAndReceiveResponse(bytes);
                json = Encoding.UTF8.GetString(bytes);
                ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                if (!response.Success)
                {
                    throw new Exception(response.ErrorMessage);
                }
                result.AddRange(response.Homeworks!.ExceptBy(result.Select(l => l.Id), l => l.Id));
            }
            return result;
        }

        public void AddMark(int markValue, int studentId, int? homeworkId, int? lessonId)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.AddMark, StudId = studentId, HomeworkId = homeworkId, LessonId = lessonId, MarkValue = markValue, TeacherId = teacher.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public List<string> GetMarksViews(int groupId, int? subjectId)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetMarks, TeacherId = teacher.Id, GroupId = groupId, SubjectId = subjectId };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
            var result = new List<string>();
            foreach (var mark in response.Marks!)
            {
                if (mark.Lesson != null)
                {
                    result.Add($"Lesson {mark.Lesson.Subject} {new DateTime(mark.Lesson.Date).ToShortDateString()} {mark.Student!.Name} {mark.Student!.Surname} - {mark.Value}");
                    continue;
                }
                if (mark.Homework != null)
                {
                    result.Add($"Homework {mark.Homework.Subject} {new DateTime(mark.Homework.DueDate).ToShortDateString()} {mark.Student!.Name} {mark.Student!.Surname} - {mark.Value}");
                    continue;
                }
            }
            return result;
        }
    }
}
