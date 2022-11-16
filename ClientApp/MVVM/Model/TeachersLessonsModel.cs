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
    class TeachersLessonsModel : TeachersModel
    {
        public TeachersLessonsModel() : base()
        {
            
        }

        public async Task<List<string>> GetLessonsAsync(int? subjectId = null, int? groupId = null, DateTime? date = null)
        {
            if (subjectId == null && groupId == null && date == null)
            {
                throw new ArgumentNullException($"{nameof(subjectId)} {nameof(groupId)} {nameof(date)}", "To get the lessons list should be provided at least the subject, the group or the date");
            }
            if (subjectId != null && groupId != null && date != null)
            {
                List<LessonDataView> lessonsSubject = await GetLessonsBySubjectAsync((int)subjectId);
                List<LessonDataView> lessonsGroup = await GetLessonsByGroupAsync((int)groupId);
                List<LessonDataView> lessonsDate = await GetLessonsByDateAsync((DateTime)date);
                List<LessonDataView> temp = lessonsSubject.IntersectBy(lessonsGroup.Select(l => l.Id), l => l.Id).ToList();
                List<string> result = new List<string>();
                foreach (var lesson in temp.IntersectBy(lessonsDate.Select(l => l.Id), l => l.Id))
                {
                    result.Add($"{new DateTime(lesson.Date):dd.MM HH:mm}. {lesson.Topic}");
                }
                return result;
            }
            if (subjectId != null && groupId != null)
            {
                List<LessonDataView> lessonsSubject = await GetLessonsBySubjectAsync((int)subjectId);
                List<LessonDataView> lessonsGroup = await GetLessonsByGroupAsync((int)groupId);
                List<string> result = new List<string>();
                foreach (var lesson in lessonsSubject.IntersectBy(lessonsGroup.Select(l => l.Id), l => l.Id))
                {
                    result.Add($"{new DateTime(lesson.Date):dd.MM HH:mm}. {lesson.Topic}");
                }
                return result;
            }
            if (subjectId != null && date != null)
            {
                List<LessonDataView> lessonsSubject = await GetLessonsBySubjectAsync((int)subjectId);
                List<LessonDataView> lessonsDate = await GetLessonsByDateAsync((DateTime)date);
                List<string> result = new List<string>();
                foreach (var lesson in lessonsSubject.IntersectBy(lessonsDate.Select(l => l.Id), l => l.Id))
                {
                    result.Add($"{new DateTime(lesson.Date):dd.MM HH:mm}. {lesson.Topic}");
                }
                return result;
            }
            if (groupId != null && date != null)
            {
                List<LessonDataView> lessonsGroup = await GetLessonsByGroupAsync((int)groupId);
                List<LessonDataView> lessonsDate = await GetLessonsByDateAsync((DateTime)date);
                List<string> result = new List<string>();
                foreach (var lesson in lessonsDate.IntersectBy(lessonsGroup.Select(l => l.Id), l => l.Id))
                {
                    result.Add($"{new DateTime(lesson.Date):dd.MM HH:mm}. {lesson.Topic}");
                }
                return result;
            }
            if (subjectId != null)
            {
                List<string> result = new List<string>();
                foreach (var lesson in await GetLessonsBySubjectAsync((int)subjectId))
                {
                    result.Add($"{new DateTime(lesson.Date):dd.MM HH:mm}. {lesson.Topic}");
                }
                return result;
            }
            if (groupId != null)
            {
                List<string> result = new List<string>();
                foreach (var lesson in await GetLessonsByGroupAsync((int)groupId))
                {
                    result.Add($"{new DateTime(lesson.Date):dd.MM HH:mm}. {lesson.Topic}");
                }
                return result;
            }
            if (date != null)
            {
                List<string> result = new List<string>();
                foreach (var lesson in await GetLessonsByDateAsync((DateTime)date))
                {
                    result.Add($"{new DateTime(lesson.Date):dd.MM HH:mm}. {lesson.Topic}");
                }
                return result;
            }
            throw new Exception();
        }

        private async Task<List<LessonDataView>> GetLessonsBySubjectAsync(int subjectId)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetLessons, SubjectId = subjectId, TeacherId = teacher.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = await Task.Run(() => app.SendRequestAndReceiveResponse(bytes));
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response.Lessons!;
        }

        private async Task<List<LessonDataView>> GetLessonsByDateAsync(DateTime date)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetLessons, LessonDate = date.Ticks, TeacherId = teacher.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = await Task.Run(() => app.SendRequestAndReceiveResponse(bytes));
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response.Lessons!;
        }

        private async Task<List<LessonDataView>> GetLessonsByGroupAsync(int groupId)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetLessons, GroupId = groupId, TeacherId = teacher.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = await Task.Run(() => app.SendRequestAndReceiveResponse(bytes));
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response.Lessons!;
        }
    }
}
