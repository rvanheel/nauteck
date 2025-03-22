namespace nauteck.data.Entities.Agenda;

public sealed class Agenda
{
    public Guid Id { get; init; }

    public DateTime Date { get; init; }

    public string Status { get; init; } = "";

    public string? Comments { get; init; }

    public DateTime CreatedAt { get; init; }

    public string CreatedBy { get; init; } = "";

    public Guid ClientId { get; init; }
}