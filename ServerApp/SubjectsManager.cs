using DatabaseClasses;

namespace ServerApp
{
    internal class SubjectsManager : DatabaseAccessManager
    {
        private static object locker = new object();

        public SubjectsManager(string connStr) : base(connStr)
        {

        }

        /// <summary>
        /// Gets subjects that are lerant by the specified group
        /// </summary>
        /// <param name="group">Group to search subjects for</param>
        /// <returns>The list of subjects that the specified group has lesspns from</returns>
        public List<Subject> GetSubjects(Group group)
        {
            var result = new List<Subject>();
            foreach (var lesson in group.Lessons)
            {
                if (!result.Any(s => s.Id == lesson.Subject.Id))
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
        /// <exception cref="InvalidOperationException">Thrown when tried to add a subject with already existing name</exception>
        public void AddSubject(Subject subject)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    if (context.Subjects.Any(s => s.Name == subject.Name))
                    {
                        throw new InvalidOperationException("Subject with this name already exists in database");
                    }
                    context.Subjects.Add(subject);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Changes the name of the specified subject in the database
        /// </summary>
        /// <param name="subject">Subject whose name is to be changed</param>
        /// <param name="newName">The new name of the subject</param>
        /// <exception cref="InvalidOperationException">Thrown when the new name of the subject is equal to its current name or when tried to rename subject that is not in the database</exception>
        public void RenameGroup(Subject subject, string newName)
        {
            if (subject.Name == newName)
            {
                throw new InvalidOperationException("Cannot change the name of the group to the current name");
            }
            lock (locker)
            {
                subject.Name = newName;
            }
        }
    }
}
