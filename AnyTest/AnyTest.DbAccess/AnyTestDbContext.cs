using AnyTest.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyTest.DbAccess
{
    public class AnyTestDbContext : DbContext
    {
        public AnyTestDbContext(DbContextOptions<AnyTestDbContext> options) : base(options) { }

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<TestQuestion> TestQuestions { get; set; }
        public virtual DbSet<TestAnswer> TestAnswers { get; set; }
        public virtual DbSet<TutorCourse> TutorCourses { get; set; }
        public virtual DbSet<TutorTest> TutorTests { get; set; }
        public virtual DbSet<StudentTest> StudentTests { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }
        public virtual DbSet<TestSubject> TestSubjects { get; set; }
        public virtual DbSet<TestCourse> TestCourses { get; set; }
        public virtual DbSet<Precondition> Preconditions { get; set; }
        public virtual DbSet<TestPass> TestPasses { get; set; }
        public DbSet<AnswerPass> AnswerPasses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Person>().HasIndex(p => p.Email).IsUnique();

            model.Entity<TutorTest>().HasKey(tt => new { tt.TutorId, tt.TestId });
            model.Entity<TutorTest>().HasOne(tt => tt.Tutor).WithMany(t => t.TestsOwned).HasForeignKey(tt => tt.TutorId).OnDelete(DeleteBehavior.NoAction);
            model.Entity<TutorTest>().HasOne(tt => tt.Test).WithMany(t => t.Owners).HasForeignKey(tt => tt.TestId).OnDelete(DeleteBehavior.NoAction);

            model.Entity<TutorCourse>().HasKey(tc => new { tc.TutorId, tc.CourseId });
            model.Entity<TutorCourse>().HasOne(tc => tc.Tutor).WithMany(t => t.CoursesOwned).HasForeignKey(tc => tc.TutorId).OnDelete(DeleteBehavior.NoAction);
            model.Entity<TutorCourse>().HasOne(tc => tc.Course).WithMany(c => c.Owners).HasForeignKey(tc => tc.CourseId).OnDelete(DeleteBehavior.NoAction);

            model.Entity<StudentTest>().HasKey(st => new { st.StudentId, st.TestId });
            model.Entity<StudentTest>().HasOne(st => st.Student).WithMany(s => s.Tests).HasForeignKey(st => st.StudentId).OnDelete(DeleteBehavior.NoAction);
            model.Entity<StudentTest>().HasOne(st => st.Test).WithMany(t => t.Students).HasForeignKey(st => st.TestId).OnDelete(DeleteBehavior.NoAction);

            model.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
            model.Entity<StudentCourse>().HasOne(cs => cs.Student).WithMany(s => s.Courses).HasForeignKey(sc => sc.StudentId).OnDelete(DeleteBehavior.NoAction);
            model.Entity<StudentCourse>().HasOne(sc => sc.Course).WithMany(c => c.Students).HasForeignKey(sc => sc.CourseId).OnDelete(DeleteBehavior.NoAction);

            model.Entity<TestSubject>().HasKey(ts => new { ts.TestId, ts.SubjectId });
            model.Entity<TestSubject>().HasOne(ts => ts.Test).WithMany(t => t.Subjects).HasForeignKey(ts => ts.TestId).OnDelete(DeleteBehavior.NoAction);
            model.Entity<TestSubject>().HasOne(ts => ts.Subject).WithMany(s => s.Tests).HasForeignKey(ts => ts.SubjectId).OnDelete(DeleteBehavior.NoAction);

            model.Entity<TestCourse>().HasOne(tc => tc.Test).WithMany(t => t.Courses).HasForeignKey(tc => tc.TestId).OnDelete(DeleteBehavior.NoAction);
            model.Entity<TestCourse>().HasOne(tc => tc.Course).WithMany(c => c.Tests).HasForeignKey(tc => tc.CourseId).OnDelete(DeleteBehavior.NoAction);
            model.Entity<TestCourse>().HasIndex(tc => tc.CourseId);

            model.Entity<Precondition>().HasKey(p => new { p.TestId, p.PreconditionId });
            model.Entity<Precondition>().HasOne(p => p.Test).WithMany(t => t.Preconditions).HasForeignKey(p => p.TestId).OnDelete(DeleteBehavior.NoAction);
            model.Entity<Precondition>().HasOne(p => p.PreconditionTest).WithMany(t => t.Dependent).HasForeignKey(p => p.PreconditionId).OnDelete(DeleteBehavior.NoAction);

            model.Entity<TestPass>().HasOne(p => p.Test).WithMany(t => t.Passes).HasForeignKey(p => p.TestId).OnDelete(DeleteBehavior.NoAction);
            model.Entity<TestPass>().HasIndex(p => p.StudentId);
            model.Entity<AnswerPass>().HasOne(a => a.Pass).WithMany(p => p.Answers).HasForeignKey(a => a.PassId).OnDelete(DeleteBehavior.NoAction);
            model.Entity<AnswerPass>().HasOne(a => a.Question).WithMany(q => q.Answered).HasForeignKey(a => a.QuestionId).OnDelete(DeleteBehavior.NoAction);
            model.Entity<AnswerPass>().HasIndex(a => a.PassId);
        }
    }
}
