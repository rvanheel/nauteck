namespace nauteck.data.Dto.Order;

public sealed record FloorOrderDto
{
    #region Consumer
    public string Id { get; init; } = "";
    public string Preamble { get; init; } = "";
    public string LastName { get; init; } = "";
    public string FirstName { get; init; } = "";
    public string Infix { get; init; } = "";

    public string Address { get; init; } = "";
    public string Number { get; init; } = "";
    public string Extension { get; init; } = "";
    public string Zipcode { get; init; } = "";
    public string City { get; init; } = "";
    public string Region { get; init; } = "";
    public string Country { get; init; } = "";

    #endregion

    #region Boat
    public string BoatBrand { get; init; } = "";
    public string BoatType { get; init; } = "";
    #endregion

    #region Invoice
    public decimal ConstructionTotal { get; init; }
    public decimal Provision { get; init; }
    public string Reference { get; init; } = "";
    public string Status { get; init; } = "";
    public decimal Total { get; init; }
    #endregion

    public DateTime CreatedAt { get; init; }
}
