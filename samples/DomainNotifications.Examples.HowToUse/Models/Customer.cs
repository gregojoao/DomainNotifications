using DomainNotifications.Entities;

namespace DomainNotifications.Examples.HowToUse.Models
{
    class Customer : Notifiable
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public Customer() { }

        public Customer(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                AddNotification(new Notification("Name", "Invalid name"));
            if (string.IsNullOrEmpty(Surname))
                AddNotification(new Notification("Surname", "Invalid surname"));
        }
    }
}