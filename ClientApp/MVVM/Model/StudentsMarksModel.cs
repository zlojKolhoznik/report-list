﻿using Networking.DataViews;
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
    internal class StudentsMarksModel
    {
        private StudentDataView student;
        private App app;

        public StudentsMarksModel()
        {
            app = (App)Application.Current;
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetStudent, UserId = app.User!.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            student = response.Student!;
        }

        internal List<string> GetMarksViews()
        {
            var result = new List<string>();
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetMarks, StudId = student.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            foreach (var mark in response.Marks!)
            {
                if (mark.Lesson != null)
                {
                    result.Add($"{mark.Homework!.Subject} Lesson {new DateTime(mark.Lesson.Date).ToShortDateString()} - {mark.Value}");
                }
                else
                {
                    result.Add($"{mark.Homework!.Subject} Homework {new DateTime(mark.Homework!.DueDate).ToShortDateString()} - {mark.Value}");
                }
            }
            return result;
        }
    }
}