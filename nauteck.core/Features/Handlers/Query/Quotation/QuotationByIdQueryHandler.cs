using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Dto.Quotation;

namespace nauteck.core.Features.Handlers.Query.Quotation;

public sealed class QuotationByIdQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.Quotation.QuotationByIdQuery, QuotationDto>
{
    public Task<QuotationDto> Handle(Queries.Quotation.QuotationByIdQuery request, CancellationToken cancellationToken)
    {
        var sql = @$"
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
        WHERE q.{DbConstants.Tables.Quotation.Columns.Id} = @{nameof(request.Id)}
        ";
        return dapperContext.Connection.QueryFirstAsync<QuotationDto>(sql, request);
    }
}
