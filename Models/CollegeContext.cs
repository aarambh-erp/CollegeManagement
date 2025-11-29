using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CollegeManagement.Models.Student;
using CollegeManagement.Models.Assignments;
using CollegeManagement.Models.Courses;
using CollegeManagement.Models.Fees;
using CollegeManagement.Models.Results;
using CollegeManagement.Models.Timetables;
using CollegeManagement.Models.Lecturers;
using CollegeManagement.Models.Rooms;

namespace CollegeManagement.Models
{
	public class CollegeContext : IdentityDbContext
    {
		public CollegeContext(DbContextOptions<CollegeContext> options) : base(options)
		{
		}
        public DbSet<StudentInfo> Students { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<StudentCourse> StudentCourses { get; set; }
		public DbSet<Result> Results { get; set; }
		public DbSet<Assignment> Assignments { get; set; }
		public DbSet<Timetable> Timetables { get; set; }
		public DbSet<Lecturer> Lecturers { get; set; }
		public DbSet<Room> Rooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // IMPORTANT for ASP.NET Identity

            // --------------------------
            // StudentCourse (Composite Key)
            // --------------------------
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            // --------------------------
            // Assignment Relationships
            // --------------------------
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Student)
                .WithMany()
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Course)
                .WithMany()
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Lecturer)
                .WithMany()
                .HasForeignKey(a => a.LecturerId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------
            // Timetable Relationships
            // --------------------------
            modelBuilder.Entity<Timetable>()
                .HasOne(t => t.Student)
                .WithMany()
                .HasForeignKey(t => t.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Timetable>()
                .HasOne(t => t.Course)
                .WithMany()
                .HasForeignKey(t => t.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Timetable>()
                .HasOne(t => t.Lecturer)
                .WithMany()
                .HasForeignKey(t => t.LecturerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Timetable>()
                .HasOne(t => t.Room)
                .WithMany()
                .HasForeignKey(t => t.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------
            // Results Relationships
            // --------------------------
            modelBuilder.Entity<Result>()
                .HasOne(r => r.Student)
                .WithMany()
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Course)
                .WithMany()
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------
            // SEED DATA
            // --------------------------

            // Lecturers
            modelBuilder.Entity<Lecturer>().HasData(
                new Lecturer { Id = 1, Name = "Amit" },
                new Lecturer { Id = 2, Name = "Pavan" },
                new Lecturer { Id = 3, Name = "Sai" }
            );

            // Students
            modelBuilder.Entity<StudentInfo>().HasData(
                new StudentInfo
                {
                    Id = 1,
                    Name = "XYZ",
                    RollNo = "CS2025-001",
                    Age = 20,
                    Gender = "Male",
                    Address = "New Delhi",
                    AdmissionDate = new DateTime(2023, 07, 15),
                    FatherName = "XYZ 1",
                    Email = "xyz@example.com",
                    DateOfBirth = new DateTime(2005, 03, 10),
                    ContactNo = "9876543210"
                }
            );

            // Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, CourseName = "Computer Science", CourseCode = "CS101", Credits = 4, Semester = "1", Status = "Enrolled", LecturerId = 1 },
                new Course { Id = 2, CourseName = "Mathematics", CourseCode = "MTH201", Credits = 3, Semester = "2", Status = "Dropped", LecturerId = 2 },
                new Course { Id = 3, CourseName = "Physics", CourseCode = "PHY301", Credits = 4, Semester = "1", Status = "Completed", LecturerId = 3 }
            );

            // StudentCourse mapping
            modelBuilder.Entity<StudentCourse>().HasData(
                new StudentCourse { StudentId = 1, CourseId = 1 },
                new StudentCourse { StudentId = 1, CourseId = 3 }
            );

            // Rooms
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, RoomNo = "A101" },
                new Room { Id = 2, RoomNo = "B202" },
                new Room { Id = 3, RoomNo = "C303" }
            );

            // Timetable
            modelBuilder.Entity<Timetable>().HasData(
                new Timetable
                {
                    Id = 1,
                    StudentId = 1,
                    CourseId = 1,
                    LecturerId = 1,
                    RoomId = 1,
                    Day = DayOfWeek.Monday,
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(10, 30, 0)
                },
                new Timetable
                {
                    Id = 2,
                    StudentId = 1,
                    CourseId = 3,
                    LecturerId = 3,
                    RoomId = 2,
                    Day = DayOfWeek.Wednesday,
                    StartTime = new TimeSpan(11, 0, 0),
                    EndTime = new TimeSpan(12, 30, 0)
                }
            );

            // Assignments
            modelBuilder.Entity<Assignment>().HasData(
                new Assignment
                {
                    Id = 1,
                    StudentId = 1,
                    CourseId = 1,
                    LecturerId = 1,
                    Title = "CS Assignment 1",
                    Description = "Basics of programming",
                    DueDate = new DateTime(2025, 12, 15),
                    SubmitDate = null,
                    Status = "InProgress"
                },
                new Assignment
                {
                    Id = 2,
                    StudentId = 1,
                    CourseId = 3,
                    LecturerId = 3,
                    Title = "Physics Lab Report",
                    Description = "Motion experiment",
                    DueDate = new DateTime(2025, 12, 18),
                    SubmitDate = null,
                    Status = "Pending"
                }
            );

            // Results
            modelBuilder.Entity<Result>().HasData(
                new Result
                {
                    Id = 1,
                    StudentId = 1,
                    CourseId = 1,
                    MarksObtained = 85,
                    TotalMarks = 100,
                    GPA = 9.0,
                    Semester = "1"
                },
                new Result
                {
                    Id = 2,
                    StudentId = 1,
                    CourseId = 3,
                    MarksObtained = 78,
                    TotalMarks = 100,
                    GPA = 8.2,
                    Semester = "1"
                }
            );
        }


    }
}