using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

using static nauteck.core.Features.Commands.Quotation;

namespace nauteck.core.Features.Handlers.Command.Quotation;

public sealed class QuotationDeleteCommandHandler(IDapperContext dapperContext) : IRequestHandler<QuotationDeleteCommand, int>
{
    public Task<int> Handle(QuotationDeleteCommand request, CancellationToken cancellationToken)
    {
        var sql = @$"DELETE FROM {DbConstants.Tables.Quotation.TableName} WHERE {DbConstants.Tables.Quotation.Columns.Id} = @{nameof(request.Id)}";
        return dapperContext.Connection.ExecuteAsync(sql, request);
    }
}
