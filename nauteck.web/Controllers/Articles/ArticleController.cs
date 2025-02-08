using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace nauteck.web.Controllers.Articles;

public class ArticleController(IMediator mediator) : BaseController(mediator)
{
    public IActionResult Index()
    {
        return View();
    }
}
