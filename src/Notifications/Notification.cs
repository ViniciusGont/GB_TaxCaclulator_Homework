namespace Notifications
{
    public class Notification
    {
        public Notification(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public string Code { get; }
        public string Description { get; }
    }
}