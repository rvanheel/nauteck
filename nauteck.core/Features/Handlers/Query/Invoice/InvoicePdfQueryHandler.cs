using nauteck.core.Implementation;
using nauteck.data.Dto.Invoice;

using static nauteck.core.Features.Queries.Invoice;

namespace nauteck.core.Features.Handlers.Query.Invoice;

public sealed class InvoicePdfQueryHandler(IDapperContext dapperContext) : IRequestHandler<InvoicePdfQuery, (BinaryData, string)>
{
    public async Task<(BinaryData, string)> Handle(InvoicePdfQuery request, CancellationToken cancellationToken)
    {
        var invoice = GetInvoice(request.Id);
        var invoiceLines = GetInvoiceLines(request.Id);
        await Task.WhenAll(invoice, invoiceLines);
        var client = await GetClient(invoice.Result.ClientId);

        
        //var binaryData = InvoiceBuilder.BuildInvoice(client, invoice.Result, invoiceLines.Result.ToArray());

        //return (binaryData, $"Offerte {invoice.Result.Reference}.pdf");
        return (BinaryData.Empty, "");
    }

    #region Private Methods
    private Task<ClientDto> GetClient(Guid id)
    {
        return dapperContext.Connection.QueryFirstAsync<ClientDto>($"SELECT * FROM {DbConstants.Tables.Client.TableName} WHERE {DbConstants.Tables.Client.Columns.Id} = @Id", new { Id = id });
    }
    private Task<InvoiceDto> GetInvoice(Guid id)
    {
        return dapperContext.Connection.QueryFirstAsync<InvoiceDto>($"SELECT * FROM {DbConstants.Tables.Invoice.TableName} WHERE {DbConstants.Tables.Invoice.Columns.Id} = @Id", new { Id = id });
    }
    private Task<IEnumerable<InvoiceLineDto>> GetInvoiceLines(Guid id)
    {
        return dapperContext.Connection.QueryAsync<InvoiceLineDto>($"SELECT * FROM {DbConstants.Tables.InvoiceLine.TableName} WHERE {DbConstants.Tables.InvoiceLine.Columns.InvoiceId} = @Id", new { Id = id });
    }
    #endregion
}
