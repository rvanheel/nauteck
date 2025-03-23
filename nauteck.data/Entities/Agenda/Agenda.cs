using System.ComponentModel.DataAnnotations;

namespace nauteck.data.Entities.Agenda;

public sealed class Agenda
{
    public Guid Id { get; init; }

   public DateTime Date { get; init; }

    public string Status { get; init; } = "";

    public string Title { get; init; } = "";

    public string? Comments { get; init; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = "";

    public Guid ClientId { get; init; }
}