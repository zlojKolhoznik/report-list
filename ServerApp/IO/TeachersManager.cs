using ServerApp.Model;

namespace ServerApp.IO
{
    internal class TeachersManager
    {
        /// <summary>
        /// Adds a new teacher to the database
        /// </summary>
        /// <remarks>DO NOT include the list of subjects and lessons in the Teacher object</remarks>
        /// <param name = "teacher" > Student to be added</param>
        /// <param name = "subjects" > Subjects of this teacher</param>
        public void AddTeacher(Teacher teacher, List<Subject>? subjects = null)
        {
            using (var context = new ReportlistContext())
            {
                if (context.Teachers.Any(t => t.UserId == teacher.UserId) || context.Students.Any(s => s.UserId == teacher.UserId))
                {
                    throw new ArgumentException($"Cannot add this teacher to the database. The user with ID {teacher.UserId} is already binded to a teacher or a student", nameof(teacher));
                }
                context.Teachers.Add(teacher);
                context.SaveChanges();
                if (subjects == null || subjects.Count < 1)
                {
                    return;
                }
                foreach (var subject in subjects)
                {
                    context.SubjectsTeachers.Add(new SubjectsTeacher() { SubjectId = subject.Id, TeacherId = teacher.Id });
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes a teacher from the database
        /// </summary>
        /// <remarks>This method will also remove all the marks, lessons and homeworks that have been set by this teacher and will remove the account of this teacher.Use wisely</remarks>
        /// <param name = "teacher" > The teacher to remove</param>
        /// <exception cref = "ArgumentException" ></ exception >
        public void RemoveTeacher(Teacher teacher)
        {
            using (var context = new ReportlistContext())
            {
                var toRemove = context.Teachers.FirstOrDefault(t => t.Id == teacher.Id);
                if (toRemove == null)
                {
                    throw new ArgumentException("Cannot remove the teacehr who is not in the database", nameof(teacher));
                }
                HomeworksManager hwm = new HomeworksManager();
                LessonsManager lm = new LessonsManager();
                AccountManager am = new AccountManager();
                string login = toRemove.User.Login;
                foreach (var homework in toRemove.Homeworks)
                {
                    hwm.RemoveHomework(homework);
                }
                foreach (var lesson in toRemove.Lessons)
                {
                    lm.RemoveLesson(lesson);
                }
                foreach (var subjectTeacher in toRemove.SubjectsTeachers)
                {
                    context.SubjectsTeachers.Remove(subjectTeacher);
                }
                context.Remove(toRemove);
                am.RemoveUser(login);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Change the data about the specified teacher in the database
        /// </summary>
        /// <param name = "teacher" > Student whose data to be changed</param>
        /// <param name = "newName" > New name of the teacher, no affect if null</param>
        /// <param name = "newSurname" > New surname of the teacher, no affect if null</param>
        /// <exception cref = "InvalidOperationException" ></ exception >
        /// < exception cref="ArgumentException"></exception>
        public void ChangeTeacherData(Teacher teacher, string? newName = null, string? newSurname = null)
        {
            using (var context = new ReportlistContext())
            {
                var toChange = context.Teachers.FirstOrDefault(t => t.Id == teacher.Id);
                if (toChange == null)
                {
                    throw new ArgumentException("The specified teacher is not in the database", nameof(teacher));
                }
                if (toChange.Name == newName)
                {
                    throw new InvalidOperationException("Cannot change name to its current value");
                }
                if (toChange.Surname == newSurname)
                {
                    throw new InvalidOperationException("Cannot change surname to its current value");
                }
                toChange.Name = newName == null ? toChange.Name : newName;
                toChange.Surname = newSurname == null ? toChange.Surname : newSurname;
                context.SaveChanges();
            }

        }

        /// <summary>
        /// Adds the new subject for the specified teacher
        /// </summary>
        /// <param name = "teacher" > Teacher to add subject to</param>
        /// <param name = "subject" > Subject to add</param>
        /// <exception cref = "InvalidOperationException" ></ exception >
        public void AddSubject(Teacher teacher, Subject subject)
        {
            using (var context = new ReportlistContext())
            {
                if (context.SubjectsTeachers.Any(st => st.TeacherId == teacher.Id && st.SubjectId == subject.Id))
                {
                    throw new InvalidOperationException("One teacher cannot have two equal subjects");
                }
                context.SubjectsTeachers.Add(new SubjectsTeacher() { SubjectId = subject.Id, TeacherId = teacher.Id });
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes the specified subject from the specified teacher's list
        /// </summary>
        /// <param name = "teacher" > Teacher to remove subject from</param>
        /// <param name = "subject" > Subject to remove</param>
        /// <exception cref = "InvalidOperationException" ></ exception >
        public void RemoveSubject(Teacher teacher, Subject subject)
        {
            using (var context = new ReportlistContext())
            {
                if (!context.SubjectsTeachers.Any(st => st.TeacherId == teacher.Id && st.SubjectId == subject.Id))
                {
                    throw new InvalidOperationException("Tried to remove subject that is not in this teacher's list");
                }
                var toRemove = context.SubjectsTeachers.First(st => st.TeacherId == teacher.Id && st.SubjectId == subject.Id);
                context.SubjectsTeachers.Remove(toRemove);
                context.SaveChanges();
            }
        }
    }
}
