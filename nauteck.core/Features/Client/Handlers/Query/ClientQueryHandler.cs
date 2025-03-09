using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

namespace nauteck.core.Features.Client.Handlers.Query;

public sealed class ClientQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.ClientQuery, IEnumerable<data.Dto.Client.ClientDto>>
{
    public async Task<IEnumerable<data.Dto.Client.ClientDto>> Handle(Queries.ClientQuery request, CancellationToken cancellationToken)
    {
        var sql = $@"SELECT 
            {DbConstants.Columns.Id}
            ,{DbConstants.Columns.Preamble}
            ,{DbConstants.Columns.LastName}
            ,{DbConstants.Columns.FirstName}
            ,{DbConstants.Columns.Infix} 
            ,{DbConstants.Columns.Phone} 
            ,{DbConstants.Columns.Email} 
            ,{DbConstants.Columns.Address} 
            ,{DbConstants.Columns.Number} 
            ,{DbConstants.Columns.Extension} 
            ,{DbConstants.Columns.Zipcode}             
            ,{DbConstants.Columns.City} 
            ,{DbConstants.Columns.Region}
            ,{DbConstants.Columns.Country}
            ,{DbConstants.Columns.BoatBrand}
            ,{DbConstants.Columns.BoatType} 
            ,{DbConstants.Columns.Active} 
            FROM {DbConstants.Tables.Client}";
        return await dapperContext.Connection.QueryAsync<data.Dto.Client.ClientDto>(sql);
    }
}
