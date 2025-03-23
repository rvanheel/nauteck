using System.Text;

using Dapper;
using MediatR;
using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Dto.Agenda;

using static nauteck.core.Features.Agenda.Queries;

namespace nauteck.core.Features.Agenda.Handlers.Query;

public sealed class AgendaItemByIdForClientIdHandler(IDapperContext dapperContext) : IRequestHandler<AgendaItemByIdForClientId, AgendaDto>
{
    public async Task<AgendaDto> Handle(AgendaItemByIdForClientId request, CancellationToken cancellationToken)
    {
        var query = $@"SELECT 
                a.{DbConstants.Columns.Id}
                , a.{DbConstants.Columns.Comments}
                , a.{DbConstants.Columns.CreatedAt}
                , a.{DbConstants.Columns.CreatedBy}
                , a.{DbConstants.Columns.Date}
                , a.{DbConstants.Columns.Status}
                , a.{DbConstants.Columns.Title}
                , c.Id AS {DbConstants.Columns.ClientId}
                , c.{DbConstants.Columns.Preamble}
                , c.{DbConstants.Columns.LastName}
                , c.{DbConstants.Columns.FirstName}
                , c.{DbConstants.Columns.Infix}
                , c.{DbConstants.Columns.Address}
                , c.{DbConstants.Columns.Number}
                , c.{DbConstants.Columns.Extension}
                , c.{DbConstants.Columns.Zipcode}
                , c.{DbConstants.Columns.City}
                , c.{DbConstants.Columns.Region}
                , c.{DbConstants.Columns.Country}
                FROM {DbConstants.Tables.Client} AS c 
                LEFT JOIN {DbConstants.Tables.Agenda} AS a ON c.{DbConstants.Columns.Id}  = a.{DbConstants.Columns.ClientId} AND a.{DbConstants.Columns.Id} = @Id
                WHERE c.{DbConstants.Columns.Id} = @ClientId";
        
        var record = await dapperContext.Connection.QueryFirstAsync<AgendaDto>(query, request);

        return record;
    }
}