using nauteck.core.Implementation;

namespace nauteck.core.Features.Client.Handlers.Query;

public sealed class ClientByIdQuery(IDapperContext dapperContext) : IRequestHandler<Queries.ClientByIdQuery, data.Entities.Client.Client>
{
    public Task<data.Entities.Client.Client> Handle(Queries.ClientByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty) return Task.FromResult(new data.Entities.Client.Client { Active = true, Country = "Nederland" });
        return dapperContext.Connection.QueryFirstAsync<data.Entities.Client.Client>($"SELECT * FROM {DbConstants.Tables.Client.TableName} WHERE {DbConstants.Columns.Id} = @Id", new { request.Id });
    }
}
