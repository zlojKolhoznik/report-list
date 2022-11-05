using DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    /// <summary>
    /// Implements adding, getting, removing and changing the information about homeworks
    /// </summary>
    internal class HomeworksManager : DatabaseAccessManager
    {
        private static object locker = new object();

        public HomeworksManager(string connStr) : base(connStr)
        {

        }

        /// <summary>
        /// Gets homeworks of the specified groups from the specified subject
        /// </summary>
        /// <param name="group">Group to get homework for</param>
        /// <param name="subject">Subject to get homework from</param>
        /// <returns>List of homeworks for specified group and subject</returns>
        public List<Homework> GetHomeworks(Group group, Subject subject)
        {
            var result = new List<Homework>();
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Homeworks.Where(hw => hw.Group.Id == group.Id && hw.Subject.Id == subject.Id).ToList();
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
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Homeworks.Where(hw => hw.Group.Id == group.Id && hw.DueDate.Date == dueDate.Date).ToList();
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
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Homeworks.Where(hw => hw.Group.Id == group.Id && hw.Teacher.Id == teacher.Id).ToList();
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
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Homeworks.Where(hw => hw.Teacher.Id == teacher.Id && hw.Subject.Id == subject.Id).ToList();
            }
            return result;
        }

        /// <summary>
        /// Adds a new homework to the database
        /// </summary>
        /// <param name="homework">Homework to add</param>
        public void AddHomework(Homework homework)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    context.Homeworks.Add(homework);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Removes the homework from the database
        /// </summary>
        /// <param name="homework">Homework to remove</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void RemoveHomework(Homework homework)
        {
            using (var context = new ReporlistContext(connStr))
            {
                if (!context.Homeworks.Any(hw => hw.Id == homework.Id))
                {
                    throw new InvalidOperationException("Cannot remove the homework that is not in the database");
                }
                lock (locker)
                {
                    Homework toRemove = context.Homeworks.First(hw => hw.Id == homework.Id);
                    context.Homeworks.Remove(toRemove);
                    context.SaveChanges();
                }
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
            if (newDueDate == homework.DueDate)
            {
                throw new InvalidOperationException("Cannot change the due date to its current value");
            }
            lock (locker)
            {
                homework.DueDate = newDueDate == null ? homework.DueDate : (DateTime)newDueDate;
                homework.FileExtension = newFileExtension == null ? homework.FileExtension : newFileExtension;
                homework.FileBytes = newFileBytes == null ? homework.FileBytes : newFileBytes;
            }
        }
    }
}
