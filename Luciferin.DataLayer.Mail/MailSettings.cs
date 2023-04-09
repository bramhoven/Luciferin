namespace Luciferin.DataLayer.Mail;

public class MailSettings
{
    public string FromEmail { get; set; }
    
    public string NotificationEmail { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public bool EnableSsl { get; set; }
}