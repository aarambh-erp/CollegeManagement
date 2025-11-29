using System;
using System.ComponentModel.DataAnnotations;

namespace CollegeManagement.Models.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        public string Identifier { get; set; } // Email OR Phone

        [Required]
        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}

