using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Dto.Account;
using nauteck.data.Models.User;

namespace nauteck.core.Features.User.Handlers.Query;

public sealed class UserByIdQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.UserByIdQuery, UserPostModel>
{
    public Task<UserPostModel> Handle(Queries.UserByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id.Equals(Guid.Empty)) return Task.FromResult(new UserPostModel { Roles = data.Enums.RoleType.Gebruiker, Active = true, DealerId = request.DealerId });

        return dapperContext.Connection.QueryFirstAsync<UserPostModel>($"SELECT * FROM {DbConstants.Tables.User} WHERE {DbConstants.Columns.Id} = @Id", new { request.Id });
    }
}
