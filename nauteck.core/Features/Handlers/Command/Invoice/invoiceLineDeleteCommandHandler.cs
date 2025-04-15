using static nauteck.core.Features.Commands.Invoice;
using static nauteck.core.Implementation.DbConstants;

namespace nauteck.core.Features.Handlers.Command.Invoice;

public sealed class InvoiceLineDeleteCommandHandler(IDapperContext dapperContext) : IRequestHandler<InvoiceLineDeleteCommand, int>
{
    public Task<int> Handle(InvoiceLineDeleteCommand request, CancellationToken cancellationToken)
    {
        var sql = @$"DELETE FROM {Tables.InvoiceLine.TableName} WHERE {Tables.InvoiceLine.Columns.Id} = @{nameof(request.Id)};
            {Sql.UpdateInvoice(nameof(InvoiceLineDeleteCommand.InvoiceId))}";
        return dapperContext.Connection.ExecuteAsync(sql, request);
    }
}