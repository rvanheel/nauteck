using MediatR;

using Microsoft.AspNetCore.Mvc;

using static nauteck.core.Features.Attachment.Commands;

namespace nauteck.web.Controllers.Attachment;

public sealed class AttachmentController(IMediator mediator) : BaseController(mediator)
{
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid Id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteAttachmentCommand(Id), cancellationToken);
        return Ok();
    }
}
