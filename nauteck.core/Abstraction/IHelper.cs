namespace nauteck.core.Abstraction;

public interface IHelper
{
    DateTime AtCurrentTimeZone { get; }
    string GenerateHashedPassword(string password);
    string GetFullName(string? firstName, string? infix, string? lastName);
    bool VerifyPassword(string? password, string? hashedPassword);
}

