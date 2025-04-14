using nauteck.core.Implementation;
using nauteck.data.Dto.Invoice;

namespace nauteck.core.Features.Handlers.Query.Invoice;

public sealed class InvoiceByIdQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.Invoice.InvoiceByIdQuery, InvoiceDto>
{
    public Task<InvoiceDto> Handle(Queries.Invoice.InvoiceByIdQuery request, CancellationToken cancellationToken)
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
           FROM {DbConstants.Tables.Invoice.TableName} q
           INNER JOIN {DbConstants.Tables.Client.TableName} c ON c.{DbConstants.Tables.Client.Columns.Id} = q.{DbConstants.Tables.Invoice.Columns.ClientId}
           WHERE q.{DbConstants.Tables.Invoice.Columns.Id} = @{nameof(request.Id)}
        """;
        return dapperContext.Connection.QueryFirstAsync<InvoiceDto>(sql, request);
    }
}
