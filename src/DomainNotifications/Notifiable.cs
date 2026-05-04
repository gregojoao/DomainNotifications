using DomainNotifications.Contracts;
using DomainNotifications.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DomainNotifications
{
    public abstract class Notifiable : INotifiable
    {
        public IReadOnlyCollection<Notification> Notifications { get => _notifications.ToArray(); }
        private readonly List<Notification> _notifications = new();

        public bool IsInvalid { get => Notifications.Any(); }
        public bool IsValid { get => !IsInvalid; }

        protected void AddNotification(Notification? notification)
        {
            if (notification != null)
                _notifications.Add(notification);
        }

        protected void AddNotifications(IEnumerable<Notification>? notifications)
        {
            if (notifications == null) return;
            
            foreach (var notification in notifications)
                AddNotification(notification);
        }

        public string NotificationsMessage() =>
            string.Join("; ", Notifications.Select(x => x.Property + ": " + x.Message)) + ";";

        public void Clear() =>
            _notifications.Clear();
    }
}