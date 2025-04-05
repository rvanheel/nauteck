using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Dto.Client;

namespace nauteck.core.Features.Handlers.Query.Client;

public sealed class ClientByIdQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.Client.ClientByIdQuery, ClientDto>
{
    public async Task<ClientDto> Handle(Queries.Client.ClientByIdQuery request, CancellationToken cancellationToken)
    {
        var sql = $"SELECT * FROM {DbConstants.Tables.Client.TableName} WHERE {DbConstants.Tables.Client.Columns.Id} = @Id";
        return await dapperContext.Connection.QueryFirstAsync<ClientDto>(sql, request);
    }
}
