using nauteck.core.Implementation;
using nauteck.data.Dto.Account;
using nauteck.data.Enums;


namespace nauteck.core.Features.User.Handlers.Query;

public sealed class UserQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.UserQuery, IEnumerable<AccountDto>>
{
    public Task<IEnumerable<AccountDto>> Handle(Queries.UserQuery request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.QueryAsync<AccountDto>($"SELECT * FROM {DbConstants.Tables.User} WHERE {DbConstants.Columns.Roles} < {(byte)RoleType.SuperUser}", cancellationToken);
    }
}
