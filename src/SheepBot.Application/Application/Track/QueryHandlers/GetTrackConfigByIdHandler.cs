using MediatR;
using Microsoft.EntityFrameworkCore;
using SheepBot.Application.Application.Track.Queries;
using SheepBot.Domain.Entities;
using SheepBot.Infrastructure.Context;

namespace SheepBot.Application.Application.Track.QueryHandlers;

public class GetTrackConfigByIdHandler(SheepContext context) : IRequestHandler<GetTrackConfigById, TrackConfig?>
{
    public async Task<TrackConfig?> Handle(GetTrackConfigById request, CancellationToken cancellationToken)
    {
        return await context.TrackConfigs
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}