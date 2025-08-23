using System.Net;
using System.Net.Mail;
using System.Text;

namespace Practice_Store.Common
{
    public class EmailSender
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailSender(string smtpHost, int smtpPort, string smtpUser, string smtpPass)
        {
            this._smtpHost = smtpHost;
            this._smtpPort = smtpPort;
            this._smtpUser = smtpUser;
            this._smtpPass = smtpPass;
        }
        public Task Execute(string UserEmail, string Body, string Subject)
        {
            SmtpClient client = new SmtpClient();
            client.Port = _smtpPort;
            client.Host = _smtpHost;
            client.EnableSsl = true;
            client.Timeout = 1000000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
            MailMessage message = new MailMessage(_smtpUser, UserEmail, Subject, Body);
            message.IsBodyHtml = true;
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            client.Send(message);
            return Task.CompletedTask;
        }
    }
}
