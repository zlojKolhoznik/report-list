namespace DatabaseClasses
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public Group Group { get; set; } = null!;
        public User User { get; set; } = null!;
        public List<Mark> Marks { get; set; }
    }
}