using Microsoft.EntityFrameworkCore;

namespace DatabaseClasses
{
    /// <summary>
    /// Class that describes the database model
    /// </summary>
    public class ReporlistContext : DbContext
    {
        private readonly string connectionString;

        /// <summary>
        /// Creates a ReportlistContext instance that connects to SQL-Server database with specified connection string
        /// </summary>
        /// <param name="connectionString">The connection string of database instance is about to connect</param>
        public ReporlistContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}