using ServerApp.Model;

namespace ServerApp.IO
{
    /// <summary>
    /// Implements students adding
    /// </summary>
    internal class StudentsManager
    {
        /// <summary>
        /// Adds a new student to the database
        /// </summary>
        /// <param name="student">Student to be added</param>
        public void AddStudent(Student student)
        {
            using (var context = new ReportlistContext())
            {
                if (context.Teachers.Any(t => t.UserId == student.UserId) || context.Students.Any(s => s.UserId == student.UserId))
                {
                    throw new ArgumentException($"Cannot add this teacher to the database. The user with ID {student.UserId} is already binded to a teacher or a student", nameof(student));
                }
                context.Students.Add(student);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the list of the students who are members of the specified group
        /// </summary>
        /// <param name="group">Group to search student for</param>
        /// <returns>The list of Student objects</returns>
        public List<Student> GetStudents(Group group)
        {
            List<Student> result = new List<Student>();
            using (var context = new ReportlistContext())
            {
                result = context.Students.Where(s => s.GroupId == group.Id).ToList();
            }
            return result;
        }

        /// <summary>
        /// Removes a student from the database
        /// </summary>
        /// <remarks>This method will also remove all the marks and will delete the account of this user. Use wisely</remarks>
        /// <param name="student">A student ro remove</param>
        /// <exception cref="ArgumentException"></exception>
        public void RemoveStudent(Student student)
        {
            using (var context = new ReportlistContext())
            {
                var toRemove = context.Students.FirstOrDefault(s => s.Id == student.Id);
                if (toRemove == null)
                {
                    throw new ArgumentException("Cannot remove the student who is not in the database", nameof(student));
                }
                MarksManager mm = new MarksManager();
                AccountManager am = new AccountManager();
                string login = toRemove.User.Login;
                foreach (var mark in toRemove.Marks)
                {
                    mm.RemoveMark(mark);
                }
                context.Students.Remove(toRemove);
                am.RemoveUser(login);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Change the data about the specified student in the database
        /// </summary>
        /// <param name="student">Student whose data to be changed</param>
        /// <param name="newName">New name of the student, no affect if null</param>
        /// <param name="newSurname">New surname of the student, no affect if null</param>
        /// <param name="newGroup">New group of the student, no affect if null</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void ChangeStudentData(Student student, string? newName = null, string? newSurname = null, Group? newGroup = null)
        {
            using (var context = new ReportlistContext())
            {
                var toChange = context.Students.FirstOrDefault(s => s.Id == student.Id);
                if (toChange == null)
                {
                    throw new ArgumentException("Cannot change information about the students who is not in the database");
                }
                if (toChange.Name == newName)
                {
                    throw new InvalidOperationException("Cannot change name to its current value");
                }
                if (toChange.Surname == newSurname)
                {
                    throw new InvalidOperationException("Cannot change surname to its current value");
                }
                if (toChange.GroupId == newGroup?.Id)
                {
                    throw new InvalidOperationException("Cannot change group to its current value");
                }
                toChange.Name = newName == null ? toChange.Name : newName;
                toChange.Surname = newSurname == null ? toChange.Surname : newSurname;
                toChange.GroupId = newGroup == null ? toChange.GroupId : newGroup.Id;
                context.SaveChanges();
            }
        }
    }
}
