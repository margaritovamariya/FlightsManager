using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public interface IMailService
    {

        Task SendEmailAsync(string ToEmail, string subject, string content);

    }

    /// <summary>
    /// Service for EmailSending
    /// </summary>
    public class SendGridMailService : IMailService
    {
        private IConfiguration _config;

        /// <summary>
        /// Sets Configuration
        /// </summary>
        /// <param name="configuration"></param>
        public SendGridMailService(IConfiguration configuration)
        {
            this._config = configuration;
        }

        /// <summary>
        /// Sends Email for confirmation
        /// </summary>
        /// <param name="ToEmail"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        public async Task SendEmailAsync(string ToEmail, string subject, string content)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("user_dani@abv.bg", "JWT Auth");
            var to = new EmailAddress(ToEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            await client.SendEmailAsync(msg);
        }
    }
}
