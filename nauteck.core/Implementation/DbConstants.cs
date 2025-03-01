namespace nauteck.core.Implementation;

public static class DbConstants
{
    public static class Columns
    {
        // general
        public const string Active = "`Active`";
        public const string DealerId = "`DealerId`";
        public const string Id = "`Id`";
        public const string CreatedAt = "`CreatedAt`";
        public const string CreatedBy = "`CreatedBy`";
        public const string ModifiedAt = "`ModifiedAt`";
        public const string ModifiedBy = "`ModifiedBy`";
        // order
        public const string ConstructionTotal = "`ConstructionTotal`";
        public const string FloorOrderId = "`FloorOrderId`";
        public const string Provision = "`Provision`";
        public const string ProvisionPercentage = "`ProvisionPercentage`";
        public const string Reference = "`Reference`";
        public const string Status = "`Status`";
        public const string StatusAction = "`StatusAction`";
        public const string Total = "`Total`";

        public const string CompanyName = "`CompanyName`";
        public const string VatNumber = "`VatNumber`";

        public const string ConstructionBy = "`ConstructionBy`";
        // personalia
        public const string FirstName = "`FirstName`";
        public const string Infix = "`Infix`";
        public const string LastName = "`LastName`";
        public const string Preamble = "`Preamble`";
        public const string Phone = "`Phone`";
        public const string Email = "`Email`";
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

        // logo
        public const string Description = "`Description`";
        public const string Price = "`Price`";
        public const string Quantity = "`Quantity`";

        // parts
        public const string Floor = "`Floor`";
        public const string FloorPrice = "`FloorPrice`";
        public const string FloorQuantity = "`FloorQuantity`";
        public const string FloorColorAbove = "`FloorColorAbove`";
        public const string FloorColorBeneath = "`FloorColorBeneath`";
        public const string FloorTotal = "`FloorTotal`";
        public const string Design = "`Design`";
        public const string DesignPrice = "`DesignPrice`";
        public const string DesignTotal = "`DesignTotal`";
        public const string Measurement = "`Measurement`";
        public const string MeasurementPrice = "`MeasurementPrice`";
        public const string MeasurementTotal = "`MeasurementTotal`";
        public const string Construction = "`Construction`";
        public const string ConstructionPrice = "`ConstructionPrice`";
        public const string CallOutCostPrice = "`CallOutCostPrice`";
        public const string CallOutCostTotal = "`CallOutCostTotal`";
        public const string CallOutCostQuantity = "`CallOutCostQuantity`";
        public const string ColorPrice = "`ColorPrice`";
        public const string ColorTotal = "`ColorTotal`";
        public const string LogoTotal = "`LogoTotal`";
        public const string FloorColorExclusive = "`FloorColorExclusive`";
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
        public const string FloorOrderLogo = "`floororderlogo`";
        public const string FloorOrderParts = "`floororderparts`";
        public const string FloorOrderStatus = "`floororderstatus`";
        public const string User = "`user`";
    }
}