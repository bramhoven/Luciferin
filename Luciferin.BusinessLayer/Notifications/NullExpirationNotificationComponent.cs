using System.Collections.Generic;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Nordigen.Models;

namespace Luciferin.BusinessLayer.Notifications;

public class NullExpirationNotificationComponent : IExpirationNotificationComponent
{
    /// <inheritdoc />
    public Task SendExpiredNotification(ICollection<Requisition> requisitions)
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task SendExpiringNotification(ICollection<Requisition> requisitions)
    {
        return Task.CompletedTask;
    }
}