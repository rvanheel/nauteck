using nauteck.data.Dto.Invoice;

using static nauteck.core.Features.Commands.Invoice;
using static nauteck.core.Implementation.DbConstants;

namespace nauteck.core.Features.Handlers.Command.Invoice;

public sealed class SaveOrUpdateInvoiceLineCommandHandler(IDapperContext dapperContext) : IRequestHandler<InvoiceLineSaveOrUpdateCommand>
{
    public async Task Handle(InvoiceLineSaveOrUpdateCommand request, CancellationToken cancellationToken)
    {
        await Insert(request.InvoiceLine);
        await Update(request.InvoiceLine);
    }
    #region Private Methods
    private Task Insert(InvoiceLineDto invoiceLineDto)
    {
        if (!invoiceLineDto.Id.Equals(Guid.Empty)) return Task.CompletedTask;
        var sql = $@"
        INSERT INTO {Tables.InvoiceLine.TableName} (
            {Tables.InvoiceLine.Columns.Id}
            , {Tables.InvoiceLine.Columns.Amount}
            , {Tables.InvoiceLine.Columns.Description}
            , {Tables.InvoiceLine.Columns.Tax}
            , {Tables.InvoiceLine.Columns.Quantity}
            , {Tables.InvoiceLine.Columns.InvoiceId}
        )
        VALUES (
        UUID()
        , @{nameof(InvoiceLineDto.Amount)}
        , @{nameof(InvoiceLineDto.Description)}
        , @{nameof(InvoiceLineDto.Tax)}
        , @{nameof(InvoiceLineDto.Quantity)}
        , @{nameof(InvoiceLineDto.InvoiceId)}
        ); 
        {Sql.UpdateInvoice(nameof(invoiceLineDto.InvoiceId))}
       ";
        return dapperContext.Connection.ExecuteAsync(sql, invoiceLineDto);
    }
    private Task Update(InvoiceLineDto invoiceLineDto)
    {
        if (invoiceLineDto.Id.Equals(Guid.Empty)) return Task.CompletedTask;
        var sql = $@"
        UPDATE {Tables.InvoiceLine.TableName} 
        SET 
            {Tables.InvoiceLine.Columns.Amount} = @{nameof(InvoiceLineDto.Amount)}
            , {Tables.InvoiceLine.Columns.Description} = @{nameof(InvoiceLineDto.Description)}
            , {Tables.InvoiceLine.Columns.Tax} = @{nameof(InvoiceLineDto.Tax)}
            , {Tables.InvoiceLine.Columns.Quantity} = @{nameof(InvoiceLineDto.Quantity)}
        WHERE {Tables.InvoiceLine.Columns.Id} = @{nameof(InvoiceLineDto.Id)};
        {Sql.UpdateInvoice(nameof(invoiceLineDto.InvoiceId))}
        ";
        return dapperContext.Connection.ExecuteAsync(sql, invoiceLineDto);
    }
    #endregion

}
