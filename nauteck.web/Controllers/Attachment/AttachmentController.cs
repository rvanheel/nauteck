using Microsoft.AspNetCore.Mvc;

using nauteck.core.Abstraction;
using nauteck.data.Models.Attachment;

using static nauteck.core.Features.Attachment.Commands;

namespace nauteck.web.Controllers.Attachment;

public sealed class AttachmentController(IMediator mediator, IBlobStorage blobStorage) : BaseController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AttachmentPostModel attachmentPostModel, CancellationToken cancellationToken)
    {
        await Mediator.Send(new AddAttachmentCommand(attachmentPostModel, DisplayName), cancellationToken);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteAttachmentCommand(id), cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetSasBlobUrl([FromQuery(Name = "extension")] string extension, CancellationToken cancellationToken)
    {
        var url = await blobStorage.BuildSasUrl(Path.ChangeExtension(Guid.NewGuid().ToString(), extension), 15, cancellationToken);
        return Ok(url);
    }


}


