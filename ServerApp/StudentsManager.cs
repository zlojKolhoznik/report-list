using DatabaseClasses;

namespace ServerApp
{
    internal class StudentsManager : DatabaseAccessManager
    {
        private static object locker = new object();

        public StudentsManager(string connStr) : base(connStr)
        {

        }

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

        public void ChangeStudentData(Student student, string? newName = null, string? newSurname = null, DateTime? newDateOfBirth = null, Group? newGroup = null)
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
                if (student.DateOfBirth == newDateOfBirth)
                {
                    throw new InvalidOperationException("Cannot change date of birth to its current value");
                }
                if (student.Group.Id == newGroup?.Id)
                {
                    throw new InvalidOperationException("Cannot change group to its current value");
                }
                student.Name = newName == null ? student.Name : newName;
                student.Surname = newSurname == null ? student.Surname : newSurname;
                student.DateOfBirth = newDateOfBirth == null ? student.DateOfBirth : (DateTime)newDateOfBirth;
                student.Group = newGroup == null ? student.Group : newGroup;
            }
        }
    }
}
