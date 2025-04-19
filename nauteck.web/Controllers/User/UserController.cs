using Microsoft.AspNetCore.Mvc;

using nauteck.core.Features.User;
using nauteck.data.Models.User;

namespace nauteck.web.Controllers.User;

public sealed class UserController(IMediator Mediator) : BaseController(Mediator)
{
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new Commands.DeleteUserCommand(id), cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var user = await Mediator.Send(new Queries.UserByIdQuery(id, Guid.Parse(DealerId)), cancellationToken);
        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var users = await Mediator.Send(new Queries.UserQuery(), cancellationToken);
        return View(users);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveOrUpdate(UserPostModel user, CancellationToken cancellationToken)
    {
        await Mediator.Send(new Commands.SaveOrUpdateUserCommand(user, DisplayName, Guid.Parse(DealerId)), cancellationToken);
        return RedirectToAction("Index");
    }
}
