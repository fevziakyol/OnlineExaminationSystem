using System.Data.Entity;
using OnlineExaminationSystem.Models;

namespace OnlineExaminationSystem
{
    public class OESContext: DbContext
    {
        public OESContext()
            : base("name=OESContext")
        { 
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<AssignedExam> AssignedExams { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("");
        }
    }
}