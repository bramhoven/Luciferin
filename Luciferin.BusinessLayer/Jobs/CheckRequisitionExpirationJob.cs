using System.Linq;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Import;
using Luciferin.BusinessLayer.Notifications;
using Quartz;

namespace Luciferin.BusinessLayer.Jobs;

public class CheckRequisitionExpirationJob : IJob
{
    private readonly IExpirationNotificationComponent _expirationNotificationComponent;
    private readonly IImportManager _importManager;

    public CheckRequisitionExpirationJob(IImportManager importManager,
        IExpirationNotificationComponent expirationNotificationComponent)
    {
        _importManager = importManager;
        _expirationNotificationComponent = expirationNotificationComponent;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var expiringRequisitions = await _importManager.GetExpiringRequisition();
        if (expiringRequisitions.Any())
            await _expirationNotificationComponent.SendExpiringNotification(expiringRequisitions);

        var expiredRequisitions = await _importManager.GetExpiredRequisition();
        if (expiredRequisitions.Any())
            await _expirationNotificationComponent.SendExpiredNotification(expiredRequisitions);
    }
}