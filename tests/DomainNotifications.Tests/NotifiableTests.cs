using System;
using System.Collections.Generic;
using System.Linq;
using DomainNotifications.Entities;
using FluentAssertions;
using Xunit;

namespace DomainNotifications.Tests
{
    public class NotifiableTests
    {
        private class TestNotifiable : Notifiable
        {
            public void AddTestNotification(Notification? notification)
            {
                AddNotification(notification);
            }

            public void AddTestNotifications(IEnumerable<Notification>? notifications)
            {
                AddNotifications(notifications);
            }
        }

        [Fact]
        public void NewInstance_ShouldBeValid()
        {
            // Arrange & Act
            var notifiable = new TestNotifiable();

            // Assert
            notifiable.IsValid.Should().BeTrue();
            notifiable.IsInvalid.Should().BeFalse();
            notifiable.Notifications.Should().BeEmpty();
        }

        [Fact]
        public void AddNotification_WithValidNotification_ShouldAddToCollection()
        {
            // Arrange
            var notifiable = new TestNotifiable();
            var notification = new Notification("Property", "Message");

            // Act
            notifiable.AddTestNotification(notification);

            // Assert
            notifiable.IsValid.Should().BeFalse();
            notifiable.IsInvalid.Should().BeTrue();
            notifiable.Notifications.Should().HaveCount(1);
            notifiable.Notifications.First().Should().Be(notification);
        }

        [Fact]
        public void AddNotification_WithNull_ShouldNotAddToCollection()
        {
            // Arrange
            var notifiable = new TestNotifiable();

            // Act
            notifiable.AddTestNotification(null);

            // Assert
            notifiable.IsValid.Should().BeTrue();
            notifiable.Notifications.Should().BeEmpty();
        }

        [Fact]
        public void AddNotifications_WithMultipleNotifications_ShouldAddAllToCollection()
        {
            // Arrange
            var notifiable = new TestNotifiable();
            var notifications = new[]
            {
                new Notification("Property1", "Message1"),
                new Notification("Property2", "Message2"),
                new Notification("Property3", "Message3")
            };

            // Act
            notifiable.AddTestNotifications(notifications);

            // Assert
            notifiable.IsInvalid.Should().BeTrue();
            notifiable.Notifications.Should().HaveCount(3);
        }

        [Fact]
        public void AddNotifications_WithNull_ShouldNotThrow()
        {
            // Arrange
            var notifiable = new TestNotifiable();

            // Act
            Action act = () => notifiable.AddTestNotifications(null);

            // Assert
            act.Should().NotThrow();
            notifiable.IsValid.Should().BeTrue();
        }

        [Fact]
        public void AddNotifications_WithEmptyCollection_ShouldNotAddAny()
        {
            // Arrange
            var notifiable = new TestNotifiable();
            var notifications = Array.Empty<Notification>();

            // Act
            notifiable.AddTestNotifications(notifications);

            // Assert
            notifiable.IsValid.Should().BeTrue();
            notifiable.Notifications.Should().BeEmpty();
        }

        [Fact]
        public void NotificationsMessage_WithNoNotifications_ShouldReturnSemicolon()
        {
            // Arrange
            var notifiable = new TestNotifiable();

            // Act
            var message = notifiable.NotificationsMessage();

            // Assert
            message.Should().Be(";");
        }

        [Fact]
        public void NotificationsMessage_WithSingleNotification_ShouldReturnFormattedMessage()
        {
            // Arrange
            var notifiable = new TestNotifiable();
            notifiable.AddTestNotification(new Notification("Name", "Invalid name"));

            // Act
            var message = notifiable.NotificationsMessage();

            // Assert
            message.Should().Be("Name: Invalid name;");
        }

        [Fact]
        public void NotificationsMessage_WithMultipleNotifications_ShouldReturnFormattedMessage()
        {
            // Arrange
            var notifiable = new TestNotifiable();
            notifiable.AddTestNotification(new Notification("Name", "Invalid name"));
            notifiable.AddTestNotification(new Notification("Email", "Invalid email"));

            // Act
            var message = notifiable.NotificationsMessage();

            // Assert
            message.Should().Be("Name: Invalid name; Email: Invalid email;");
        }

        [Fact]
        public void Clear_WithNotifications_ShouldRemoveAll()
        {
            // Arrange
            var notifiable = new TestNotifiable();
            notifiable.AddTestNotification(new Notification("Property1", "Message1"));
            notifiable.AddTestNotification(new Notification("Property2", "Message2"));

            // Act
            notifiable.Clear();

            // Assert
            notifiable.IsValid.Should().BeTrue();
            notifiable.Notifications.Should().BeEmpty();
        }

        [Fact]
        public void Clear_WithNoNotifications_ShouldNotThrow()
        {
            // Arrange
            var notifiable = new TestNotifiable();

            // Act
            Action act = () => notifiable.Clear();

            // Assert
            act.Should().NotThrow();
            notifiable.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Notifications_ShouldBeReadOnly()
        {
            // Arrange
            var notifiable = new TestNotifiable();
            notifiable.AddTestNotification(new Notification("Property", "Message"));

            // Act
            var notifications = notifiable.Notifications;

            // Assert
            notifications.Should().BeAssignableTo<IReadOnlyCollection<Notification>>();
        }

        [Fact]
        public void IsValid_AfterAddingAndClearing_ShouldBeTrue()
        {
            // Arrange
            var notifiable = new TestNotifiable();
            notifiable.AddTestNotification(new Notification("Property", "Message"));

            // Act
            notifiable.Clear();

            // Assert
            notifiable.IsValid.Should().BeTrue();
            notifiable.IsInvalid.Should().BeFalse();
        }
    }
}
