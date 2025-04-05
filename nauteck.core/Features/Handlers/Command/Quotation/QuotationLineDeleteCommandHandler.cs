using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

using static nauteck.core.Features.Commands.Quotation;

namespace nauteck.core.Features.Handlers.Command.Quotation;

public sealed class QuotationLineDeleteCommandHandler(IDapperContext dapperContext) : IRequestHandler<QuotationLineDeleteCommand, int>
{
    public Task<int> Handle(QuotationLineDeleteCommand request, CancellationToken cancellationToken)
    {
        var sql = @$"DELETE FROM {DbConstants.Tables.QuotationLine.TableName} WHERE {DbConstants.Tables.QuotationLine.Columns.Id} = @{nameof(request.Id)};
            
            UPDATE {DbConstants.Tables.Quotation.TableName} AS q 
            INNER JOIN {DbConstants.Tables.QuotationLine.TableName} AS ql
                ON ql.{DbConstants.Tables.QuotationLine.Columns.QuotationId} = q.{DbConstants.Tables.Quotation.Columns.Id}
            SET q.{DbConstants.Tables.Quotation.Columns.Amount} = ql.{DbConstants.Tables.QuotationLine.Columns.Quantity}*ql.{DbConstants.Tables.QuotationLine.Columns.Amount}
            WHERE q.{DbConstants.Tables.Quotation.Columns.Id} = @{nameof(request.Id)};
        ";
        return dapperContext.Connection.ExecuteAsync(sql, request);
    }
}