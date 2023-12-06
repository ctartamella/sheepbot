using AutoMapper;
using Dapper;
using MediatR;
using SheepBot.Application.Application.Track.Commands;
using SheepBot.Application.Interfaces;
using SheepBot.Domain.Entities;

namespace SheepBot.Application.Application.Track.CommandHandlers;

public class UpdateTracksHandler(IMapper mapper, IConnectionFactory connectionFactory) : IRequestHandler<UpdateTracks>
{
    private const string TrackType = "TrackType";
    private const string ConfigType = "TrackConfigType";
    private const string UpdateTracksProcedure = "[dbo].[merge_tracks]";
    private const string UpdateConfigsProcedure = "[dbo].[merge_track_configs]";

    public async Task Handle(UpdateTracks request, CancellationToken cancellationToken)
    {
        using var connection = connectionFactory.GetConnection();

        var tracks = request.Tracks
            .GroupBy(t => t.TrackId)
            .Select(g => g.First());

        using var trackTable =  Domain.Entities.Track.CreateDataTable(
            mapper.Map<IEnumerable<iRacing.Client.Models.iRacingTrack>, IEnumerable<Domain.Entities.Track>>(tracks));
        
        // Merging tracks
        await connection.ExecuteAsync(UpdateTracksProcedure, new
        {
            Tracks = trackTable.AsTableValuedParameter(TrackType)
        }).ConfigureAwait(false);

        var configs = TrackConfig.CreateDataTable(mapper.Map<IEnumerable<TrackConfig>>(request.Tracks));
        
        // Merging configs
        await connection.ExecuteAsync(UpdateConfigsProcedure, new
        {
            Configs = configs.AsTableValuedParameter(ConfigType)
        }).ConfigureAwait(false);
    }
}