﻿using MailKit.Net.Smtp;
using MimeKit;
using SchoolProject.Data.Helper;
using SchoolProject.Service.Abstract;

namespace SchoolProject.Service.Implementation
{
    public class EmailService : IEmailService
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        #endregion
        #region Ctor
        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        #endregion
        #region Methods
        public async Task<string> SendEmailAsync(string email, string Message, string? reason)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "wellcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("School Team", _emailSettings.FromEmail));
                    message.To.Add(new MailboxAddress("testing", email));
                    message.Subject = reason == null ? "No Submitted" : reason;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                //end of sending email
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
        #endregion
    }
}
