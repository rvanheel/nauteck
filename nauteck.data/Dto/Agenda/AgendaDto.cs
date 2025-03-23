using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
namespace nauteck.data.Dto.Agenda;

public sealed record AgendaDto
{
    public Guid Id { get; init; }
    public Guid ClientId { get; init; }
    
    [Display(Name = "Opmerkingen")]
    public string? Comments { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? CreatedBy { get; init; }
    
    [Display(Name = "Datum")]
    public DateTime Date { get; init; }
    public string? Status { get; init; }
    
    [Display(Name = "Titel")]
    public string? Title { get; init; }
    public string? Preamble { get; init; }
    public string? LastName { get; init; }
    public string? FirstName { get; init; }
    public string? Infix { get; init; }
    public string? Address { get; init; }
    public string? Number { get; init; }
    public string? Extension { get; init; }
    public string? Zipcode { get; init; }
    public string? City { get; init; }
    public string? Region { get; init; }
    public string? Country { get; init; }
}
