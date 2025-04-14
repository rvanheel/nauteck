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
        const string sql = $"""
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
            );
        """;
        await dapperContext.Connection.ExecuteAsync(sql, quotationDto);
    }
    private Task Update(QuotationDto quotationDto)
    {
        if (quotationDto.Id.Equals(Guid.Empty)) return Task.CompletedTask;
        const string sql = $"""
            UPDATE {Tables.Quotation.TableName} 
            SET 
                {Tables.Quotation.Columns.Amount} = @{nameof(QuotationDto.Amount)}
                , {Tables.Quotation.Columns.Description} = @{nameof(QuotationDto.Description)}
                , {Tables.Quotation.Columns.Date} = @{nameof(QuotationDto.Date)}
                , {Tables.Quotation.Columns.Status} = @{nameof(QuotationDto.Status)}
            WHERE {Tables.Quotation.Columns.Id} = @{nameof(QuotationDto.Id)}
        """;
        return dapperContext.Connection.ExecuteAsync(sql, quotationDto);
    }
    #endregion
}
