using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace SMS_LMS.Utilities.EmailService
{
    public class EmailSender : IEmailSender
    {
        public IOptions<EmailStamp> _options { get; }

        public EmailSender(IOptions<EmailStamp> options)
        {
            _options = options;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var mail = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_options.Value.FromEmail),
                Subject = subject,
            };
            mail.From.Add(new MailboxAddress(_options.Value.Username, _options.Value.FromEmail));
            mail.To.Add(MailboxAddress.Parse(toEmail));

            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            mail.Body = builder.ToMessageBody();

            var smtp = new SmtpClient();
            smtp.Connect(_options.Value.Host, _options.Value.Port, _options.Value.EnableSsl);
            smtp.Authenticate(_options.Value.FromEmail, _options.Value.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);
            mail.To.Clear();
        }
    }
}
