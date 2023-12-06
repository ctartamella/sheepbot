using MediatR;

namespace SheepBot.Application.Application.Track.Queries;

public record GetAllTracks : IRequest<IEnumerable<Domain.Entities.Track>>
{
    
}