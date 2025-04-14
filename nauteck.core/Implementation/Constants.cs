namespace nauteck.core.Implementation;

public static class Constants
{
    public static readonly string[] Countries = [
    
        "België",
        "Denemarken",
        "Duitsland",
        "Engeland",
        "Finland",
        "Frankrijk",
        "Griekenland",
        "Ierland",
        "Italië",
        "Kroatië",
        "Luxemburg",
        "Malta",
        "Monaco", 
        "Nederland",
        "Noorwegen",
        "Polen",
        "Portugal",
        "Schotland",
        "Spanje",
        "Turkije",
        "Wales",
        "Zweden"
    ];
    public static readonly string[] Preamble = ["Dhr.", "Mevr.", "Geen"];

    public static readonly CultureInfo DutchCulture = new("nl-NL");
    public static class TimeZone
    {
        public const string EuropeAmsterdam = "Europe/Amsterdam";
        public const string CentralEuropeanStandardTime = "Central Europe Standard Time";
    }
    public static class QuotationStats
    {
        public const string Concept = "Concept";
        public const string Sent = "Verzonden";
        public const string Accepted = "Geaccepteerd";
        public const string Rejected = "Afgewezen";
        public const string Invoiced = "Gefactureerd";

        public static readonly string[] All = [
            Concept,
            Sent,
            Accepted,
            Rejected,
            Invoiced
        ];
    }
    public static class Status
    {
        public const string Cancelled = "Geannuleerd";
        public const string DefinitiveDesign = "Definitief design";
        public const string DefinitiveOrder = "Definitieve opdracht";
        public const string Finished = "Afgerond";
        public const string InProduction = "In produktie";
        public const string MontagePlanned = "Montage gepland";
        public const string MeasurementPlanned = "Inmeten gepland";
        public const string Paid = "Betaald";
        public const string Proforma = "Proforma opdracht";

        public static readonly string[] All =
        [
            Proforma,
            DefinitiveOrder,
            Paid,
            MeasurementPlanned,
            DefinitiveDesign,
            MontagePlanned,
            Finished,
            InProduction,
            Cancelled
        ];
    }
}
