using System;
using CollegeManagement.Models.Student;
using CollegeManagement.Models.Courses;
using CollegeManagement.Models.Timetables;
using CollegeManagement.Models.Assignments;
using CollegeManagement.Models.Results;
using CollegeManagement.Models.Fees;
namespace CollegeManagement.Services
{
	public interface IStudentRepository
	{
		public List<StudentInfo> GetAllStudents();
		public StudentInfo GetStudentByid(int id);
		public StudentInfo GetStudentByEmail(string Email);
		public List<Course> GetCoursesByStudentId(int id);
		public List<Timetable> GetTimetablesByStudentId(int id);
		public List<Assignment> GetAssignmentsByStudentId(int id);
		public List<Result> GetResultsByStudentId(int id);
		public List<Fee> GetFeesByStudentId(int id);
		public int GetUpcomingAssignmentsCount(int id);
    }
}