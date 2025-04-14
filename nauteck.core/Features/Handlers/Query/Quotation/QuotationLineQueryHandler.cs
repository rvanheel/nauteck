using nauteck.core.Implementation;
using nauteck.data.Dto.Quotation;

namespace nauteck.core.Features.Handlers.Query.Quotation;

public sealed class QuotationLineQueryHandler(IDapperContext dapperContext): IRequestHandler<Queries.Quotation.QuotationLineQuery, IEnumerable<QuotationLineDto>>
{
    public Task<IEnumerable<QuotationLineDto>> Handle(Queries.Quotation.QuotationLineQuery request, CancellationToken cancellationToken)
    {
        const string sql = $"""
            SELECT 
                ql.*
            FROM {DbConstants.Tables.QuotationLine.TableName} ql
            WHERE ql.{DbConstants.Tables.QuotationLine.Columns.QuotationId} = @{nameof(request.Id)}
        """;
        return dapperContext.Connection.QueryAsync<QuotationLineDto>(sql, request);
    }
}