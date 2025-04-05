using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Dto.Quotation;

using static nauteck.core.Features.Commands.Quotation;

namespace nauteck.core.Features.Handlers.Command.Quotation;

public sealed class SaveOrUpdateQuotationCommandHandler(IDapperContext dapperContext) : IRequestHandler<SaveOrUpdateQuotationCommand>
{
    public async Task Handle(SaveOrUpdateQuotationCommand request, CancellationToken cancellationToken)
    {
        await Insert(request.Quotation);
        await Update(request.Quotation);
    }

    #region Private Methods
    private async Task Insert(QuotationDto quotationDto)
    {        
        if (!quotationDto.Id.Equals(Guid.Empty)) return;
        var sql = @$"
        SET @lastid = UUID();
        SELECT
            @newNumber := 
                CONCAT(extract(YEAR FROM current_date())
                , LPAD(extract(MONTH FROM current_date()), 2, '0')
                , LPAD(COUNT({DbConstants.Tables.Quotation.Columns.Id})+1, 3, '0'))
            FROM {DbConstants.Tables.Quotation.TableName}
            WHERE extract(YEAR FROM {DbConstants.Tables.Quotation.Columns.Date}) = extract(YEAR FROM current_date()) 
            AND extract(MONTH FROM {DbConstants.Tables.Quotation.Columns.Date}) = extract(MONTH FROM current_date());

        INSERT INTO {DbConstants.Tables.Quotation.TableName} (
            {DbConstants.Tables.Quotation.Columns.Id}
            , {DbConstants.Tables.Quotation.Columns.Amount}
            , {DbConstants.Tables.Quotation.Columns.ClientId}
            , {DbConstants.Tables.Quotation.Columns.Date}
            , {DbConstants.Tables.Quotation.Columns.Description}
            , {DbConstants.Tables.Quotation.Columns.Status}
            , {DbConstants.Tables.Quotation.Columns.Reference}
            )
            VALUES (
            @lastid
            , 0
            , @{nameof(QuotationDto.ClientId)}
            , @{nameof(QuotationDto.Date)}
            , @{nameof(QuotationDto.Description)}
            , @{nameof(QuotationDto.Status)}
            , @newNumber    
            );";
        await dapperContext.Connection.ExecuteAsync(sql, quotationDto);
    }
    private Task Update(QuotationDto quotationDto)
    {
        if (quotationDto.Id.Equals(Guid.Empty)) return Task.CompletedTask;
        var sql = @$"UPDATE {DbConstants.Tables.Quotation.TableName} 
            SET 
            {DbConstants.Tables.Quotation.Columns.Amount} = @{nameof(QuotationDto.Amount)}
            , {DbConstants.Tables.Quotation.Columns.Description} = @{nameof(QuotationDto.Description)}
            , {DbConstants.Tables.Quotation.Columns.Date} = @{nameof(QuotationDto.Date)}
            , {DbConstants.Tables.Quotation.Columns.Status} = @{nameof(QuotationDto.Status)}
            WHERE {DbConstants.Tables.Quotation.Columns.Id} = @{nameof(QuotationDto.Id)}";
        return dapperContext.Connection.ExecuteAsync(sql, quotationDto);
    }
    #endregion
}
