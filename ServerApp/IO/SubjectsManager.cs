using ServerApp.Model;

namespace ServerApp.IO
{
    internal class SubjectsManager
    {

        /// <summary>
        /// Gets subjects that are learnt by the specified group
        /// </summary>
        /// <param name="group">Group to search subjects for</param>
        /// <returns>The list of subjects that the specified group has lesspns from</returns>
        public List<Subject> GetSubjects(Group group)
        {
            var result = new List<Subject>();
            foreach (var lesson in group.GroupsLessons.Select(gl => gl.Lessons))
            {
                if (!result.Any(s => s.Id == lesson.SubjectId))
                {
                    result.Add(lesson.Subject);
                }
            }
            return result;
        }

        /// <summary>
        /// Adds a new subject to the database
        /// </summary>
        /// <param name="subject">Subject to add</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddSubject(Subject subject)
        {
            using (var context = new ReportlistContext())
            {
                if (context.Subjects.Any(s => s.Name == subject.Name))
                {
                    throw new InvalidOperationException("Subject with this name already exists in database");
                }
                context.Subjects.Add(subject);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes the subject from the database
        /// </summary>
        /// <remarks>This method will also remove all marks, homework and lessons from this subject. Use wisely</remarks>
        /// <param name="subject"></param>
        /// <exception cref="ArgumentException"></exception>
        public void RemoveSubject(Subject subject)
        {
            using (var context = new ReportlistContext())
            {
                var toRemove = context.Subjects.FirstOrDefault(t => t.Id == subject.Id);
                if (toRemove == null)
                {
                    throw new ArgumentException("Cannot remove the teacehr who is not in the database", nameof(subject));
                }
                HomeworksManager hwm = new HomeworksManager();
                LessonsManager lm = new LessonsManager();
                foreach (var homework in toRemove.Homeworks)
                {
                    hwm.RemoveHomework(homework);
                }
                foreach (var lesson in toRemove.Lessons)
                {
                    lm.RemoveLesson(lesson);
                }
                foreach (var subjectTeacher in toRemove.SubjectsTeachers)
                {
                    context.SubjectsTeachers.Remove(subjectTeacher);
                }
                context.Remove(toRemove);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Changes the name of the specified subject in the database
        /// </summary>
        /// <param name="subject">Subject whose name is to be changed</param>
        /// <param name="newName">The new name of the subject</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void RenameSubject(Subject subject, string newName)
        {
            if (subject.Name == newName)
            {
                throw new InvalidOperationException("Cannot change the name of the group to the current name");
            }
            using (var context = new ReportlistContext())
            {
                var toRename = context.Subjects.FirstOrDefault(s => s.Id == subject.Id);
                if (toRename == null)
                {
                    throw new ArgumentException("Cannot remove the subject that is not in the database", nameof(subject));
                }
                toRename.Name = newName;
                context.SaveChanges();
            }
        }
    }
}
