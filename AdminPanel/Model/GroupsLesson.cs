using System;
using System.Collections.Generic;

namespace AdminPanel.Model
{
    public partial class GroupsLesson
    {
        public int Id { get; set; }
        public int GroupsId { get; set; }
        public int LessonsId { get; set; }

        public virtual Group Groups { get; set; } = null!;
        public virtual Lesson Lessons { get; set; } = null!;
    }
}
