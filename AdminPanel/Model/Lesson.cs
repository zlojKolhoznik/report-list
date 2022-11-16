using System;
using System.Collections.Generic;

namespace AdminPanel.Model
{
    public partial class Lesson
    {
        public Lesson()
        {
            GroupsLessons = new HashSet<GroupsLesson>();
            Marks = new HashSet<Mark>();
        }

        public int Id { get; set; }
        public string Topic { get; set; } = null!;
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }

        public virtual Subject Subject { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
        public virtual ICollection<GroupsLesson> GroupsLessons { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
    }
}
