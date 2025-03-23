using MediatR;
using Microsoft.AspNetCore.Mvc;

using nauteck.core.Abstraction;
using nauteck.core.Features.Agenda;
using nauteck.web.Models.Agenda;

namespace nauteck.web.Controllers.Agenda;

public class AgendaController(IMediator mediator, IHelper helper) : BaseController(mediator)
{
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new Commands.DeleteAgendaCommand(id), cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id, Guid clientId, CancellationToken cancellationToken)
    {
        var agendaItem = await Mediator.Send(new Queries.AgendaItemByIdForClientId(id, clientId), cancellationToken);
        return View(agendaItem);
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveOrUpdate(data.Entities.Agenda.Agenda agenda, CancellationToken cancellationToken)
    {
        agenda.CreatedAt = helper.AtCurrentTimeZone;
        agenda.CreatedBy = DisplayName;
        await Mediator.Send(new Commands.SaveOrUpdateAgendaCommand(agenda), cancellationToken);
        return RedirectToAction("Edit", "Client", new { Id = agenda.ClientId }, "nav-appointments");
    }
}