using System;
using CollegeManagement.Models.Student;

namespace CollegeManagement.Models.Fees
{
	public class Fee
	{
        public int Id { get; set; }

        public int StudentId { get; set; }
        public StudentInfo Student { get; set; }

        public string FeeType { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}

