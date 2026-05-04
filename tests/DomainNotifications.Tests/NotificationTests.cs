using System;
using DomainNotifications.Entities;
using FluentAssertions;
using Xunit;

namespace DomainNotifications.Tests
{
    public class NotificationTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateNotification()
        {
            // Arrange
            var property = "TestProperty";
            var message = "Test message";

            // Act
            var notification = new Notification(property, message);

            // Assert
            notification.Property.Should().Be(property);
            notification.Message.Should().Be(message);
        }

        [Fact]
        public void Constructor_WithNullProperty_ShouldThrowArgumentNullException()
        {
            // Arrange
            string? property = null;
            var message = "Test message";

            // Act
            Action act = () => new Notification(property!, message);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("property");
        }

        [Fact]
        public void Constructor_WithNullMessage_ShouldThrowArgumentNullException()
        {
            // Arrange
            var property = "TestProperty";
            string? message = null;

            // Act
            Action act = () => new Notification(property, message!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName("message");
        }

        [Theory]
        [InlineData("", "message")]
        [InlineData("property", "")]
        public void Constructor_WithEmptyStrings_ShouldCreateNotification(string property, string message)
        {
            // Act
            var notification = new Notification(property, message);

            // Assert
            notification.Property.Should().Be(property);
            notification.Message.Should().Be(message);
        }
    }
}
