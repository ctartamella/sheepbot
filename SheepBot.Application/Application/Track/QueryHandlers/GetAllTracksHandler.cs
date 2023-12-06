using MediatR;
using SheepBot.Application.Application.Track.Queries;

namespace SheepBot.Application.Application.Track.QueryHandlers;

public class GetAllTracksHandler : IRequestHandler<GetAllTracks, IEnumerable<Domain.Entities.Track>>
{
    public Task<IEnumerable<Domain.Entities.Track>> Handle(GetAllTracks request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}