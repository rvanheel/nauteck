namespace nauteck.web.Models.Agenda;

public sealed record AgendaViewModel(DateTime StartDate, DateTime EndDate, IEnumerable<data.Entities.Agenda.Agenda> AgendaItems);