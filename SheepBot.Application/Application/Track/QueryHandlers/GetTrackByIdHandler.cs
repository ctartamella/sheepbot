using MediatR;
using SheepBot.Application.Application.Track.Queries;

namespace SheepBot.Application.Application.Track.QueryHandlers;

public class GetTrackByIdHandler : IRequestHandler<GetTrackById, Domain.Entities.Track>
{
    public Task<Domain.Entities.Track> Handle(GetTrackById request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}