using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace Luciferin.DataLayer.Mail;

public class MailDal : IDisposable
{
    private readonly string _fromEmail;
    private readonly string _notificationEmail;
    private readonly SmtpClient _smtp;

    public MailDal(IOptionsSnapshot<MailSettings> options)
    {
        var mailSettings = options.Value;
        _fromEmail = mailSettings.FromEmail;
        _notificationEmail = mailSettings.NotificationEmail;
        _smtp = new SmtpClient
        {
            Host = mailSettings.Host,
            Port = mailSettings.Port,
            Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
            EnableSsl = mailSettings.EnableSsl
        };
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _smtp.Dispose();
    }

    /// <summary>
    ///     Sends a mail message.
    /// </summary>
    /// <param name="emailSubject">The email subject</param>
    /// <param name="emailMessage">The email message</param>
    public async Task SendEmail(string emailSubject, string emailMessage)
    {
        if (string.IsNullOrWhiteSpace(_notificationEmail))
            return;
        
        using var message = new MailMessage();
        message.From = new MailAddress(_fromEmail);
        message.To.Add(_notificationEmail);

        message.Subject = emailSubject;
        message.Body = emailMessage;

        await _smtp.SendMailAsync(message);
    }
}