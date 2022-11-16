using Networking.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientApp.MVVM.Model
{
    internal class StudentsMarksModel : StudentsModel
    {
        public StudentsMarksModel() : base()
        {

        }

        public List<string> GetMarksViews()
        {
            var result = new List<string>();
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetMarks, StudId = student.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
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
