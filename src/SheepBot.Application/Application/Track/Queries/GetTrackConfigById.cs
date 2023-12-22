using MediatR;
using SheepBot.Domain.Entities;

namespace SheepBot.Application.Application.Track.Queries;

public class GetTrackConfigById : IRequest<TrackConfig?>
{
    public int Id { get; init; }
}