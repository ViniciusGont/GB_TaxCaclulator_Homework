
namespace Notifications
{
    public interface INotifier
    {
        bool HasNotifications();

        IList<Notification> GetNotifications();

        void AddNotification(Notification notification);
        void AddNotifications(IEnumerable<Notification> notifications);

    }
}
