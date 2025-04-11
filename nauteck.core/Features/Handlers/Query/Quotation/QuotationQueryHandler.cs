using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Dto.Quotation;

namespace nauteck.core.Features.Handlers.Query.Quotation;

public sealed class QuotationQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.Quotation.QuotationQuery, IEnumerable<QuotationDto>>
{
    public async Task<IEnumerable<QuotationDto>> Handle(Queries.Quotation.QuotationQuery request, CancellationToken cancellationToken)
    {
        const string sql = $"""
            SELECT 
                q.*
            , c.{DbConstants.Tables.Client.Columns.Preamble}
            , c.{DbConstants.Tables.Client.Columns.FirstName}
            , c.{DbConstants.Tables.Client.Columns.Infix}
            , c.{DbConstants.Tables.Client.Columns.LastName}
            , c.{DbConstants.Tables.Client.Columns.Address}
            , c.{DbConstants.Tables.Client.Columns.Number}
            , c.{DbConstants.Tables.Client.Columns.Extension}
            , c.{DbConstants.Tables.Client.Columns.Zipcode}
            , c.{DbConstants.Tables.Client.Columns.City}
            , c.{DbConstants.Tables.Client.Columns.Region}
            , c.{DbConstants.Tables.Client.Columns.Country}
            FROM {DbConstants.Tables.Quotation.TableName} q
            INNER JOIN {DbConstants.Tables.Client.TableName} c ON c.{DbConstants.Tables.Client.Columns.Id} = q.{DbConstants.Tables.Quotation.Columns.ClientId}
        """;
        return await dapperContext.Connection.QueryAsync<QuotationDto>(sql);
    }
}
