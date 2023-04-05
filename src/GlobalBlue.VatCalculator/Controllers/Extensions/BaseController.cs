using Microsoft.AspNetCore.Mvc;
using Notifications;

namespace GlobalBlue.TaxCalculator.Controllers.Extensions
{
    [Produces("application/json")]
    [ApiController]
    public class BaseController : Controller
    {
        protected readonly INotifier _notifier;

        protected BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected IActionResult NotFoundResult(string errorCode, string error)
        {
            return NotFound(new
            {
                code = errorCode,
                description = error
            });
        }

        protected bool IsValidOperation()
        {
            return !_notifier.HasNotifications();
        }

        protected IActionResult InvalidOperationResult()
        {
            var notifications = _notifier.GetNotifications();
            var notFoundNotification = notifications.FirstOrDefault(n => n.Code == NotificationMessages.NotFoundCode);
            var forbidNotification = notifications.FirstOrDefault(n => n.Code == NotificationMessages.Forbid);
            var conflictNotification = notifications.FirstOrDefault(n => n.Code == NotificationMessages.Conflict);

            if (notFoundNotification is not null)
                return NotFound(notFoundNotification);

            if (forbidNotification is not null)
                return Forbid();

            if (conflictNotification is not null)
                return Conflict();

            return UnprocessableEntity(notifications);
        }
    }
}
