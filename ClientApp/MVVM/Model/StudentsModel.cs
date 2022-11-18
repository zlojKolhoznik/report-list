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
    internal abstract class StudentsModel
    {
        protected App app;
        protected StudentDataView student;

        public StudentsModel()
        {
            app = (App)Application.Current;
            student = GetStudent(app.User!.Id);
        }

        public List<SubjectDataView> GetSubjects()
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetSubjects, GroupId = student.GroupId };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response.Subjects!.ToList();
        }

        private StudentDataView GetStudent(int userId)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetStudent, UserId = userId };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response.Student!;
        }
    }
}