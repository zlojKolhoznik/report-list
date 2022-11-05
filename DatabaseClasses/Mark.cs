namespace DatabaseClasses
{
    public class Mark
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public Student Student { get; set; } = null!;
        public Lesson? Lesson { get; set; }
        public Homework? Homework { get; set; }
    }
}