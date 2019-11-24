using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace FplPriceNotificator.Email
    {
    public class EmailSender :IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings=emailSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }

        private async Task Execute(string email, string subject, string message)
        {
            try
            {
                var toEmail = string.IsNullOrEmpty(email)
                    ? _emailSettings.ToEmail
                    : email;
                var mail = new MailMessage()
                {
                    From=new MailAddress(_emailSettings.UsernameEmail, "Your friendly reminder")
                };
                mail.To.Add(new MailAddress(toEmail));
                mail.Subject=subject;
                mail.Body=message;
                mail.IsBodyHtml=true;
                mail.Priority=MailPriority.Normal;

                using (var smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials=
                        new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl=true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
    }
