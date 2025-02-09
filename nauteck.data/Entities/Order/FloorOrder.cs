namespace nauteck.data.Entities.Order;

public sealed class FloorOrder
{
    #region Consumer
    public string Id { get; set; } = "";
    public string Preamble { get; set; } = "";
    public string LastName { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string Infix { get; set; } = "";

    public string Address { get; set; } = "";
    public string Number { get; set; } = "";
    public string Extension { get; set; } = "";
    public string Zipcode { get; set; } = "";
    public string City { get; set; } = "";
    public string Region { get; set; } = "";
    public string Country { get; set; } = "";

    #endregion

    #region Boat
    public string BoatBrand { get; set; } = "";
    public string BoatType { get; set; } = "";
    #endregion

    #region Invoice

    public decimal Provision { get; set; }
    public string Reference { get; set; } = "";
    public string Status { get; set; } = "";
    public decimal Total { get; set; }
    #endregion

    /*
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "";
    public DateTime ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = "";
    public bool Active { get; set; }
    public decimal Discount { get; set; }
    public string StatusAction { get; set; } = "";
    public string Comment { get; set; } = "";
    public string FreeText { get; set; } = "";
    public Guid? DealerId { get; set; }
    public decimal FreePrice { get; set; }
    public string FreePriceText { get; set; } = "";
    public decimal ProvisionPercentage { get; set; }
    public string ConstructionBy { get; set; } = "";
    public string InvoiceFreeText { get; set; } = "";
    public string CompanyName { get; set; } = "";
    public string VatNumber { get; set; } = "";
    */
}
