using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;

namespace nauteck.core.Features.Order;

public sealed class GetInvoiceNumberQueryHandler(IDbConnection dbConnection) : IRequestHandler<Queries.GetInvoiceNumberQuery, string>
{
    public async Task<string> Handle(Queries.GetInvoiceNumberQuery request, CancellationToken cancellationToken)
    {
        var query = $"SELECT CONCAT(extract(YEAR FROM current_date()), LPAD(extract(MONTH FROM current_date()), 2, '0'), LPAD(COUNT({DbConstants.Columns.Id})+1, 3, '0')) AS {DbConstants.Columns.Id} FROM {DbConstants.Tables.FloorOrder} WHERE extract(YEAR FROM {DbConstants.Columns.CreatedAt}) = extract(YEAR FROM current_date()) AND extract(MONTH FROM {DbConstants.Columns.CreatedAt}) = extract(MONTH FROM current_date())";
        var result = await dbConnection.QueryFirstAsync<string>(query);
        return result;
    }
}