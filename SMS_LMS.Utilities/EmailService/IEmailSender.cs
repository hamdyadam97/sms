using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Utilities.EmailService
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
