using System;
using System.Collections.Generic;

namespace AdminPanel.Model
{
    public partial class Subject
    {
        public Subject()
        {
            Homeworks = new HashSet<Homework>();
            Lessons = new HashSet<Lesson>();
            SubjectsTeachers = new HashSet<SubjectsTeacher>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Homework> Homeworks { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<SubjectsTeacher> SubjectsTeachers { get; set; }
    }
}
