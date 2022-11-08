using ServerApp.Model;

namespace ServerApp
{
    /// <summary>
    /// Implements adding, getting and changing lessons
    /// </summary>
    internal class LessonsManager
    {
        /// <summary>
        /// Gets lessons of the specified group from the specified subject
        /// </summary>
        /// <param name="group">Group, whose lessons the method is to return</param>
        /// <param name="subject">Subject to search lessons from</param>
        /// <returns>The list of lessons of the specified group from the specified subject</returns>
        public List<Lesson> GetLessons(Group group, Subject subject)
        {
            List<Lesson> result;
            using (var context = new ReportlistContext())
            {
                result = context.Lessons.Where(l => l.Subject.Id == subject.Id && l.GroupsLessons.Select(gl => gl.Groups.Id).Contains(group.Id)).ToList();
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
            using (var context = new ReportlistContext())
            {
                result = context.Lessons.Where(l => l.Date.Date == date.Date && l.GroupsLessons.Select(gl => gl.Groups.Id).Contains(group.Id)).ToList();
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
            using (var context = new ReportlistContext())
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
            using (var context = new ReportlistContext())
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
            using (var context = new ReportlistContext())
            {
                result = context.Lessons.Where(l => l.GroupsLessons.Select(gl => gl.Groups.Id).Contains(group.Id) && l.Teacher.Id == teacher.Id).ToList();
            }
            return result;
        }

        /// <summary>
        /// Adds a new lesson to the database
        /// </summary>
        /// <param name="lesson">Lesson to add</param>
        /// <exception cref="InvalidOperationException"></exception>
        public async void AddLesson(Lesson lesson, List<Group> groups)
        {
            using (var context = new ReportlistContext())
            {
                var lessonsOfGroup = new List<Lesson>();
                foreach (var group in groups)
                {
                    lessonsOfGroup.AddRange(context.GroupsLessons.Where(gl => gl.GroupsId == group.Id).Select(gl => gl.Lessons).AsEnumerable());
                }
                var lessonsOfTeacher = context.Lessons.Where(l => l.TeacherId == lesson.TeacherId).ToList();
                if (lessonsOfGroup.Any(l => Math.Abs((lesson.Date - l.Date).Hours) < 1) || lessonsOfTeacher.Any(l => Math.Abs((lesson.Date - l.Date).Hours) < 1))
                {
                    throw new InvalidOperationException("Cannot add a new lesson if there is a lesson in the database for the same group or the same teacher on the same time");
                }
                context.Lessons.Add(lesson);
                context.SaveChanges();
                foreach (var group in groups)
                {
                    var groupsLesson = new GroupsLesson() { GroupsId = group.Id, LessonsId = lesson.Id };
                    context.GroupsLessons.Add(groupsLesson);
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
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
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
            using (var context = new ReportlistContext())
            {
                var toChange = context.Lessons.FirstOrDefault(l => l.Id == lesson.Id);
                if (toChange == null)
                {
                    throw new ArgumentException("Cannot change the lesson than is not in the database", nameof(lesson));
                }
                toChange.Topic = newTopic == null ? toChange.Topic : newTopic;
                toChange.Date = newDate == null ? toChange.Date : (DateTime)newDate;
                toChange.Subject = newSubject == null ? toChange.Subject : newSubject;
                toChange.Teacher = newTeacher == null ? toChange.Teacher : newTeacher;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes a lesson from the database
        /// </summary>
        /// <remarks>This method will also remove all the marks for this lesson. Use wisely</remarks>
        /// <param name="lesson">The lesson to remove</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveLesson(Lesson lesson)
        {
            using (var context = new ReportlistContext())
            {
                var toRemove = context.Lessons.FirstOrDefault(l => l.Id == lesson.Id);
                MarksManager mm = new MarksManager();
                if (toRemove == null)
                {
                    throw new InvalidOperationException("Cannot remove the lesson that is not in the database");
                }
                foreach (var mark in lesson.Marks)
                {
                    mm.RemoveMark(mark);
                }
                context.Lessons.Remove(toRemove);
                context.SaveChanges();
            }
        }
    }
}
