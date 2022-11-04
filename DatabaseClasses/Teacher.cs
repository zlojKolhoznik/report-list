namespace DatabaseClasses
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public User User { get; set; } = null!;
        public List<Subject> Subjects { get; set; }
        public List<Group> Groups { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Mark> Marks { get; set; }
    }
}