using MediatR;

namespace SheepBot.Application.Application.Track.Queries;

public class GetTracksByName : IRequest<IEnumerable<Domain.Entities.Track>>
{
    public string Name { get; init; } = null!;
    public int MaxResults { get; init; } = -1;
}