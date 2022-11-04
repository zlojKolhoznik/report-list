using DatabaseClasses;

namespace ServerApp
{
    /// <summary>
    /// Implements marks getting and adding
    /// </summary>
    internal class MarksManager : DatabaseAccessManager
    {
        private static object locker = new object();

        public MarksManager(string connStr) : base(connStr)
        {

        }

        /// <summary>
        /// Gets the list of marks of the specified student
        /// </summary>
        /// <param name="student">Student whose marks method is to return</param>
        /// <param name="subject">Subject of marks method is to return, all marks if null</param>
        /// <returns>The List of marks of the specified student</returns>
        public List<Mark> GetMarks(Student student, Subject? subject = null)
        {
            List<Mark> result;
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    result = context.Marks.Where(s => s.Student.Id == student.Id).ToList();
                }
            }
            if (subject != null)
            {
                result = SelectMarks(result, subject);
            }
            return result;
        }

        /// <summary>
        /// Gets the list of marks of the specified student
        /// </summary>
        /// <param name="group">Group whose marks method is to return</param>
        /// <param name="subject">Subject of marks method is to return, ignores this parameter if null</param>
        /// <param name="teacher">Subject of marks method is to return, ignores this parameter if null</param>
        /// <returns>The List of marks of the specified group</returns>
        public List<Mark> GetMarks(Group group, Subject? subject = null, Teacher? teacher = null)
        {
            List<Mark> result;
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    result = context.Marks.Where(s => s.Student.Group.Id == group.Id).ToList();
                }
            }
            result = SelectMarks(result, subject, teacher);
            return result;
        }

        /// <summary>
        /// Adds a new mark to the database
        /// </summary>
        /// <param name="mark">Mark the method is to add</param>
        public void AddMark(Mark mark)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    context.Marks.Add(mark);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Changes an existing mark value in database
        /// </summary>
        /// <param name="mark">The mark, value of which is to be changed</param>
        /// <param name="newValue">New value of the mark</param>
        /// <exception cref="ArgumentException">Thrown when the mark is not in database</exception>
        public void ChangeMark(Mark mark, int newValue)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    Mark? markFromDb = context.Marks.FirstOrDefault(m => m.Id == mark.Id);
                    if (markFromDb == null)
                    {
                        throw new ArgumentException("This mark is not in database", nameof(mark));
                    }
                    markFromDb.Value = newValue;
                    context.SaveChanges();
                }
            }
        }

        private static List<Mark> SelectMarks(List<Mark> source, Subject? subject = null, Teacher? teacher = null)
        {
            if (subject != null)
            {
                source = source.Where(m => m.Subject.Id == subject!.Id).ToList();
            }
            if (teacher != null)
            {
                source = source.Where(m => m.Teacher.Id == teacher!.Id).ToList();
            }
            return source;
        }
    }
}
