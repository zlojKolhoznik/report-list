namespace DatabaseClasses
{
    public class Mark
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
        public Student Student { get; set; } = null!;
        public Teacher Teacher { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
    }
}