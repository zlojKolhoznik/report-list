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

        public void AddLesson(Lesson lesson)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    context.Lessons.Add(lesson);
                    context.SaveChanges();
                }
            }
        }

        public void ChangeLessonInfo(Lesson lesson, string? newTopic = null, DateTime? newDate = null, Subject? newSubject = null, Teacher? newTeacher = null)
        {
            if (lesson.Topic == newTopic)
            {
                throw new InvalidOperationException("Cannot change topic to its current value");
            }
            if (lesson.Date == newDate)
            {
                throw new InvalidOperationException("Cannot change date to its current value");
            }
            if (lesson.Subject == newSubject)
            {
                throw new InvalidOperationException("Cannot change subject to its current value");
            }
            if (lesson.Teacher == newTeacher)
            {
                throw new InvalidOperationException("Cannot change teacher to its current value");
            }
            lock (locker)
            {
                lesson.Topic = newTopic == null ? lesson.Topic : newTopic;
                lesson.Date = newDate == null ? lesson.Date : (DateTime)newDate;
                lesson.Subject = newSubject == null ? lesson.Subject : newSubject;
                lesson.Teacher = newTeacher == null ? lesson.Teacher : newTeacher;
            }
        }

        public void RemoveLesson(Lesson lesson)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    if (!context.Lessons.Any(l => l.Id == lesson.Id))
                    {
                        throw new InvalidOperationException("Cannot remove the lesson that is not in the database");
                    }
                    context.Lessons.Remove(lesson);
                    context.SaveChanges();
                }
            }
        }
    }
}
