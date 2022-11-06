using System;
using System.Collections.Generic;

namespace ServerApp.Model
{
    public partial class Student
    {
        public Student()
        {
            Marks = new HashSet<Mark>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Mark> Marks { get; set; }
    }
}
