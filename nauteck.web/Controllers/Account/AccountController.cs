using MediatR;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using nauteck.core.Features.Account;
using nauteck.web.Models.Account;

namespace nauteck.web.Controllers.Account;

public sealed class AccountController(IMediator mediator) : Controller
{
    [AllowAnonymous]
    public IActionResult AccessDenied() => View();

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string message = "")
    {
        ViewData["message"] = "";
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] AccountViewModel accountViewModel, CancellationToken cancellationToken)
    {
        var query = new SignInQuery(CookieAuthenticationDefaults.AuthenticationScheme, accountViewModel.Password, accountViewModel.User);

        var principal = await mediator.Send(query, cancellationToken);
        if (principal is null)
        {
            ViewData["message"] = "Ongeldige gebruikersnaam of code.";
            return View();
        }
        var dt = DateTimeOffset.UtcNow;
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
        {
            AllowRefresh = true,
            ExpiresUtc = dt.AddDays(1),
            IsPersistent = true,
            IssuedUtc = dt
        });

        return LocalRedirect("/");
    }


    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }
}
