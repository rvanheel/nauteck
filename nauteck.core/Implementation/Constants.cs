using System.Globalization;

namespace nauteck.core.Implementation;

public static class Constants
{
    public static readonly CultureInfo DutchCulture = new("nl-NL");
    public static class TimeZone
    {
        public const string EuropeAmsterdam = "Europe/Amsterdam";
        public const string CentralEuropeanStandardTime = "Central Europe Standard Time";
    }

    public static class Status
    {
        public const string CANCELLED = "Geannuleerd";
        public const string DEFINITIVE_DESIGN = "Definitief design";
        public const string DEFINITIVE_ORDER = "Definitieve opdracht";
        public const string FINISHED = "Afgerond";
        public const string IN_PRODUCTION = "In produktie";
        public const string MONTAGE_PLANNED = "Montage gepland";
        public const string MEASUREMENT_PLANNED = "Inmeten gepland";
        public const string PAID = "Betaald";
        public const string PROFORMA = "Proforma opdracht";

        public static readonly string[] ALL = [PROFORMA, DEFINITIVE_ORDER, PAID, MEASUREMENT_PLANNED, DEFINITIVE_DESIGN, MONTAGE_PLANNED, FINISHED, IN_PRODUCTION, CANCELLED];
    }
}
