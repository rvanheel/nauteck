
using System.ComponentModel.DataAnnotations;

namespace nauteck.data.Dto.Quotation;

public sealed record QuotationDto
{
    public Guid Id { get; set; }
    public Guid ClientId { get; init; }

    [Display(Name ="Bedrag")]
    public decimal Amount { get; init; }

    [Display(Name ="Datum")]
    public DateTime Date { get; init; }

    [Display(Name = "Omschrijving")]
    public string? Description { get; init; }

    public string? Status { get; init; }

    [Display(Name = "Referentie")]
    public string? Reference { get; init; }

    // client
    [Display(Name ="Aanhef")]
    public string? Preamble { get; init; }

    [Display(Name = "Voornaam")]
    public string? FirstName { get; init; }

    [Display(Name = "Tussenvoegsel")]
    public string? Infix { get;init; }

    [Display(Name = "Naam")]
    public string? LastName { get; init; }

    [Display(Name = "Adres")]
    public string? Address { get; init; }
    
    [Display(Name = "Huisnummer")]
    public string? Number { get; init; }

    [Display(Name = "Toevoeging")]
    public string? Extension { get; init; }

    [Display(Name = "Postcode")]
    public string? Zipcode { get; init; }

    [Display(Name = "Plaats")]
    public string? City { get; init; }

    [Display(Name = "Provincie")]
    public string? Region { get; init; }

    [Display(Name = "Land")]
    public string? Country { get; init; }
}
