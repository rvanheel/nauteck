using nauteck.core.Implementation;
using nauteck.data.Dto.Invoice;

namespace nauteck.core.Features.Handlers.Query.Invoice;

public sealed class InvoiceLineQueryHandler(IDapperContext dapperContext): IRequestHandler<Queries.Invoice.InvoiceLineQuery, IEnumerable<InvoiceLineDto>>
{
    public Task<IEnumerable<InvoiceLineDto>> Handle(Queries.Invoice.InvoiceLineQuery request, CancellationToken cancellationToken)
    {
        const string sql = $"""
            SELECT 
                ql.*
            FROM {DbConstants.Tables.InvoiceLine.TableName} ql
            WHERE ql.{DbConstants.Tables.InvoiceLine.Columns.InvoiceId} = @{nameof(request.Id)}
        """;
        return dapperContext.Connection.QueryAsync<InvoiceLineDto>(sql, request);
    }
}