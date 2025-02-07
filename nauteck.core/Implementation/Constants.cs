using System.Globalization;

namespace nauteck.core.Implementation;

public static class Constants
{
    public static CultureInfo DutchCulture = new("nl-NL");
    public static class TimeZone
    {
        public const string EuropeAmsterdam = "Europe/Amsterdam";
        public const string CentralEuropeanStandardTime = "Central Europe Standard Time";
    }
}
