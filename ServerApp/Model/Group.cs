using System;
using System.Collections.Generic;

namespace ServerApp.Model
{
    public partial class Group
    {
        public Group()
        {
            GroupsLessons = new HashSet<GroupsLesson>();
            Homeworks = new HashSet<Homework>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<GroupsLesson> GroupsLessons { get; set; }
        public virtual ICollection<Homework> Homeworks { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
