using Networking.DataViews;
using Networking.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return response.Subjects!;
        }

        public List<string> GetMarksViews(int groupId, int? subjectId)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetMarks, TeacherId = teacher.Id, GroupId = groupId, SubjectId = subjectId };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
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
