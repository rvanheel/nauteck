using Dapper;

using MediatR;

using Mysqlx.Crud;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Dto.Quotation;

using static nauteck.core.Features.Commands.Quotation;
using static nauteck.core.Implementation.DbConstants;

namespace nauteck.core.Features.Handlers.Command.Quotation;

public sealed class SaveOrUpdateQuotationCommandHandler(IDapperContext dapperContext) : IRequestHandler<SaveOrUpdateQuotationCommand, Guid>
{
    public async Task<Guid> Handle(SaveOrUpdateQuotationCommand request, CancellationToken cancellationToken)
    {
        var id = request.Quotation.Id.Equals(Guid.Empty) ? Guid.NewGuid() : request.Quotation.Id;
        await Insert(request.Quotation, id);
        await Update(request.Quotation);
        return request.Quotation.Id;
    }

    #region Private Methods
    private async Task Insert(QuotationDto quotationDto, Guid id)
    {        
        if (!quotationDto.Id.Equals(Guid.Empty)) return;
        quotationDto.Id = id;
        var sql = @$"
        SELECT
            @newNumber := 
                CONCAT(extract(YEAR FROM current_date())
                , LPAD(extract(MONTH FROM current_date()), 2, '0')
                , LPAD(COUNT({Tables.Quotation.Columns.Id})+1, 3, '0'))
            FROM {Tables.Quotation.TableName}
            WHERE extract(YEAR FROM {Tables.Quotation.Columns.Date}) = extract(YEAR FROM current_date()) 
            AND extract(MONTH FROM {Tables.Quotation.Columns.Date}) = extract(MONTH FROM current_date());

        INSERT INTO {Tables.Quotation.TableName} (
            {Tables.Quotation.Columns.Id}
            , {Tables.Quotation.Columns.Amount}
            , {Tables.Quotation.Columns.ClientId}
            , {Tables.Quotation.Columns.Date}
            , {Tables.Quotation.Columns.Description}
            , {Tables.Quotation.Columns.Status}
            , {Tables.Quotation.Columns.Reference}
            )
        VALUES (
            @{nameof(QuotationDto.Id)}
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
        var sql = @$"UPDATE {Tables.Quotation.TableName} 
            SET 
            {Tables.Quotation.Columns.Amount} = @{nameof(QuotationDto.Amount)}
            , {Tables.Quotation.Columns.Description} = @{nameof(QuotationDto.Description)}
            , {Tables.Quotation.Columns.Date} = @{nameof(QuotationDto.Date)}
            , {Tables.Quotation.Columns.Status} = @{nameof(QuotationDto.Status)}
            WHERE {Tables.Quotation.Columns.Id} = @{nameof(QuotationDto.Id)}";
        return dapperContext.Connection.ExecuteAsync(sql, quotationDto);
    }
    #endregion
}

public sealed class QuotationLineSaveOrUpdateCommandHandler(IDapperContext dapperContext) : IRequestHandler<QuotationLineSaveOrUpdateCommand>
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
        var sql = @$"INSERT INTO {Tables.QuotationLine.TableName} (
            {Tables.QuotationLine.Columns.Id}
            , {Tables.QuotationLine.Columns.Amount}
            , {Tables.QuotationLine.Columns.Description}
            , {Tables.QuotationLine.Columns.Quantity}
            , {Tables.QuotationLine.Columns.QuotationId}
            )
        VALUES (
            UUID()
            , @{nameof(QuotationLineDto.Amount)}
            , @{nameof(QuotationLineDto.Description)}
            , @{nameof(QuotationLineDto.Quantity)}
            , @{nameof(QuotationLineDto.QuotationId)}
        ); 
        {Sql.UpdateQuotion(nameof(Tables.QuotationLine.Columns.QuotationId))}";
        return dapperContext.Connection.ExecuteAsync(sql, quotationLineDto);
    }
    private Task Update(QuotationLineDto quotationLineDto)
    {
        if (quotationLineDto.Id.Equals(Guid.Empty)) return Task.CompletedTask;
        var sql = @$"UPDATE {Tables.QuotationLine.TableName} 
            SET 
            {Tables.QuotationLine.Columns.Amount} = @{nameof(QuotationLineDto.Amount)}
            , {Tables.QuotationLine.Columns.Description} = @{nameof(QuotationLineDto.Description)}
            , {Tables.QuotationLine.Columns.Quantity} = @{nameof(QuotationLineDto.Quantity)}
            WHERE {Tables.QuotationLine.Columns.Id} = @{nameof(QuotationLineDto.Id)};
            {Sql.UpdateQuotion(nameof(Tables.QuotationLine.Columns.QuotationId))}";
        return dapperContext.Connection.ExecuteAsync(sql, quotationLineDto);
    }
    #endregion

}