using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Nordigen.Models;
using Luciferin.DataLayer.Mail;

namespace Luciferin.BusinessLayer.Notifications;

public class MailExpirationNotificationComponent : IExpirationNotificationComponent
{
    private readonly MailDal _mailDal;

    public MailExpirationNotificationComponent(MailDal mailDal)
    {
        _mailDal = mailDal;
    }

    /// <inheritdoc />
    public async Task SendExpiredNotification(ICollection<Requisition> requisitions)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("The following connections are expired:" + Environment.NewLine + Environment.NewLine);

        foreach (var requisition in requisitions)
            stringBuilder.Append(requisition.Reference + Environment.NewLine);

        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append("Please reconnect these accounts." + Environment.NewLine);
        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append("Thanks," + Environment.NewLine + "Luciferin");
        
        await _mailDal.SendEmail("Connections are expired", stringBuilder.ToString());
    }

    /// <inheritdoc />
    public async Task SendExpiringNotification(ICollection<Requisition> requisitions)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("The following connections are expiring:" + Environment.NewLine + Environment.NewLine);

        foreach (var requisition in requisitions)
            stringBuilder.Append(requisition.Reference + Environment.NewLine);

        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append("Please reconnect these accounts." + Environment.NewLine);
        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append("Thanks," + Environment.NewLine + "Luciferin");
        
        await _mailDal.SendEmail("Connections are expiring", stringBuilder.ToString());
    }
}