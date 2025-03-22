using MediatR;
using Microsoft.AspNetCore.Mvc;
using nauteck.core.Features.Agenda;
using nauteck.web.Models.Agenda;

namespace nauteck.web.Controllers.Agenda;

public class AgendaController(IMediator mediator) : BaseController(mediator)
{
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new Commands.DeleteAgendaCommand(id), cancellationToken);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(DateTime? date, CancellationToken cancellationToken)
    {
        var today = DateTime.Now;
        var startDate = date ?? new DateTime(today.Year, today.Month, 1);
        var agendaItems = await Mediator.Send(new Queries.AgendaQuery(), cancellationToken);
        var model = new AgendaViewModel(startDate, startDate.AddMonths(1), agendaItems);
        
        return View(model);
    }
}