using System;

namespace DomainNotifications.Entities
{
    public sealed class Notification
    {
        public string Property { get; private set; }
        public string Message { get; private set; }

        public Notification(string property, string message)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}