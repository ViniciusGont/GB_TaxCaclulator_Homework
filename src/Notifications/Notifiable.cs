
using FluentValidation;
using System.Text;


namespace Notifications
{
    public abstract class Notifiable
    {
        private readonly List<Notification> _notifications = new();
        protected bool HasNotifications => _notifications.Any();

        public IEnumerable<Notification> GetNotifications()
        {
            return _notifications.GroupBy(x => x.Description).Select(g => g.First()); 
        }

        public abstract bool IsValid();

        public void AddNotification(string property, string description)
        {
            _notifications.Add(new Notification(property, description));
        }

        protected bool Validate(IValidator validator)
        {
            var context = new ValidationContext<object>(this);
            var result = validator.Validate(context);

            foreach (var item in result.Errors)
            {
                AddNotification(item.PropertyName, item.ErrorMessage);
            }

            return !HasNotifications;
        }
    }
}
