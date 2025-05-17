namespace TestTask.Unistrim.UserService.Models;

public class Customer
{
    
}

public readonly record struct CustomerId (Guid value)

public sealed class Email
{
    private readonly string _value;
}