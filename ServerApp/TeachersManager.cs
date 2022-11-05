using DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    internal class TeachersManager : DatabaseAccessManager
    {
        private object locker = new object();

        public TeachersManager(string connStr) : base(connStr)
        {

        }

        // <summary>
        /// Adds a new teacher to the database
        /// </summary>
        /// <param name="teacher">Student to be added</param>
        public void AddTeacher(Teacher teacher)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    context.Teachers.Add(teacher);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Change the data about the specified teacher in the database
        /// </summary>
        /// <param name="teacher">Student whose data to be changed</param>
        /// <param name="newName">New name of the teacher, no affect if null</param>
        /// <param name="newSurname">New surname of the teacher, no affect if null</param>
        /// <param name="newGroup">New group of the teacher, no affect if null</param>
        /// <exception cref="InvalidOperationException">Thrown when tried to change data to its current value</exception>
        public void ChangeTeachertData(Teacher teacher, string? newName = null, string? newSurname = null)
        {
            lock (locker)
            {
                if (teacher.Name == newName)
                {
                    throw new InvalidOperationException("Cannot change name to its current value");
                }
                if (teacher.Surname == newSurname)
                {
                    throw new InvalidOperationException("Cannot change surname to its current value");
                }
                teacher.Name = newName == null ? teacher.Name : newName;
                teacher.Surname = newSurname == null ? teacher.Surname : newSurname;
            }
        }

        /// <summary>
        /// Adds the new subject for the specified teacher
        /// </summary>
        /// <param name="teacher">Teacher to add subject to</param>
        /// <param name="subject">Subject to add</param>
        /// <exception cref="InvalidOperationException">Thrown when tried to add subject that is already in teacher's list</exception>
        public void AddSubject(Teacher teacher, Subject subject)
        {
            if (teacher.Subjects.Any(s => s.Id == subject.Id))
            {
                throw new InvalidOperationException("There cannot be two equal subjects for the same teacher");
            }
            lock (locker)
            {
                teacher.Subjects.Add(subject);
            }
        }

        /// <summary>
        /// Removes the specified subject from the specified teacher's list
        /// </summary>
        /// <param name="teacher">Teacher to remove subject from</param>
        /// <param name="subject">Subject to remove</param>
        /// <exception cref="InvalidOperationException">Thrown when tried to remove subject that is not in specified teacher's list</exception>
        public void RemoveSubject(Teacher teacher, Subject subject)
        {
            if (!teacher.Subjects.Any(s => s.Id == subject.Id))
            {
                throw new InvalidOperationException("Tried to remove subject that is not in this teacher's list");
            }
            lock (locker)
            {
                Subject toRemove = teacher.Subjects.First(s => s.Id == subject.Id);
                teacher.Subjects.Remove(toRemove);
            }
        }
    }
}
