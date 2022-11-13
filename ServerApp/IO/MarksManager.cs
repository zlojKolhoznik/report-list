using ServerApp.Model;
using Microsoft.EntityFrameworkCore;
namespace ServerApp.IO
{
    /// <summary>
    /// Implements marks getting and adding
    /// </summary>
    internal class MarksManager
    {

        /// <summary>
        /// Gets the list of marks of the specified student
        /// </summary>
        /// <param name="group">Group whose marks method is to return</param>
        /// <param name="subject">Subject of marks method is to return, ignores this parameter if null</param>
        /// <param name="teacher">Subject of marks method is to return, ignores this parameter if null</param>
        /// <returns>The List of marks of the specified group</returns>
        public List<Mark> GetMarks(Group group, Subject? subject = null, Teacher? teacher = null)
        {
            var result = new List<Mark>();
            using (var context = new ReportlistContext())
            {
                result = context.Marks.Include(m => m.Student)
                                      .Include(m => m.Homework)
                                      .ThenInclude(hw => hw.Subject)
                                      .Include(m => m.Lesson)
                                      .ThenInclude(l => l.Subject)
                                      .Where(m => m.Student.GroupId == group.Id)
                                      .ToList();
            }
            result = SelectMarks(result, subject, teacher);
            return result;
        }

        public List<Mark> GetMarks(Student student)
        {
            List<Mark> result = new List<Mark>();
            using (var context = new ReportlistContext())
            {
                result = context.Marks.Include(m => m.Lesson)
                                      .ThenInclude(l => l.Subject)
                                      .Include(m => m.Homework)
                                      .ThenInclude(hw => hw.Subject)
                                      .Where(m => m.StudentId == student.Id)
                                      .ToList();
            }
            return result;
        }

        /// <summary>
        /// Adds a new mark to the database
        /// </summary>
        /// <param name="mark">Mark the method is to add</param>
        public void AddMark(Mark mark)
        {
            using (var context = new ReportlistContext())
            {
                context.Marks.Add(mark);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Changes an existing mark value in database
        /// </summary>
        /// <param name="mark">The mark, value of which is to be changed</param>
        /// <param name="newValue">New value of the mark</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void ChangeMark(Mark mark, int newValue)
        {
            using (var context = new ReportlistContext())
            {
                var toChange = context.Marks.FirstOrDefault(m => m.Id == mark.Id);
                if (toChange == null)
                {
                    throw new ArgumentException("Cannot change the mark that is not in the database", nameof(mark));
                }
                if (toChange.Value == newValue)
                {
                    throw new InvalidOperationException("Cannot change mark value to its current value");
                }
                toChange.Value = newValue;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes a mark from the database
        /// </summary>
        /// <param name="mark">Mark to remove</param>
        /// <exception cref="ArgumentException"></exception>
        public void RemoveMark(Mark mark)
        {
            using (var context = new ReportlistContext())
            {
                var toRemove = context.Marks.FirstOrDefault(m => m.Id == mark.Id);
                if (toRemove == null)
                {
                    throw new ArgumentException("Cannot remove the mark that is not in the database", nameof(mark));
                }
                context.Marks.Remove(toRemove);
                context.SaveChanges();
            }
        }

        private static List<Mark> SelectMarks(List<Mark> source, Subject? subject = null, Teacher? teacher = null)
        {
            if (subject != null)
            {
                source = source.Where(m => m.Lesson?.SubjectId == subject.Id || m.Homework?.SubjectId == subject.Id).ToList();
            }
            if (teacher != null)
            {
                source = source.Where(m => m.Lesson?.TeacherId == teacher.Id || m.Homework?.TeacherId == teacher.Id).ToList();
            }
            return source;
        }
    }
}
