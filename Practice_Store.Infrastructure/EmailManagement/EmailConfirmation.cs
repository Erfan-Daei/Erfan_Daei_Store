using Practice_Store.Common;
using System.Net;
using System.Net.Mail;

public class EmailConfirmation
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    public EmailConfirmation(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
    {
        this._smtpServer = smtpServer;
        this._smtpPort = smtpPort;
        this._smtpUser = smtpUser;
        this._smtpPass = smtpPass;
    }
    public int Create4DigitCode()
    {
        var random = new Random();
        int code = random.Next(1000, 10000);
        return code;
    }

    public ResultDto Send4DigitCode(int Code, string RecipientEmail)
    {
        try
        {
            using (var Client = new SmtpClient(_smtpServer, _smtpPort))
            {
                Client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                Client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpUser),
                    Subject = "Your Email Confirmation Code",
                    Body = $"Your Email Confirmation Code Is: {Code:D4}",
                    IsBodyHtml = false
                };

                mailMessage.To.Add(RecipientEmail);

                Client.Send(mailMessage);
                return new ResultDto()
                {
                    IsSuccess = true,
                };
            }
        }
        catch (Exception)
        {
            return new ResultDto()
            {
                IsSuccess = false,
            };
        }
    }
}
