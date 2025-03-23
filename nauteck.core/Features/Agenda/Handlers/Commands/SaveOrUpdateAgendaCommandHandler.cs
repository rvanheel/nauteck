using Dapper;
using MediatR;
using nauteck.core.Abstraction;
using nauteck.core.Implementation;

namespace nauteck.core.Features.Agenda.Handlers.Commands;

public sealed class SaveOrUpdateAgendaCommandHandler(IDapperContext dapperContext) : IRequestHandler<Agenda.Commands.SaveOrUpdateAgendaCommand>
{
    public async Task Handle(Agenda.Commands.SaveOrUpdateAgendaCommand request, CancellationToken cancellationToken)
    {
        await Insert(request.AgendaItem, cancellationToken);
        await Update(request.AgendaItem, cancellationToken);
    }
    #region Private Methods
    private async Task Insert(data.Entities.Agenda.Agenda agendaItem, CancellationToken cancellationToken)
    {
        if (!agendaItem.Id.Equals(Guid.Empty)) return;
        await dapperContext.Connection.ExecuteAsync($@"
            INSERT INTO {DbConstants.Tables.Agenda} ({DbConstants.Columns.Id}, {DbConstants.Columns.ClientId}, {DbConstants.Columns.Status}, {DbConstants.Columns.Title}, {DbConstants.Columns.Date}, {DbConstants.Columns.Comments}, {DbConstants.Columns.CreatedAt}, {DbConstants.Columns.CreatedBy})
            VALUES (UUID()
                , @{nameof(DbConstants.Columns.ClientId)}
                , @{nameof(DbConstants.Columns.Status)}
                , @{nameof(DbConstants.Columns.Title)}
                , @{nameof(DbConstants.Columns.Date)}
                , @{nameof(DbConstants.Columns.Comments)}
                , @{nameof(DbConstants.Columns.CreatedAt)}
                , @{nameof(DbConstants.Columns.CreatedBy)}
            )", agendaItem);
    }
    private async Task Update(data.Entities.Agenda.Agenda agendaItem, CancellationToken cancellationToken)
    {
        if (agendaItem.Id.Equals(Guid.Empty)) return;
        await dapperContext.Connection.ExecuteAsync($@"
            UPDATE {DbConstants.Tables.Agenda}
            SET 
                {DbConstants.Columns.Title} = @{nameof(DbConstants.Columns.Title)}
                , {DbConstants.Columns.Status} = @{nameof(DbConstants.Columns.Status)}
                , {DbConstants.Columns.Date} = @{nameof(DbConstants.Columns.Date)}
                , {DbConstants.Columns.Comments} = @{nameof(DbConstants.Columns.Comments)}
            WHERE {DbConstants.Columns.Id} = @{nameof(DbConstants.Columns.Id)}
        ", agendaItem);
    }
    #endregion
}