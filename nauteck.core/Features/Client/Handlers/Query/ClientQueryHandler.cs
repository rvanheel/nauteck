using nauteck.core.Implementation;

namespace nauteck.core.Features.Client.Handlers.Query;

public sealed class ClientQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.ClientQuery, IEnumerable<data.Dto.Client.ClientDto>>
{
    public async Task<IEnumerable<data.Dto.Client.ClientDto>> Handle(Queries.ClientQuery request, CancellationToken cancellationToken)
    {
        var sql = $@"SELECT 
            {DbConstants.Tables.Client.Columns.Id}
            ,{DbConstants.Tables.Client.Columns.Preamble}
            ,{DbConstants.Tables.Client.Columns.LastName}
            ,{DbConstants.Tables.Client.Columns.FirstName}
            ,{DbConstants.Tables.Client.Columns.Infix} 
            ,{DbConstants.Tables.Client.Columns.Phone} 
            ,{DbConstants.Tables.Client.Columns.Email} 
            ,{DbConstants.Tables.Client.Columns.Address} 
            ,{DbConstants.Tables.Client.Columns.Number} 
            ,{DbConstants.Tables.Client.Columns.Extension} 
            ,{DbConstants.Tables.Client.Columns.Zipcode}             
            ,{DbConstants.Tables.Client.Columns.City} 
            ,{DbConstants.Tables.Client.Columns.Region}
            ,{DbConstants.Tables.Client.Columns.Country}
            ,{DbConstants.Tables.Client.Columns.BoatBrand}
            ,{DbConstants.Tables.Client.Columns.BoatType} 
            ,{DbConstants.Tables.Client.Columns.Active} 
            FROM {DbConstants.Tables.Client.TableName}";
        return await dapperContext.Connection.QueryAsync<data.Dto.Client.ClientDto>(sql);
    }
}
