using System;
using CollegeManagement.Models.Courses;
using CollegeManagement.Models.Student;
using CollegeManagement.Models.Lecturers;
using CollegeManagement.Models.Rooms;
using CollegeManagement.Models.Timetables;
using CollegeManagement.Models.Assignments;
using CollegeManagement.Models.Results;
using CollegeManagement.Models.Fees;
using CollegeManagement.Models;

namespace CollegeManagement.Services.Student
{
    public class MockStudentRepository : IStudentRepository
    {
        List<StudentInfo> Students = new List<StudentInfo>
        {
            new StudentInfo
            {
                Id = 1,
                Name = "XYZ",
                RollNo = "CS2025-001",
                Age = 20,
                Gender = "Male",
                Address = "New Delhi, India",
                AdmissionDate = new DateTime(2023, 07, 15),
                FatherName = "XYZ 1",
                Email = "xyz@example.com",
                DateOfBirth = new DateTime(2005, 03, 10),
                ContactNo = "9876543210",
                PhotoPath = ""
            }
        };
        List<Course> Courses = new List<Course>
        {
            new Course { Id = 1, CourseName = "Computer Science", CourseCode = "CS101", Credits = 4, Semester = "1", Status = "Enrolled", LecturerId = 1 },
            new Course { Id = 2, CourseName = "Mathematics", CourseCode = "MTH201", Credits = 3, Semester = "2", Status = "Dropped", LecturerId = 2 },
            new Course { Id = 3, CourseName = "Physics", CourseCode = "PHY301", Credits = 4, Semester = "1", Status = "Completed", LecturerId = 3 }
        };
        List<StudentCourse> StudentCourses = new List<StudentCourse>
        {
            new StudentCourse { StudentId = 1, CourseId = 1 },
            new StudentCourse { StudentId = 1, CourseId = 3 }
        };
        List<Lecturer> Lecturers = new List<Lecturer>
        {
            new Lecturer {Id = 1, Name = "Amit"},
            new Lecturer {Id = 2, Name = "Pavan"},
            new Lecturer {Id = 3, Name = "Sai"}
        };
        List<Room> Rooms = new List<Room>
        {
            new Room{Id = 101, RoomNo = "101"},
            new Room{Id = 201, RoomNo = "201"},
            new Room{Id = 301, RoomNo = "301"}
        };
        List<Timetable> Timetables = new List<Timetable>
        {
            new Timetable
            {
                Id = 1,
                StudentId = 1,
                CourseId = 1,
                Day = DayOfWeek.Monday,
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(10, 30, 0),
                LecturerId = 1,
                RoomId = 101
            },

            new Timetable
            {
                Id = 2,
                StudentId = 1,
                CourseId = 3,
                Day = DayOfWeek.Monday,
                StartTime = new TimeSpan(11, 0, 0),
                EndTime = new TimeSpan(12, 30, 0),
                LecturerId = 3,
                RoomId = 202
            },

            new Timetable
            {
                Id = 3,
                StudentId = 1,
                CourseId = 1,
                Day = DayOfWeek.Tuesday,
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(10, 30, 0),
                LecturerId = 1,
                RoomId = 101
            },

            new Timetable
            {
                Id = 4,
                StudentId = 1,
                CourseId = 3,
                Day = DayOfWeek.Wednesday,
                StartTime = new TimeSpan(11, 0, 0),
                EndTime = new TimeSpan(12, 30, 0),
                LecturerId = 3,
                RoomId = 202
            },

            new Timetable
            {
                Id = 5,
                StudentId = 1,
                CourseId = 1,
                Day = DayOfWeek.Thursday,
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(11, 30, 0),
                LecturerId = 1,
                RoomId = 101
            }
        };
        List<Assignment> Assignments = new List<Assignment>
        {
            new Assignment
            {
                Id = 11,
                StudentId = 1,
                CourseId = 1,
                Title = "Maths Assignment",
                Description = "Solve integration and differentiation problems (Chapter 3).",
                Status = "InProgress",
                AssignmentFilePath = "/assignments/math_assignment_1.pdf",
                SubmitDate = null,
                DueDate = new DateTime(2025, 01, 20, 23, 59, 00),
                LecturerId = 1
            },

            new Assignment
            {
                Id = 12,
                StudentId = 1,
                CourseId = 3,
                Title = "Physics Lab Report",
                Description = "Prepare a detailed report on the Motion & Friction experiment.",
                Status = "Pending",
                AssignmentFilePath = null,                  // No file submitted yet
                SubmitDate = null,
                DueDate = new DateTime(2025, 01, 25, 23, 59, 00),
                LecturerId = 2
            }
        };
        List<Result> Results = new List<Result>
        {
            new Result
            {
                Id = 1,
                StudentId = 1,
                CourseId = 1,
                MarksObtained = 85,
                TotalMarks = 100,
                GPA = 3.7,
                Semester = "Semester 1"
            },
            new Result
            {
                Id = 2,
                StudentId = 1,
                CourseId = 2,
                MarksObtained = 78,
                TotalMarks = 100,
                GPA = 3.4,
                Semester = "Semester 1"
            },
            new Result
            {
                Id = 3,
                StudentId = 1,
                CourseId = 3,
                MarksObtained = 92,
                TotalMarks = 100,
                GPA = 4.0,
                Semester = "Semester 2"
            },
            new Result
            {
                Id = 4,
                StudentId = 1,
                CourseId = 4,
                MarksObtained = 67,
                TotalMarks = 100,
                GPA = 2.9,
                Semester = "Semester 2"
            }
        };
        List<Fee> Fees = new List<Fee>
        {
            new Fee
            {
                Id = 1,
                StudentId = 1,
                FeeType = "Tuition Fee - Semester 1",
                Amount = 45000,
                PaymentDate = new DateTime(2025, 01, 10),
                PaymentStatus = "Paid"
            },
            new Fee
            {
                Id = 2,
                StudentId = 1,
                FeeType = "Library Fee",
                Amount = 3000,
                PaymentDate = new DateTime(2025, 02, 05),
                PaymentStatus = "Paid"
            },
            new Fee
            {
                Id = 3,
                StudentId = 1,
                FeeType = "Hostel Fee - January",
                Amount = 8000,
                PaymentDate = new DateTime(2025, 03, 01),
                PaymentStatus = "Due"
            }
        };

        public List<StudentInfo> GetAllStudents()
        {
            return Students;
        }

        public List<Course> GetCoursesByStudentId(int studentId)
        {
            var courseIds = StudentCourses
                .Where(x => x.StudentId == studentId)
                .Select(x => x.CourseId)
                .ToList();

            var courses = Courses
                .Where(c => courseIds.Contains(c.Id))
                .ToList();

            foreach (var course in courses)
            {
                course.Lecturer = Lecturers.FirstOrDefault(l => l.Id == course.LecturerId);
            }

            return courses;
        }


        public StudentInfo GetStudentByEmail(string Email)
        {
            StudentInfo student = Students.FirstOrDefault(e => e.Email == Email);
            return student;
        }

        public StudentInfo GetStudentByid(int id)
        {
            StudentInfo student = Students.FirstOrDefault(e => e.Id == id);
            return student;
        }

        public List<Timetable> GetTimetablesByStudentId(int id)
        {
            var timetables = Timetables
                             .Where(c => c.StudentId == id)
                             .ToList();
            foreach(var timetable in timetables)
            {
                timetable.Course = Courses.FirstOrDefault(e => e.Id == timetable.CourseId);
                timetable.Lecturer = Lecturers.FirstOrDefault(e => e.Id == timetable.LecturerId);
                timetable.Room = Rooms.FirstOrDefault(e => e.Id == timetable.RoomId);
            }
            return timetables;
        }
        public List<Assignment> GetAssignmentsByStudentId(int id)
        {
            var assignments = Assignments
                              .Where(e => e.StudentId == id)
                              .ToList();
            foreach(var assignment in assignments)
            {
                assignment.Course = Courses.FirstOrDefault(e => e.Id == assignment.CourseId);
                assignment.Student = Students.FirstOrDefault(e => e.Id == assignment.StudentId);
                assignment.Lecturer = Lecturers.FirstOrDefault(e => e.Id == assignment.LecturerId);

            }
            return assignments;
        }
        public List<Result> GetResultsByStudentId(int id)
        {
            var results = Results
                          .Where(e => e.StudentId == id)
                          .ToList();
            foreach(var result in results)
            {
                result.Student = Students.FirstOrDefault(e => e.Id == result.StudentId);
                result.Course = Courses.FirstOrDefault(e => e.Id == result.CourseId);
            }
            return results;
        }
        public List<Fee> GetFeesByStudentId(int studentId)
        {
            return Fees.Where(f => f.StudentId == studentId).ToList();
        }

        public int GetUpcomingAssignmentsCount(int id)
        {
            return Assignments
               .Where(e => e.StudentId == id && e.DueDate > DateTime.Now)
               .Count();
        }
    }
}

