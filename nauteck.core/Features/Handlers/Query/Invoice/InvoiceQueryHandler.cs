using System.Text;

using Dapper;

using nauteck.core.Implementation;
using nauteck.data.Dto.Invoice;

namespace nauteck.core.Features.Handlers.Query.Invoice;

public sealed class InvoiceQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.Invoice.InvoiceQuery, IEnumerable<InvoiceDto>>
{
    public async Task<IEnumerable<InvoiceDto>> Handle(Queries.Invoice.InvoiceQuery request, CancellationToken cancellationToken)
    {
        var queryBuilder = new StringBuilder($"""
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
        """);
        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            queryBuilder.AppendLine($"WHERE q.{DbConstants.Tables.Invoice.Columns.Status} = @Status");
        }
        return await dapperContext.Connection.QueryAsync<InvoiceDto>(queryBuilder.ToString(), request);
    }
}
