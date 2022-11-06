using System;
using System.Collections.Generic;

namespace ServerApp.Model
{
    public partial class Teacher
    {
        public Teacher()
        {
            Homeworks = new HashSet<Homework>();
            Lessons = new HashSet<Lesson>();
            SubjectsTeachers = new HashSet<SubjectsTeacher>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Homework> Homeworks { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<SubjectsTeacher> SubjectsTeachers { get; set; }
    }
}
