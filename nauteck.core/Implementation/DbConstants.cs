namespace nauteck.core.Implementation;

public static class DbConstants
{
    public static class Columns
    {
        // general
        public const string Id = "`Id`";
        public const string CreatedAt = "`CreatedAt`";
        public const string CreatedBy = "`CreatedBy`";
        // order
        public const string ConstructionTotal = "`ConstructionTotal`";
        public const string FloorOrderId = "`FloorOrderId`";
        public const string Provision = "`Provision`";
        public const string Reference = "`Reference`";
        public const string Status = "`Status`";
        public const string Total = "`Total`";

        public const string CompanyName = "`CompanyName`";
        public const string VatNumber = "`VatNumber`";
        // personalia
        public const string FirstName = "`FirstName`";
        public const string Infix = "`Infix`";
        public const string LastName = "`LastName`";
        public const string Preamble = "`Preamble`";
        // address
        public const string Address = "`Address`";
        public const string City = "`City`";
        public const string Country = "`Country`";
        public const string Extension = "`Extension`";
        public const string Number = "`Number`";
        public const string Region = "`Region`";
        public const string Zipcode = "`Zipcode`";
        // boat
        public const string BoatBrand = "`BoatBrand`";
        public const string BoatType = "`BoatType`";

        public const string Comment = "`Comment`";

        public const string Discount = "`Discount`";

        public const string InvoiceFreeText = "`InvoiceFreeText`";
        public const string FreeText = "`FreeText`";

        public const string FreePrice = "`FreePrice`";
        public const string FreePriceText = "`FreePriceText`";
    }
    public static class Tables
    {
        public const string Dealer = "`dealer`";
        public const string Floor = "`floor`";
        public const string FloorColor = "`floorcolor`";
        public const string FloorColorExclusive = "`floorcolorexclusive`";
        public const string FloorConstruction = "`floorconstruction`";
        public const string FloorDesign = "`floordesign`";
        public const string FloorLogo = "`floorlogo`";
        public const string FloorMeasurement = "`floormeasurement`";
        public const string FloorOrder = "`floororder`";
        public const string FloorOrderAttachment = "`floororderattachment`";
        public const string FloorOrderParts = "`floororderparts`";
        public const string FloorOrderStatus = "`floororderstatus`";
        public const string User = "`user`";
    }
}