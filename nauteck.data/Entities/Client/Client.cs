
using System.ComponentModel.DataAnnotations;

namespace nauteck.data.Entities.Client;

public sealed class Client
{
    public Guid Id { get; set; }

    [Display(Name= "Aanhef")]
    public string? Preamble { get; init; }

    [Display(Name ="Achternaam")]
    public string LastName { get; set; } = "";

    [Display(Name = "Voornaam")]
    public string? FirstName { get; set; }

    [Display(Name = "Tussenvoegsel")]
    public string? Infix { get; set; }

    [Display(Name = "Telefoon")]
    public string? Phone { get; set; }

    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Display(Name = "Straat")]
    public string Address { get; set; } = "";

    [Display(Name = "Huisnummer")]
    public string Number { get; set; } = "";

    [Display(Name = "Toevoeging")]
    public string? Extension { get; set; }

    [Display(Name = "Postcode")]
    public string Zipcode { get; set; } = "";

    [Display(Name = "Plaats")]
    public string City { get; set; } = "";

    [Display(Name = "Provincie")]
    public string? Region { get; set; } = "";

    [Display(Name = "Land")]
    public string Country { get; set; } = "";

    [Display(Name = "Boot Merk")]
    public string BoatBrand { get; set; } = "";

    [Display(Name = "Boot Type")]
    public string BoatType { get; set; } = "";

    [Display(Name = "Opmerkingen")]
    public string? Remarks { get; set; }

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "";
    public DateTime ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = "";
    public bool Active { get; set; }
}