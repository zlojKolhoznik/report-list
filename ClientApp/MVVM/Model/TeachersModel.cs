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
    internal abstract class TeachersModel
    {
        protected App app;
        protected TeacherDataView teacher;

        public TeachersModel()
        {
            app = (App)Application.Current;
            teacher = GetTeacher(app.User!.Id);
        }

        public TeacherDataView Teacher => teacher;

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
            return response.Subjects!.Where(s => teacher.SubjectsIds.Contains((int)s.Id!)).ToList();
        }

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
    }
}