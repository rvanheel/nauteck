using System.Runtime.InteropServices;

namespace nauteck.core.Implementation;

public sealed class Helper : IHelper
{
    private readonly TimeZoneInfo _tzi = TimeZoneInfo.FindSystemTimeZoneById(
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? Constants.TimeZone.CentralEuropeanStandardTime
            : Constants.TimeZone.EuropeAmsterdam);

    public DateTime AtCurrentTimeZone => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _tzi);

    public string GetFullName(string? firstName, string? infix, string? lastName)
    {
        var info = CultureInfo.CurrentCulture.TextInfo;
        return $"{info.ToTitleCase($"{firstName}")} {info.ToLower($"{infix}")} {info.ToTitleCase($"{lastName}")}".Replace("  ", " ").Trim();
    }
    #region BCrypt
    public string? GenerateHashedPassword(string? password)
    {
        return string.IsNullOrWhiteSpace(password) ? password : BCrypt.Net.BCrypt.HashPassword(password);
    }
    public bool VerifyPassword(string? password, string? hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
    #endregion
}