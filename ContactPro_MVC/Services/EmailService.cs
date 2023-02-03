using ContactPro_MVC.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ContactPro_MVC.Services
{
    public class EmailService : IEmailSender
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }   

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // inject mail settings
            var emailSender = _mailSettings.Email;
            // handles all formating of emails
            MimeMessage newEmail = new();
            // ensures that it is in correct format for mimemessage
            newEmail.Sender = MailboxAddress.Parse(emailSender);

            // grab each email that is being mailed to
            foreach(var emailAddress in email.Split(";")){
                newEmail.To.Add(MailboxAddress.Parse(emailAddress));
            }

            newEmail.Subject = subject;
            //formats the message from html to email
            BodyBuilder emailBody = new();
            emailBody.HtmlBody = htmlMessage;

            newEmail.Body = emailBody.ToMessageBody();

            // Log into the smtp client
            using SmtpClient smtpClient = new();

            try
            {
                var host = _mailSettings.Host;
                var port = _mailSettings.Port;
                var password = _mailSettings.Password;
                // connects to smtp client
                await smtpClient.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(emailSender, password);
                //send and disconnect email service
                await smtpClient.SendAsync(newEmail);
                await smtpClient.DisconnectAsync(true);

            } catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }
        }
    }
}
