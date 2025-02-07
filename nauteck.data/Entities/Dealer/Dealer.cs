using nauteck.data.Entities.Account;

namespace nauteck.data.Entities.Dealer;

public sealed class Dealer
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string Zipcode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Provision { get; set; }
    public bool FloorQuantityNotRequired { get; set; }

    // Navigation Properties
    public ICollection<User> Users { get; set; } = [];
}

