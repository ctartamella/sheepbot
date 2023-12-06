using MediatR;

namespace SheepBot.Application.Application.Track.Queries;

public record GetTrackById : IRequest<Domain.Entities.Track>
{
    public long Id { get; init; }
}