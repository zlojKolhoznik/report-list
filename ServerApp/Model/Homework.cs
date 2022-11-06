using System;
using System.Collections.Generic;

namespace ServerApp.Model
{
    public partial class Homework
    {
        public Homework()
        {
            Marks = new HashSet<Mark>();
        }

        public int Id { get; set; }
        public byte[] FileBytes { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
        public virtual ICollection<Mark> Marks { get; set; }
    }
}
