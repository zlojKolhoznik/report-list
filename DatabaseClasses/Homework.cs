namespace DatabaseClasses
{
    public class Homework
    {
        public int Id { get; set; }
        public byte[] FileBytes { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public Group Group { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
    }
}