using System;
using System.ComponentModel.DataAnnotations;

namespace CollegeManagement.Models.ViewModels.Account
{
    public class VerifyOtpViewModel
    {
        public string Identifier { get; set; } // Email OR Phone

        [Required]
        public string Otp { get; set; }
    }
}

