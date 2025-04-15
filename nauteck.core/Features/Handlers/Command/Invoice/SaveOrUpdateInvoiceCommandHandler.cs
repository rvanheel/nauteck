using nauteck.data.Dto.Invoice;

using static nauteck.core.Features.Commands.Invoice;
using static nauteck.core.Implementation.DbConstants;

namespace nauteck.core.Features.Handlers.Command.Invoice;

public sealed class SaveOrUpdateInvoiceCommandHandler(IDapperContext dapperContext) : IRequestHandler<SaveOrUpdateInvoiceCommand, Guid>
{
    public async Task<Guid> Handle(SaveOrUpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var id = request.Invoice.Id.Equals(Guid.Empty) ? Guid.NewGuid() : request.Invoice.Id;
        await Insert(request.Invoice, id);
        await Update(request.Invoice);
        return request.Invoice.Id;
    }

    #region Private Methods
    private async Task Insert(InvoiceDto invoiceDto, Guid id)
    {
        if (!invoiceDto.Id.Equals(Guid.Empty)) return;
        invoiceDto.Id = id;
        const string sql = $"""
            SELECT
                @newNumber := 
                    CONCAT(extract(YEAR FROM current_date())
                    , LPAD(extract(MONTH FROM current_date()), 2, '0')
                    , LPAD(COUNT({Tables.Invoice.Columns.Id})+1, 3, '0'))
                FROM {Tables.Invoice.TableName}
                WHERE extract(YEAR FROM {Tables.Invoice.Columns.Date}) = extract(YEAR FROM current_date()) 
                AND extract(MONTH FROM {Tables.Invoice.Columns.Date}) = extract(MONTH FROM current_date());
    
            INSERT INTO {Tables.Invoice.TableName} (
                {Tables.Invoice.Columns.Id}
                , {Tables.Invoice.Columns.Amount}
                , {Tables.Invoice.Columns.ClientId}
                , {Tables.Invoice.Columns.Date}
                , {Tables.Invoice.Columns.Description}
                , {Tables.Invoice.Columns.Status}
                , {Tables.Invoice.Columns.Reference}
                )
            VALUES (
                @{nameof(InvoiceDto.Id)}
                , 0
                , @{nameof(InvoiceDto.ClientId)}
                , @{nameof(InvoiceDto.Date)}
                , @{nameof(InvoiceDto.Description)}
                , @{nameof(InvoiceDto.Status)}
                , @newNumber    
            );
        """;
        await dapperContext.Connection.ExecuteAsync(sql, invoiceDto);
    }
    private Task Update(InvoiceDto invoiceDto)
    {
        if (invoiceDto.Id.Equals(Guid.Empty)) return Task.CompletedTask;
        const string sql = $"""
            UPDATE {Tables.Invoice.TableName} 
            SET 
                {Tables.Invoice.Columns.Amount} = @{nameof(InvoiceDto.Amount)}
                , {Tables.Invoice.Columns.Description} = @{nameof(InvoiceDto.Description)}
                , {Tables.Invoice.Columns.Date} = @{nameof(InvoiceDto.Date)}
                , {Tables.Invoice.Columns.Status} = @{nameof(InvoiceDto.Status)}
            WHERE {Tables.Invoice.Columns.Id} = @{nameof(InvoiceDto.Id)}
        """;
        return dapperContext.Connection.ExecuteAsync(sql, invoiceDto);
    }
    #endregion
}
