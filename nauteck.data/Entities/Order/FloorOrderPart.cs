namespace nauteck.data.Entities.Order;

public sealed class FloorOrderPart
{
    public Guid Id { get; set; }
    public string Floor { get; set; } = "";
    public decimal FloorPrice { get; set; }
    public decimal FloorQuantity { get; set; }
    public string FloorColorAbove { get; set; } = "";
    public string FloorColorBeneath { get; set; } = "";
    public decimal FloorTotal { get; set; }
    public string Design { get; set; } = "";
    public decimal DesignPrice { get; set; }
    public decimal DesignTotal { get; set; }
    public string Measurement { get; set; } = "";
    public decimal MeasurementPrice { get; set; }
    public decimal MeasurementTotal { get; set; }
    public string Construction { get; set; } = "";
    public decimal ConstructionPrice { get; set; }
    public decimal ConstructionTotal { get; set; }
    public decimal CallOutCostPrice { get; set; }
    public decimal CallOutCostTotal { get; set; }
    public decimal CallOutCostQuantity { get; set; } = 0.00m;
    public decimal ColorPrice { get; set; } = 0.00m;
    public decimal ColorTotal { get; set; } = 0.00m;
    public decimal LogoTotal { get; set; } = 0.00m;
    public bool FloorColorExclusive { get; set; } = false;
    public Guid FloorOrderId { get; set; }
}
