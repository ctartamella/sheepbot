using Discord;
using Discord.Interactions;
using MediatR;
using SheepBot.Application.Application.Track.Queries;
using SheepBot.Domain.Entities;

namespace SheepBot.Modules;

public class BaseIRacingModule(IMediator mediator) : InteractionModuleBase<SocketInteractionContext>
{
    protected const string TrackByNameId = "track-by-name";
    protected const string TrackConfigByNameId = "track-config-by-name";
    protected const string TrackConfigTrackByNameId = "track-config-track-by-name";
    private const string TrackByNamePlaceholder = "Select a track";
    
    protected async Task<MessageComponent?> BuildTrackConfigComponent(int id, string customHandlerId)
    {
        var request = new GetTrackConfigsForTrack { TrackId = id };
        var configs = (await mediator.Send(request).ConfigureAwait(false)).ToList();

        if (configs.Count == 0)
        {
            await RespondAsync("No configs found. This is probably a bug.", ephemeral: true).ConfigureAwait(false);
            return null;
        }

        if (configs.Count == 1)
        {
            await ReturnConfigResultAsync(configs.First()).ConfigureAwait(false);
            return null;
        }

        var trackList = new SelectMenuBuilder()
            .WithPlaceholder(TrackByNamePlaceholder)
            .WithCustomId(customHandlerId)
            .WithMinValues(1)
            .WithMaxValues(1);
        
        foreach (var config in configs)
        {
            trackList.AddOption(config.Name, config.Id.ToString());
        }
        
        return new ComponentBuilder().WithSelectMenu(trackList).Build();
    }

    protected async Task<MessageComponent?> BuildTrackComponent(string name, string customHandlerId)
    {
        var request = new GetTracksByName {Name = name, MaxResults = 25};
        var tracks = (await mediator.Send(request).ConfigureAwait(false)).ToList();

        if (tracks.Count == 0)
        {
            await RespondAsync("No results found.", ephemeral: true).ConfigureAwait(false);
            return null;
        }

        if (tracks.Count == 1)
        {
            await ReturnTrackResultAsync(tracks.First()).ConfigureAwait(false);
            return null;
        }

        var trackList = new SelectMenuBuilder()
            .WithPlaceholder(TrackByNamePlaceholder)
            .WithCustomId(customHandlerId)
            .WithMinValues(1)
            .WithMaxValues(1);
        
        foreach (var track in tracks)
        {
            trackList.AddOption(track.Name, track.Id.ToString());
        }
        
        return new ComponentBuilder().WithSelectMenu(trackList).Build();
    }
    
    protected async Task ReturnConfigResultAsync(TrackConfig config)
    {
        await FollowupAsync($"You've selected the {config.Name} configuration.").ConfigureAwait(false);
    }
    
    protected async Task ReturnTrackResultAsync(Track track)
    {
        await RespondAsync($"You've selected trackId {track.TrackId}").ConfigureAwait(false);
    }
}