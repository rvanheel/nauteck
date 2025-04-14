using nauteck.data.Dto.Quotation;

using static nauteck.core.Features.Commands.Quotation;
using static nauteck.core.Implementation.DbConstants;

namespace nauteck.core.Features.Handlers.Command.Quotation;

public sealed class SaveOrUpdateQuotationLineCommandHandler(IDapperContext dapperContext) : IRequestHandler<QuotationLineSaveOrUpdateCommand>
{
    public async Task Handle(QuotationLineSaveOrUpdateCommand request, CancellationToken cancellationToken)
    {
        await Insert(request.QuotationLine);
        await Update(request.QuotationLine);
    }
    #region Private Methods
    private Task Insert(QuotationLineDto quotationLineDto)
    {
        if (!quotationLineDto.Id.Equals(Guid.Empty)) return Task.CompletedTask;
        var sql = $@"
        INSERT INTO {Tables.QuotationLine.TableName} (
            {Tables.QuotationLine.Columns.Id}
            , {Tables.QuotationLine.Columns.Amount}
            , {Tables.QuotationLine.Columns.Description}
            , {Tables.QuotationLine.Columns.Tax}
            , {Tables.QuotationLine.Columns.Quantity}
            , {Tables.QuotationLine.Columns.QuotationId}
        )
        VALUES (
        UUID()
        , @{nameof(QuotationLineDto.Amount)}
        , @{nameof(QuotationLineDto.Description)}
        , @{nameof(QuotationLineDto.Tax)}
        , @{nameof(QuotationLineDto.Quantity)}
        , @{nameof(QuotationLineDto.QuotationId)}
        ); 
        {Sql.UpdateQuotation(nameof(quotationLineDto.QuotationId))}
       ";
        return dapperContext.Connection.ExecuteAsync(sql, quotationLineDto);
    }
    private Task Update(QuotationLineDto quotationLineDto)
    {
        if (quotationLineDto.Id.Equals(Guid.Empty)) return Task.CompletedTask;
        var sql = $@"
        UPDATE {Tables.QuotationLine.TableName} 
        SET 
            {Tables.QuotationLine.Columns.Amount} = @{nameof(QuotationLineDto.Amount)}
            , {Tables.QuotationLine.Columns.Description} = @{nameof(QuotationLineDto.Description)}
            , {Tables.QuotationLine.Columns.Tax} = @{nameof(QuotationLineDto.Tax)}
            , {Tables.QuotationLine.Columns.Quantity} = @{nameof(QuotationLineDto.Quantity)}
        WHERE {Tables.QuotationLine.Columns.Id} = @{nameof(QuotationLineDto.Id)};
        {Sql.UpdateQuotation(nameof(quotationLineDto.QuotationId))}
        ";
        return dapperContext.Connection.ExecuteAsync(sql, quotationLineDto);
    }
    #endregion

}