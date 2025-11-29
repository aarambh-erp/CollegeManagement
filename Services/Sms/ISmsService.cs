using System;
namespace CollegeManagement.Services.Sms
{
	public interface ISmsService
	{
        public Task SendSmsAsync(string phone, string body);
    }
}

