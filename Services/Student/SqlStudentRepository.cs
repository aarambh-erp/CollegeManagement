using System;
using CollegeManagement.Models.Assignments;
using CollegeManagement.Models.Courses;
using CollegeManagement.Models.Fees;
using CollegeManagement.Models.Results;
using CollegeManagement.Models.Student;
using CollegeManagement.Models.Timetables;
using CollegeManagement.Models;
using CollegeManagement.Models.Lecturers;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagement.Services.Student
{
	public class SqlStudentRepository : IStudentRepository
	{
        private readonly CollegeContext _collegeContext;
		public SqlStudentRepository(CollegeContext collegeContext)
		{
            _collegeContext = collegeContext;
		}

        public List<StudentInfo> GetAllStudents()
        {
            return _collegeContext.Students.ToList();
        }

        public List<Assignment> GetAssignmentsByStudentId(int id)
        {
            return _collegeContext.Assignments
                    .Where(a => a.StudentId == id)
                    .Include(a => a.Course)
                    .Include(a => a.Student)
                    .Include(a => a.Lecturer)
                    .ToList();
        }
        public List<Course> GetCoursesByStudentId(int id)
        {
            return _collegeContext.StudentCourses
                .Where(sc => sc.StudentId == id)
                .Include(sc => sc.Course)
                    .ThenInclude(c => c.Lecturer)
                .Select(sc => sc.Course)
                .ToList();
        }

        public List<Fee> GetFeesByStudentId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Result> GetResultsByStudentId(int id)
        {
            return _collegeContext.Results
                    .Where(e => e.StudentId == id)
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .ToList();
        }

        public StudentInfo GetStudentByEmail(string Email)
        {
            return _collegeContext.Students.FirstOrDefault(e => e.Email == Email);
        }

        public StudentInfo GetStudentByid(int id)
        {
            return _collegeContext.Students.FirstOrDefault(e => e.Id == id);
        }

        public List<Timetable> GetTimetablesByStudentId(int id)
        {
            return _collegeContext.Timetables
                    .Where(e => e.StudentId == id)
                    .Include(e => e.Lecturer)
                    .Include(e => e.Course)
                    .Include(e => e.Room)
                    .ToList();
        }
        public int GetUpcomingAssignmentsCount(int studentId)
        {
            return _collegeContext.Assignments
                .Where(e => e.StudentId == studentId && e.DueDate > DateTime.Now)
                .Count();
        }
    }
}

