using DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    internal class LessonsManager : DatabaseAccessManager
    {
        private static object locker = new object();

        public LessonsManager(string connStr) : base(connStr)
        {

        }

        public List<Lesson> GetLessons(Group group, Subject subject)
        {
            List<Lesson> result;
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Lessons.Where(l => l.Subject.Id == subject.Id && l.Group.Id == group.Id).ToList();
            }
            return result;
        }

        public List<Lesson> GetLessons(Group group, DateTime date)
        {
            List<Lesson> result;
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Lessons.Where(l => l.Date.Date == date.Date && l.Group.Id == group.Id).ToList();
            }
            return result;
        }

        public List<Lesson> GetLessons(Teacher teacher, Subject subject)
        {
            List<Lesson> result;
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Lessons.Where(l => l.Subject.Id == subject.Id && l.Teacher.Id == teacher.Id).ToList();
            }
            return result;
        }

        public List<Lesson> GetLessons(Teacher teacher, DateTime date)
        {
            List<Lesson> result;
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Lessons.Where(l => l.Date.Date == date.Date && l.Teacher.Id == teacher.Id).ToList();
            }
            return result;
        }

        public List<Lesson> GetLessons(Teacher teacher, Group group)
        {
            List<Lesson> result;
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Lessons.Where(l => l.Group.Id == group.Id && l.Teacher.Id == teacher.Id).ToList();
            }
            return result;
        }

        // TODO: Implement AddLesson, RemoveLesson and ChangeLessonInfo

        private void AddLesson(Lesson lesson)
        {

        }

        private void ChangeLessonInfo(Lesson lesson, string? newTopic = null, DateTime? newDate = null, Subject? newSubject = null, Teacher? newTeacher = null)
        {

        }

        private void RemoveLesson(Lesson lesson)
        {

        }
    }
}
