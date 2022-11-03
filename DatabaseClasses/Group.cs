namespace DatabaseClasses
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Student> Students { get; set; }
        public List<Homework> Homeworks { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}