namespace DatabaseClasses
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Topic { get; set; } = null!;
        public DateTime Date { get; set; }
        public Subject Subject { get; set; } = null!;
        public Teacher Teacher { get; set; } = null!;
        public Group Group { get; set; } = null!;
        public List<Mark> Marks { get; set; }
    }
}