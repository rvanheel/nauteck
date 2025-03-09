using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

namespace nauteck.core.Features.Dealer;

public sealed record class DealerQuery : IRequest<IEnumerable<data.Entities.Dealer.Dealer>>;

public sealed class DealerQueryHandler(IDapperContext dapperContext) : IRequestHandler<DealerQuery, IEnumerable<data.Entities.Dealer.Dealer>>
{
    public Task<IEnumerable<data.Entities.Dealer.Dealer>> Handle(DealerQuery request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.QueryAsync<data.Entities.Dealer.Dealer>($"SELECT * FROM {DbConstants.Tables.Dealer}");
    }
}