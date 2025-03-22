using MediatR;
using Microsoft.AspNetCore.Mvc;
using nauteck.web.Models.Agenda;

namespace nauteck.web.Controllers.Agenda;

public class AgendaController(IMediator mediator) : BaseController(mediator)
{
    // GET
    public async Task<IActionResult> Index(DateTime? date, CancellationToken cancellationToken)
    {
        var today = DateTime.Now;
        var startDate = date ?? new DateTime(today.Year, today.Month, 1);
        var model = new AgendaViewModel(startDate, startDate.AddMonths(1));
        
        return View(model);
    }
}