using System;
using System.Collections.Generic;

namespace ServerApp.Model
{
    public partial class Mark
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int StudentId { get; set; }
        public int? LessonId { get; set; }
        public int? HomeworkId { get; set; }

        public virtual Homework? Homework { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public virtual Student Student { get; set; } = null!;
    }
}
