using MailKit.Net.Smtp;
using MimeKit;
using SidPortfolio.Infrastructure.Repositories.Services.EmailService;
using SidPortfolio.Models;
using System;
using System.Threading.Tasks;

namespace SidPortfolio.Repositories.Services.EmailService
{
    public class EmailRepository : IEmailRepository
    {
        #region Private Variables
        private readonly EmailConfigurationModel _emailConfig;
        #endregion

        #region Constructor
        public EmailRepository(EmailConfigurationModel emailConfig)
        {
            _emailConfig = emailConfig;
        }
        #endregion
        public async Task<bool> SendEmail(MessageModel message)
        {
            var emailMessage = CreateEmailMessage(message);
            return Send(emailMessage);

        }
        #region Private Methods
        private MimeMessage CreateEmailMessage(MessageModel message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("SidPortfolio", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            return emailMessage;
        }

        private bool Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    var clientResponse = client.Send(mailMessage);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
                finally
                {

                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
        #endregion
    }
}
