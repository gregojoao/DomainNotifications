# Domain Notifications

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![NuGet](https://img.shields.io/nuget/v/DomainNotifications.svg)](https://www.nuget.org/packages/DomainNotifications/)
[![License](https://img.shields.io/github/license/grecojoao/DomainNotifications)](LICENSE)

A lightweight and elegant implementation of the Domain Notification design pattern for .NET applications. Simplify validation and error handling in your domain layer with a clean, fluent API.

## 🚀 Features

- ✅ **Simple & Intuitive** - Easy-to-use API for collecting domain validation errors
- ✅ **Type-Safe** - Built with nullable reference types for enhanced null safety
- ✅ **Lightweight** - Zero external dependencies
- ✅ **Well-Tested** - Comprehensive unit test coverage
- ✅ **Modern .NET** - Built for .NET 10 with latest C# features
- ✅ **Production-Ready** - Used in real-world applications

## 📦 Installation

Install via NuGet Package Manager:

```bash
dotnet add package DomainNotifications
```

Or via Package Manager Console:

```powershell
Install-Package DomainNotifications
```

## 🎯 Quick Start

### 1. Inherit from Notifiable

Create your domain entities by inheriting from the `Notifiable` base class:

```csharp
using DomainNotifications;
using DomainNotifications.Entities;

public class Customer : Notifiable
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int Age { get; set; }

    public void Validate()
    {
        if (string.IsNullOrEmpty(Name))
            AddNotification(new Notification("Name", "Name is required"));

        if (string.IsNullOrEmpty(Email))
            AddNotification(new Notification("Email", "Email is required"));
        else if (!Email.Contains("@"))
            AddNotification(new Notification("Email", "Invalid email format"));

        if (Age < 18)
            AddNotification(new Notification("Age", "Must be 18 or older"));
    }
}
```

### 2. Validate and Check Results

```csharp
var customer = new Customer 
{ 
    Name = "", 
    Email = "invalid-email",
    Age = 15
};

customer.Validate();

if (customer.IsInvalid)
{
    // Get all notifications
    foreach (var notification in customer.Notifications)
    {
        Console.WriteLine($"{notification.Property}: {notification.Message}");
    }

    // Or get formatted message
    Console.WriteLine(customer.NotificationsMessage());
    // Output: Name: Name is required; Email: Invalid email format; Age: Must be 18 or older;
}
```

### 3. Use in Your Application

```csharp
public class CustomerService
{
    public Result CreateCustomer(CustomerDto dto)
    {
        var customer = new Customer
        {
            Name = dto.Name,
            Email = dto.Email,
            Age = dto.Age
        };

        customer.Validate();

        if (customer.IsInvalid)
        {
            return Result.Fail(customer.NotificationsMessage());
        }

        // Save customer to database
        _repository.Add(customer);
        
        return Result.Success();
    }
}
```

## 📚 API Reference

### Notifiable Class

Base class for domain entities that need validation.

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Notifications` | `IReadOnlyCollection<Notification>` | Collection of all validation notifications |
| `IsValid` | `bool` | Returns `true` if there are no notifications |
| `IsInvalid` | `bool` | Returns `true` if there are any notifications |

#### Methods

| Method | Description |
|--------|-------------|
| `AddNotification(Notification notification)` | Adds a single notification |
| `AddNotifications(IEnumerable<Notification> notifications)` | Adds multiple notifications |
| `NotificationsMessage()` | Returns formatted string with all notifications |
| `Clear()` | Removes all notifications |

### Notification Class

Represents a single validation error.

#### Constructor

```csharp
public Notification(string property, string message)
```

#### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Property` | `string` | Name of the property that failed validation |
| `Message` | `string` | Validation error message |

## 🎨 Advanced Usage

### Multiple Validations

```csharp
public class Order : Notifiable
{
    public Customer Customer { get; set; }
    public List<OrderItem> Items { get; set; }

    public void Validate()
    {
        // Validate customer
        Customer.Validate();
        AddNotifications(Customer.Notifications);

        // Validate items
        if (Items == null || !Items.Any())
            AddNotification(new Notification("Items", "Order must have at least one item"));
        else
        {
            foreach (var item in Items)
            {
                item.Validate();
                AddNotifications(item.Notifications);
            }
        }
    }
}
```

### Custom Validation Rules

```csharp
public class Product : Notifiable
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public void Validate()
    {
        ValidateName();
        ValidatePrice();
    }

    private void ValidateName()
    {
        if (string.IsNullOrWhiteSpace(Name))
            AddNotification(new Notification(nameof(Name), "Product name is required"));
        else if (Name.Length < 3)
            AddNotification(new Notification(nameof(Name), "Product name must be at least 3 characters"));
        else if (Name.Length > 100)
            AddNotification(new Notification(nameof(Name), "Product name cannot exceed 100 characters"));
    }

    private void ValidatePrice()
    {
        if (Price <= 0)
            AddNotification(new Notification(nameof(Price), "Price must be greater than zero"));
        else if (Price > 1000000)
            AddNotification(new Notification(nameof(Price), "Price cannot exceed 1,000,000"));
    }
}
```

### Integration with ASP.NET Core

```csharp
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateCustomerRequest request)
    {
        var customer = new Customer
        {
            Name = request.Name,
            Email = request.Email,
            Age = request.Age
        };

        customer.Validate();

        if (customer.IsInvalid)
        {
            return BadRequest(new 
            { 
                errors = customer.Notifications.Select(n => new 
                { 
                    field = n.Property, 
                    message = n.Message 
                })
            });
        }

        _customerService.Create(customer);
        return Ok(customer);
    }
}
```

## 🧪 Testing

The library includes comprehensive unit tests. Run them with:

```bash
dotnet test
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**João Greco** - [Greco Labs](https://github.com/grecojoao)

## 🌟 Show Your Support

If this project helped you, please give it a ⭐️!

## 📝 Changelog

### Version 3.0.0
- Updated to .NET 10
- Enhanced null safety with nullable reference types
- Updated all dependencies to latest versions
- Improved performance and modern C# features
- Added comprehensive unit tests

### Version 1.0.2
- Added property in notification return message

### Version 1.0.0
- Initial release
