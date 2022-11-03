namespace DatabaseClasses
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Teacher> Teachers { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Homework> Homeworks { get; set; }
    }
}