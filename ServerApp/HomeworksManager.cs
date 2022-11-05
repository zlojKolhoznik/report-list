using DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    internal class HomeworksManager : DatabaseAccessManager
    {
        private static object locker = new object();

        public HomeworksManager(string connStr) : base(connStr)
        {

        }

        public List<Homework> GetHomeworks(Group group, Subject subject)
        {
            var result = new List<Homework>();
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Homeworks.Where(hw => hw.Group.Id == group.Id && hw.Subject.Id == subject.Id).ToList();
            }
            return result;
        }

        public List<Homework> GetHomeworks(Group group, DateTime dueDate)
        {
            var result = new List<Homework>();
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Homeworks.Where(hw => hw.Group.Id == group.Id && hw.DueDate.Date == dueDate.Date).ToList();
            }
            return result;
        }

        public List<Homework> GetHomeworks(Teacher teacher, Group group)
        {
            var result = new List<Homework>();
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Homeworks.Where(hw => hw.Group.Id == group.Id && hw.Teacher.Id == teacher.Id).ToList();
            }
            return result;
        }

        public List<Homework> GetHomeworks(Teacher teacher, Subject subject)
        {
            var result = new List<Homework>();
            using (var context = new ReporlistContext(connStr))
            {
                result = context.Homeworks.Where(hw => hw.Teacher.Id == teacher.Id && hw.Subject.Id == subject.Id).ToList();
            }
            return result;
        }

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
