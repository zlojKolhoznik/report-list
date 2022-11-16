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
    class TeachersLessonsModel
    {
        App app;
        TeacherDataView teacher;

        public TeachersLessonsModel()
        {
            app = (App)Application.Current;
            teacher = GetTeacher(app.User!.Id);
        }

        public TeacherDataView Teacher => teacher;

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
            return response.Subjects!.Where(s=>teacher.SubjectsIds.Contains((int)s.Id!)).ToList();
        }

        public List<string> GetLessons(int? subjectId = null, int? groupId = null, DateTime? date = null)
        {
            if (subjectId == null && groupId == null && date == null)
            {
                throw new ArgumentNullException($"{nameof(subjectId)} {nameof(groupId)} {nameof(date)}", "To get the lessons list should be provided at least the subject, the group or the date");
            }
            if (subjectId != null && groupId != null && date != null)
            {
                List<LessonDataView> lessonsSubject = GetLessonsBySubject((int)subjectId);
                List<LessonDataView> lessonsGroup = GetLessonsByGroup((int)groupId);
                List<LessonDataView> lessonsDate = GetLessonsByDate((DateTime)date);
                List<LessonDataView> temp = lessonsSubject.IntersectBy(lessonsGroup.Select(l => l.Id), l => l.Id).ToList();
                List<string> result = new List<string>();
                foreach (var lesson in temp.IntersectBy(lessonsDate.Select(l => l.Id), l => l.Id))
                {
                    result.Add($"{new DateTime(lesson.Date).ToString("dd.MM HH:mm")}. {lesson.Topic}");
                }
                return result;
            }
            if (subjectId != null && groupId != null)
            {
                List<LessonDataView> lessonsSubject = GetLessonsBySubject((int)subjectId);
                List<LessonDataView> lessonsGroup = GetLessonsByGroup((int)groupId);
                List<string> result = new List<string>();
                foreach (var lesson in lessonsSubject.IntersectBy(lessonsGroup.Select(l => l.Id), l => l.Id))
                {
                    result.Add($"{new DateTime(lesson.Date).ToString("dd.MM HH:mm")}. {lesson.Topic}");
                }
                return result;
            }
            if (subjectId != null && date != null)
            {
                List<LessonDataView> lessonsSubject = GetLessonsBySubject((int)subjectId);
                List<LessonDataView> lessonsDate = GetLessonsByDate((DateTime)date);
                List<string> result = new List<string>();
                foreach (var lesson in lessonsSubject.IntersectBy(lessonsDate.Select(l => l.Id), l => l.Id))
                {
                    result.Add($"{new DateTime(lesson.Date).ToString("dd.MM HH:mm")}. {lesson.Topic}");
                }
                return result;
            }
            if (groupId != null && date != null)
            {
                List<LessonDataView> lessonsGroup = GetLessonsByGroup((int)groupId);
                List<LessonDataView> lessonsDate = GetLessonsByDate((DateTime)date);
                List<string> result = new List<string>();
                foreach (var lesson in lessonsDate.IntersectBy(lessonsGroup.Select(l => l.Id), l => l.Id))
                {
                    result.Add($"{new DateTime(lesson.Date).ToString("dd.MM HH:mm")}. {lesson.Topic}");
                }
                return result;
            }
            if (subjectId != null)
            {
                List<string> result = new List<string>();
                foreach (var lesson in GetLessonsBySubject((int)subjectId))
                {
                    result.Add($"{new DateTime(lesson.Date).ToString("dd.MM HH:mm")}. {lesson.Topic}");
                }
                return result;
            }
            if (groupId != null)
            {
                List<string> result = new List<string>();
                foreach (var lesson in GetLessonsByGroup((int)groupId))
                {
                    result.Add($"{new DateTime(lesson.Date).ToString("dd.MM HH:mm")}. {lesson.Topic}");
                }
                return result;
            }
            if (date != null)
            {
                List<string> result = new List<string>();
                foreach (var lesson in GetLessonsByDate((DateTime)date))
                {
                    result.Add($"{new DateTime(lesson.Date).ToString("dd.MM HH:mm")}. {lesson.Topic}");
                }
                return result;
            }
            throw new Exception();
        }

        private List<LessonDataView> GetLessonsBySubject(int subjectId)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetLessons, SubjectId = subjectId, TeacherId = teacher.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response.Lessons!;
        }

        private List<LessonDataView> GetLessonsByDate(DateTime date)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetLessons, LessonDate = date.Ticks, TeacherId = teacher.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
            json = Encoding.UTF8.GetString(bytes);
            ResponseOptions response = JsonConvert.DeserializeObject<ResponseOptions>(json)!;
            if (!response.Success)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response.Lessons!;
        }

        private List<LessonDataView> GetLessonsByGroup(int groupId)
        {
            RequestOptions request = new RequestOptions() { RequestType = RequestType.GetLessons, GroupId = groupId, TeacherId = teacher.Id };
            string json = JsonConvert.SerializeObject(request);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            bytes = app.SendRequestAndReceiveResponse(bytes);
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
