using Networking.DataViews;
using Networking.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.MVVM.Model
{
    internal class StudentsLessonsModel : StudentsModel
    {
        public StudentsLessonsModel() : base()
        {

        }

        public async Task<List<string>> GetLessonsViewAsync(int? subjectId = null, DateTime? date = null)
        {
            if (subjectId == null && date == null)
            {
                throw new ArgumentNullException($"{nameof(subjectId)} {nameof(date)}", "To get lessons should be provided at least the subject or the date");
            }
            List<string> result = new List<string>();
            RequestOptions request;
            ResponseOptions response;
            byte[] bytes;
            string json;
            if (date != null && subjectId != null)
            {
                request = new RequestOptions() { RequestType = RequestType.GetLessons, GroupId = student.GroupId, LessonDate = date.Value.Ticks };
                json = JsonConvert.SerializeObject(request);
                bytes = Encoding.UTF8.GetBytes(json);
                bytes = await Task.Run(() => app.SendRequestAndReceiveResponse(bytes));
                json = Encoding.UTF8.GetString(bytes);
                response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                if (!response.Success)
                {
                    throw new Exception(response.ErrorMessage);
                }
                List<LessonDataView> lessonsForDate = response.Lessons!.ToList();
                request = new RequestOptions() { RequestType = RequestType.GetLessons, GroupId = student.GroupId, SubjectId = subjectId };
                json = JsonConvert.SerializeObject(request);
                bytes = Encoding.UTF8.GetBytes(json);
                bytes = await Task.Run(() => app.SendRequestAndReceiveResponse(bytes));
                json = Encoding.UTF8.GetString(bytes);
                response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                if (!response.Success)
                {
                    throw new Exception(response.ErrorMessage);
                }
                List<LessonDataView> lessonsForSubject = response.Lessons!.ToList();
                foreach (var lesson in lessonsForDate.IntersectBy(lessonsForSubject.Select(l=>l.Id), l=>l.Id))
                {
                    result.Add($"{new DateTime(lesson.Date):dd.MM HH:mm}. {lesson.Topic}");
                }
                return result;
            }
            else if (subjectId != null)
            {
                request = new RequestOptions() { RequestType = RequestType.GetLessons, GroupId = student.GroupId, SubjectId = subjectId };
                json = JsonConvert.SerializeObject(request);
                bytes = Encoding.UTF8.GetBytes(json);
                bytes = await Task.Run(() => app.SendRequestAndReceiveResponse(bytes));
                json = Encoding.UTF8.GetString(bytes);
                response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                if (!response.Success)
                {
                    throw new Exception(response.ErrorMessage);
                }
                foreach (var lesson in response.Lessons!)
                {
                    result.Add($"{new DateTime(lesson.Date):dd.MM HH:mm}. {lesson.Topic}");
                }
                return result;
            }
            else
            {
                request = new RequestOptions() { RequestType = RequestType.GetLessons, GroupId = student.GroupId, LessonDate = date!.Value.Ticks };
                json = JsonConvert.SerializeObject(request);
                bytes = Encoding.UTF8.GetBytes(json);
                bytes = await Task.Run(() => app.SendRequestAndReceiveResponse(bytes));
                json = Encoding.UTF8.GetString(bytes);
                response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
                if (!response.Success)
                {
                    throw new Exception(response.ErrorMessage);
                }
                foreach (var lesson in response.Lessons!)
                {
                    result.Add($"{new DateTime(lesson.Date):dd.MM HH:mm}. {lesson.Topic}");
                }
                return result;
            }
        }
    }
}
