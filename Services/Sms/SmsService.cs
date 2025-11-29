using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Microsoft.Extensions.Options;
using CollegeManagement.Models.Emails; 
namespace CollegeManagement.Services.Sms
{
    public class SmsService : ISmsService
    {
        private readonly SmsSettings _settings;

        public SmsService(IOptions<SmsSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendSmsAsync(string toNumber, string message)
        {
            TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);

            var msg = await MessageResource.CreateAsync(
                to: new PhoneNumber(toNumber),
                from: new PhoneNumber(_settings.FromNumber),
                body: message
            );
        }
    }
}
