﻿// <auto-generated />
using System;
using AnyTest.DbAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnyTest.DbAccess.Migrations
{
    [DbContext(typeof(AnyTestDbContext))]
    [Migration("20200502114358_AddedTestSubjectsCollection")]
    partial class AddedTestSubjectsCollection
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AnyTest.Model.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Changed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<long?>("SubjectId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("AnyTest.Model.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronimic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("People");
                });

            modelBuilder.Entity("AnyTest.Model.Precondition", b =>
                {
                    b.Property<long>("TestId")
                        .HasColumnType("bigint");

                    b.Property<long>("PreconditionId")
                        .HasColumnType("bigint");

                    b.HasKey("TestId", "PreconditionId");

                    b.HasIndex("PreconditionId");

                    b.ToTable("Preconditions");
                });

            modelBuilder.Entity("AnyTest.Model.Student", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("AnyTest.Model.StudentCourse", b =>
                {
                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<long>("CourseId")
                        .HasColumnType("bigint");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("AnyTest.Model.StudentTest", b =>
                {
                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint");

                    b.HasKey("StudentId", "TestId");

                    b.HasIndex("TestId");

                    b.ToTable("StudentTests");
                });

            modelBuilder.Entity("AnyTest.Model.Subject", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Changed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("AnyTest.Model.Test", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Changed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVerstion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("AnyTest.Model.TestAnswer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderNo")
                        .HasColumnType("int");

                    b.Property<int>("Percent")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<long>("TestQuestionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TestQuestionId");

                    b.ToTable("TestAnswers");
                });

            modelBuilder.Entity("AnyTest.Model.TestCourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CourseId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<long>("OrderNo")
                        .HasColumnType("bigint");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("TestId");

                    b.ToTable("TestCourses");
                });

            modelBuilder.Entity("AnyTest.Model.TestQuestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderNo")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("TestQuestions");
                });

            modelBuilder.Entity("AnyTest.Model.TestSubject", b =>
                {
                    b.Property<long>("TestId")
                        .HasColumnType("bigint");

                    b.Property<long>("SubjectId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.HasKey("TestId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("TestSubjects");
                });

            modelBuilder.Entity("AnyTest.Model.Tutor", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Tutors");
                });

            modelBuilder.Entity("AnyTest.Model.TutorCourse", b =>
                {
                    b.Property<long>("TutorId")
                        .HasColumnType("bigint");

                    b.Property<long>("CourseId")
                        .HasColumnType("bigint");

                    b.HasKey("TutorId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("TutorCourses");
                });

            modelBuilder.Entity("AnyTest.Model.TutorTest", b =>
                {
                    b.Property<long>("TutorId")
                        .HasColumnType("bigint");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint");

                    b.HasKey("TutorId", "TestId");

                    b.HasIndex("TestId");

                    b.ToTable("TutorTests");
                });

            modelBuilder.Entity("AnyTest.Model.Course", b =>
                {
                    b.HasOne("AnyTest.Model.Tutor", "Author")
                        .WithMany("CoursesCreated")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnyTest.Model.Subject", "Subject")
                        .WithMany("Courses")
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("AnyTest.Model.Precondition", b =>
                {
                    b.HasOne("AnyTest.Model.TestCourse", "PreconditionTest")
                        .WithMany("Dependent")
                        .HasForeignKey("PreconditionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AnyTest.Model.TestCourse", "Test")
                        .WithMany("Preconditions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTest.Model.Student", b =>
                {
                    b.HasOne("AnyTest.Model.Person", "Person")
                        .WithOne("Student")
                        .HasForeignKey("AnyTest.Model.Student", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTest.Model.StudentCourse", b =>
                {
                    b.HasOne("AnyTest.Model.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AnyTest.Model.Student", "Student")
                        .WithMany("Courses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTest.Model.StudentTest", b =>
                {
                    b.HasOne("AnyTest.Model.Student", "Student")
                        .WithMany("Tests")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AnyTest.Model.Test", "Test")
                        .WithMany("Students")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTest.Model.Test", b =>
                {
                    b.HasOne("AnyTest.Model.Tutor", "Author")
                        .WithMany("TestsCreated")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTest.Model.TestAnswer", b =>
                {
                    b.HasOne("AnyTest.Model.TestQuestion", "TestQuestion")
                        .WithMany("TestAnswers")
                        .HasForeignKey("TestQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTest.Model.TestCourse", b =>
                {
                    b.HasOne("AnyTest.Model.Course", "Course")
                        .WithMany("Tests")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AnyTest.Model.Test", "Test")
                        .WithMany("Courses")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTest.Model.TestQuestion", b =>
                {
                    b.HasOne("AnyTest.Model.Test", "Test")
                        .WithMany("TestQuestions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTest.Model.TestSubject", b =>
                {
                    b.HasOne("AnyTest.Model.Subject", "Subject")
                        .WithMany("Tests")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AnyTest.Model.Test", "Test")
                        .WithMany("Subjects")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTest.Model.Tutor", b =>
                {
                    b.HasOne("AnyTest.Model.Person", "Person")
                        .WithOne("Tutor")
                        .HasForeignKey("AnyTest.Model.Tutor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTest.Model.TutorCourse", b =>
                {
                    b.HasOne("AnyTest.Model.Course", "Course")
                        .WithMany("Owners")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AnyTest.Model.Tutor", "Tutor")
                        .WithMany("CoursesOwned")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTest.Model.TutorTest", b =>
                {
                    b.HasOne("AnyTest.Model.Test", "Test")
                        .WithMany("Owners")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AnyTest.Model.Tutor", "Tutor")
                        .WithMany("TestsOwned")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
