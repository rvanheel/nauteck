﻿using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

using static nauteck.core.Features.Commands.Quotation;
using static nauteck.core.Implementation.DbConstants;

namespace nauteck.core.Features.Handlers.Command.Quotation;

public sealed class QuotationLineDeleteCommandHandler(IDapperContext dapperContext) : IRequestHandler<QuotationLineDeleteCommand, int>
{
    public Task<int> Handle(QuotationLineDeleteCommand request, CancellationToken cancellationToken)
    {
        var sql = @$"DELETE FROM {Tables.QuotationLine.TableName} WHERE {Tables.QuotationLine.Columns.Id} = @{nameof(request.Id)};
            {Sql.UpdateQuotion(nameof(QuotationLineDeleteCommand.QuotationId))}";
        return dapperContext.Connection.ExecuteAsync(sql, request);
    }
}