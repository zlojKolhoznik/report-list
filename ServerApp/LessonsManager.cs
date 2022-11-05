using DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    /// <summary>
    /// Implements adding, getting and changing lessons
    /// </summary>
    internal class LessonsManager : DatabaseAccessManager
    {
        private static object locker = new object();

        public LessonsManager(string connStr) : base(connStr)
        {

        }

        /// <summary>
        /// Gets lessons of the specified group from the specified subject
        /// </summary>
        /// <param name="group">Group, whose lessons the method is to return</param>
        /// <param name="subject">Subject to search lessons from</param>
        /// <returns>The list of lessons of the specified group from the specified subject</returns>
        public List<Lesson> GetLessons(Group group, Subject subject)
        {
            List<Lesson> result;
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Lessons.Where(l => l.Subject.Id == subject.Id && l.Group.Id == group.Id).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gets lessons of the specified group on the specified date
        /// </summary>
        /// <param name="group">Group, whose lessons the method is to return</param>
        /// <param name="date">Date to search lessons on</param>
        /// <returns>The list of lessons of the specified group on the specified date</returns>
        public List<Lesson> GetLessons(Group group, DateTime date)
        {
            List<Lesson> result;
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Lessons.Where(l => l.Date.Date == date.Date && l.Group.Id == group.Id).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gets lessons of the specified teacher from the specified subject
        /// </summary>
        /// <param name="teacher">Teacher, whose lessons the method is to return</param>
        /// <param name="subject">Subject to search lessons from</param>
        /// <returns>The list of lessons of the specified teacher from the specified subject</returns>
        public List<Lesson> GetLessons(Teacher teacher, Subject subject)
        {
            List<Lesson> result;
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Lessons.Where(l => l.Subject.Id == subject.Id && l.Teacher.Id == teacher.Id).ToList();
            }
            return result;
        }


        /// <summary>
        /// Gets lessons of the specified teacher on the specified date
        /// </summary>
        /// <param name="teacher">Teacher, whose lessons the method is to return</param>
        /// <param name="date">Date to search lessons on</param>
        /// <returns>The list of lessons of the specified teacher on the specified date</returns>
        public List<Lesson> GetLessons(Teacher teacher, DateTime date)
        {
            List<Lesson> result;
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Lessons.Where(l => l.Date.Date == date.Date && l.Teacher.Id == teacher.Id).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gets lessons of the specified teacher in the specified group
        /// </summary>
        /// <param name="teacher">Teacher, whose lessons the method is to return</param>
        /// <param name="group">Group, whose lessons the method is to return</param>
        /// <returns>The list of lessons of the specified teacher from the specified subject</returns>
        public List<Lesson> GetLessons(Teacher teacher, Group group)
        {
            List<Lesson> result;
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Lessons.Where(l => l.Group.Id == group.Id && l.Teacher.Id == teacher.Id).ToList();
            }
            return result;
        }

        /// <summary>
        /// Adds a new lesson to the database
        /// </summary>
        /// <param name="lesson">Lesson to add</param>
        /// <exception cref="InvalidOperationException">Thrown when tried to add the new lesson for the group or teacher on the occupied time</exception>
        public void AddLesson(Lesson lesson)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    if (context.Lessons.Any(l => (l.Group.Id == lesson.Group.Id || l.Teacher.Id == lesson.Teacher.Id) && Math.Abs((l.Date - lesson.Date).Hours) < 1))
                    {
                        throw new InvalidOperationException("Cannot add a new lesson if there is a lesson in the database for the same group or the same teacher on the same time");
                    }
                    context.Lessons.Add(lesson);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Changes the information about the lesson in the database
        /// </summary>
        /// <param name="lesson">Lesson to change information for</param>
        /// <param name="newTopic">New topic of the lesson, ignored if null</param>
        /// <param name="newDate">New date of the lesson, ignored if null</param>
        /// <param name="newSubject">New subject of the lesson, ignored if null</param>
        /// <param name="newTeacher">New teacher of the lesson, ignored if null</param>
        /// <exception cref="InvalidOperationException">Thrown when tried to change the property to its current value</exception>
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

        /// <summary>
        /// Removes a lesson from the database
        /// </summary>
        /// <param name="lesson">The lesson to remove</param>
        /// <exception cref="InvalidOperationException">Thrown when tried to remove a lesson that is not in the database</exception>
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
