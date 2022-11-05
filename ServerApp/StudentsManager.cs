using DatabaseClasses;

namespace ServerApp
{
    /// <summary>
    /// Implements students adding
    /// </summary>
    internal class StudentsManager : DatabaseAccessManager
    {
        private static object locker = new object();

        public StudentsManager(string connStr) : base(connStr)
        {

        }

        /// <summary>
        /// Adds a new student to the database
        /// </summary>
        /// <param name="student">Student to be added</param>
        public void AddStudent(Student student)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    context.Students.Add(student);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Change the data about the specified student in the database
        /// </summary>
        /// <param name="student">Student whose data to be changed</param>
        /// <param name="newName">New name of the student, no affect if null</param>
        /// <param name="newSurname">New surname of the student, no affect if null</param>
        /// <param name="newGroup">New group of the student, no affect if null</param>
        /// <exception cref="InvalidOperationException">Thrown when tried to change data to its current value</exception>
        public void ChangeStudentData(Student student, string? newName = null, string? newSurname = null, Group? newGroup = null)
        {
            lock (locker)
            {
                if (student.Name == newName)
                {
                    throw new InvalidOperationException("Cannot change name to its current value");
                }
                if (student.Surname == newSurname)
                {
                    throw new InvalidOperationException("Cannot change surname to its current value");
                }
                if (student.Group.Id == newGroup?.Id)
                {
                    throw new InvalidOperationException("Cannot change group to its current value");
                }
                student.Name = newName == null ? student.Name : newName;
                student.Surname = newSurname == null ? student.Surname : newSurname;
                student.Group = newGroup == null ? student.Group : newGroup;
            }
        }
    }
}
