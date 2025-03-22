using Dapper;
using MediatR;
using nauteck.core.Abstraction;
using nauteck.core.Implementation;

namespace nauteck.core.Features.Agenda.Handlers.Commands;

public sealed class DeleteAgendaCommandHandler(IDapperContext dapperContext) : IRequestHandler<Agenda.Commands.DeleteAgendaCommand>
{
    public Task Handle(Agenda.Commands.DeleteAgendaCommand request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.ExecuteAsync($"DELETE FROM {DbConstants.Tables.Agenda} WHERE {DbConstants.Columns.Id} = @Id", request);
    }
}