using System.Collections.Generic;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Nordigen.Models;

namespace Luciferin.BusinessLayer.Notifications;

public interface IExpirationNotificationComponent
{
    /// <summary>
    ///     Sends an notification about requisitions that will be expiring.
    /// </summary>
    /// <param name="requisitions">List of requisitions that are expired.</param>
    /// <returns></returns>
    Task SendExpiredNotification(ICollection<Requisition> requisitions);

    /// <summary>
    ///     Send an notification about requisitions that will be expiring.
    /// </summary>
    /// <param name="requisitions">List of requisitions that will be expiring soon.</param>
    /// <returns></returns>
    Task SendExpiringNotification(ICollection<Requisition> requisitions);
}