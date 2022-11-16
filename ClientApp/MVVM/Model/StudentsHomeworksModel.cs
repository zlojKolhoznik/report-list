using Networking.DataViews;
using Networking.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientApp.MVVM.Model
{
    class StudentsHomeworksModel : StudentsModel
    {
        public StudentsHomeworksModel() : base()
        {
            
        }

        public List<HomeworkDataView> GetHomeworks(int? subjectId, DateTime? date)
        {
            List<HomeworkDataView> homeworks = new List<HomeworkDataView>();
            if (subjectId == null && date == null)
            {
                List<SubjectDataView> subjects = GetSubjects();
                foreach (var subject in subjects)
                {
                    RequestOptions request = new RequestOptions() { RequestType = RequestType.GetHomeworks, GroupId = student.GroupId, SubjectId = subject.Id };
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
            else if (date == null)
            {
                RequestOptions request = new RequestOptions() { RequestType = RequestType.GetHomeworks, GroupId = student.GroupId, SubjectId = subjectId };
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
            else if (subjectId == null)
            {
                RequestOptions request = new RequestOptions() { RequestType = RequestType.GetHomeworks, GroupId = student.GroupId, HomeworkDueDate = date.Value.Ticks };
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
            else
            {
                RequestOptions request = new RequestOptions() { RequestType = RequestType.GetHomeworks, GroupId = student.GroupId, SubjectId = subjectId };
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
                request = new RequestOptions() { RequestType = RequestType.GetHomeworks, GroupId = student.GroupId, HomeworkDueDate = date.Value.Ticks };
                json = JsonConvert.SerializeObject(request);
                bytes = Encoding.UTF8.GetBytes(json);
                bytes = app.SendRequestAndReceiveResponse(bytes);
                json = Encoding.UTF8.GetString(bytes);
                response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                if (!response.Success)
                {
                    throw new Exception(response.ErrorMessage);
                }
                homeworks = homeworks.IntersectBy(response.Homeworks!.Select(hw=>hw.Id), hw=>hw.Id).ToList();
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
    }
}
