using nauteck.core.Implementation;
using nauteck.data.Dto.Quotation;

using static nauteck.core.Features.Queries.Quotation;

namespace nauteck.core.Features.Handlers.Query.Quotation;

public sealed class QuotationPdfQueryHandler(IDapperContext dapperContext) : IRequestHandler<QuotationPdfQuery, (BinaryData, string)>
{
    public async Task<(BinaryData, string)> Handle(QuotationPdfQuery request, CancellationToken cancellationToken)
    {
        var quotation = GetQuotation(request.Id);
        var quotationLines = GetQuotationLines(request.Id);
        await Task.WhenAll(quotation, quotationLines);
        var client = await GetClient(quotation.Result.ClientId);

        
        var binaryData = QuotationBuilder.BuildQuotation(client, quotation.Result, quotationLines.Result.ToArray());

        return (binaryData, $"Offerte {quotation.Result.Reference}.pdf");
    }

    #region Private Methods
    private Task<ClientDto> GetClient(Guid id)
    {
        return dapperContext.Connection.QueryFirstAsync<ClientDto>($"SELECT * FROM {DbConstants.Tables.Client.TableName} WHERE {DbConstants.Tables.Client.Columns.Id} = @Id", new { Id = id });
    }
    private Task<QuotationDto> GetQuotation(Guid id)
    {
        return dapperContext.Connection.QueryFirstAsync<QuotationDto>($"SELECT * FROM {DbConstants.Tables.Quotation.TableName} WHERE {DbConstants.Tables.Quotation.Columns.Id} = @Id", new { Id = id });
    }
    private Task<IEnumerable<QuotationLineDto>> GetQuotationLines(Guid id)
    {
        return dapperContext.Connection.QueryAsync<QuotationLineDto>($"SELECT * FROM {DbConstants.Tables.QuotationLine.TableName} WHERE {DbConstants.Tables.QuotationLine.Columns.QuotationId} = @Id", new { Id = id });
    }
    #endregion
}
