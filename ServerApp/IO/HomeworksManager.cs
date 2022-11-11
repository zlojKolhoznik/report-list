using ServerApp.Model;

namespace ServerApp.IO
{
    /// <summary>
    /// Implements adding, getting, removing and changing the information about homeworks
    /// </summary>
    internal class HomeworksManager
    {
        /// <summary>
        /// Gets homeworks of the specified groups from the specified subject
        /// </summary>
        /// <param name="group">Group to get homework for</param>
        /// <param name="subject">Subject to get homework from</param>
        /// <returns>List of homeworks for specified group and subject</returns>
        public List<Homework> GetHomeworks(Group group, Subject subject)
        {
            var result = new List<Homework>();
            using (var context = new ReportlistContext())
            {
                result = context.Homeworks.Where(hw => hw.GroupId == group.Id && hw.SubjectId == subject.Id).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gets homeworks of the specified groups assigned on the specified date
        /// </summary>
        /// <param name="group">Group to get homework for</param>
        /// <param name="dueDate">Date the homeworks should be assigned</param>
        /// <returns>List of homeworks for specified group assgned on the specified date</returns>
        public List<Homework> GetHomeworks(Group group, DateTime dueDate)
        {
            var result = new List<Homework>();
            using (var context = new ReportlistContext())
            {
                result = context.Homeworks.Where(hw => hw.GroupId == group.Id && hw.DueDate.Date == dueDate.Date).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gets homeworks of the specified groups assigned by the specified teacher
        /// </summary>
        /// <param name="group">Group to get homework for</param>
        /// <param name="teacher">Teacher who assigned homeworks</param>
        /// <returns>List of homeworks for specified group and assigned by the specified teacher</returns>
        public List<Homework> GetHomeworks(Teacher teacher, Group group)
        {
            var result = new List<Homework>();
            using (var context = new ReportlistContext())
            {
                result = context.Homeworks.Where(hw => hw.GroupId == group.Id && hw.TeacherId == teacher.Id).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gets homeworks assigned by the specified teacher from the specified subject
        /// </summary>
        /// <param name="teacher">Teacher who assigned the homeworks</param>
        /// <param name="subject">Subject to get homework from</param>
        /// <returns>List of homeworks for specified group and subject</returns>
        public List<Homework> GetHomeworks(Teacher teacher, Subject subject)
        {
            var result = new List<Homework>();
            using (var context = new ReportlistContext())
            {
                result = context.Homeworks.Where(hw => hw.TeacherId == teacher.Id && hw.SubjectId == subject.Id).ToList();
            }
            return result;
        }

        /// <summary>
        /// Adds a new homework to the database
        /// </summary>
        /// <param name="homework">Homework to add</param>
        public void AddHomework(Homework homework)
        {
            using (var context = new ReportlistContext())
            {
                context.Homeworks.Add(homework);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes the homework from the database
        /// </summary>
        /// <remarks>This method will also remove all the marks for this homework. Use wisely</remarks>
        /// <param name="homework">Homework to remove</param>
        /// <exception cref="ArgumentException"></exception>
        public void RemoveHomework(Homework homework)
        {
            using (var context = new ReportlistContext())
            {
                var toRemove = context.Homeworks.FirstOrDefault(hw => hw.Id == homework.Id);
                MarksManager mm = new MarksManager();
                if (toRemove == null)
                {
                    throw new ArgumentException("Cannot remove the homework that is not in the database", nameof(homework));
                }
                foreach (var mark in toRemove.Marks)
                {
                    mm.RemoveMark(mark);
                }
                context.Homeworks.Remove(toRemove);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Changes the information about the homework
        /// </summary>
        /// <remarks>
        /// newFileBytes and newFileExtension parameters should be both null or both have a non-null value. In other case the method will throw an exception
        /// </remarks>
        /// <param name="homework">Homework to change</param>
        /// <param name="newFileBytes">New bytes of the file with the task, ignored if null</param>
        /// <param name="newFileExtension">Extension of the new file with the task, ignored if null</param>
        /// <param name="newDueDate">New date on which students must do the homework, ignored if null</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void ChangeHomeworkInfo(Homework homework, byte[]? newFileBytes = null, string? newFileExtension = null, DateTime? newDueDate = null)
        {
            if (newFileBytes == null ^ newFileExtension == null)
            {
                throw new InvalidOperationException("Cannot change the file without changing the extension and cannot change the extension without changing the file");
            }
            using (var context = new ReportlistContext())
            {
                var toChange = context.Homeworks.FirstOrDefault(hw => hw.Id == homework.Id);
                if (toChange == null)
                {
                    throw new ArgumentException("Cannot change the homework that is not in the database", nameof(homework));
                }
                if (newDueDate == toChange.DueDate)
                {
                    throw new InvalidOperationException("Cannot change the due date to its current value");
                }
                toChange.DueDate = newDueDate == null ? toChange.DueDate : (DateTime)newDueDate;
                toChange.FileExtension = newFileExtension == null ? toChange.FileExtension : newFileExtension;
                toChange.FileBytes = newFileBytes == null ? toChange.FileBytes : newFileBytes;
                context.SaveChanges();
            }

        }
    }
}
