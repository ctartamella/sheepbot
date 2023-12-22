using Discord.Interactions;
using MediatR;
using SheepBot.Application.Application.Track.Queries;

namespace SheepBot.Modules;

[Group("track", "Get track info")]
// ReSharper disable once UnusedType.Global
public class TrackInfoModule(IMediator mediator) : BaseIRacingModule(mediator)
{
    [SlashCommand("lookup", "Get Track Info")]
    // ReSharper disable once UnusedMember.Global
    public Task GetTrackInfoAsync([Summary(description: "Track search parameter")] string name)
    {
        return StartTrackSearch(name, TrackByNameId);
    }
    
    [SlashCommand("layout", "Find information about a specific layout")]
    // ReSharper disable once UnusedMember.Global
    public Task GetLayoutInfo([Summary(description: "Track search parameter")] string name)
    {
        return StartTrackSearch(name, TrackConfigTrackByNameId);
    }
    
    [ComponentInteraction(TrackByNameId)]
    // ReSharper disable once UnusedMember.Global
    public async Task HandleTrackMenuAsync(string trackId)
    {
        var isValid = int.TryParse(trackId, out var id);
        if (!isValid)
        {
            await RespondAsync("Invalid track ID.  This is likely a bug.").ConfigureAwait(false);
            return;
        }

        var track = await mediator.Send(new GetTrackById {Id = id}).ConfigureAwait(false);
        if (track is null)
        {
            await RespondAsync("Invalid track ID.  This is likely a bug.").ConfigureAwait(false);
            return;
        }
        
        await ReturnTrackResultAsync(track).ConfigureAwait(false);
    }
    
    [ComponentInteraction(TrackConfigTrackByNameId)]
    // ReSharper disable once UnusedMember.Global
    public async Task HandleTrackConfigTrackMenuAsync(string trackId)
    {
        var isValid = int.TryParse(trackId, out var id);
        if (!isValid)
        {
            await RespondAsync("Invalid track ID.  This is likely a bug.").ConfigureAwait(false);
            return;
        }

        var message = await BuildTrackConfigComponent(id, $"track;{TrackConfigByNameId}").ConfigureAwait(false);
        await RespondAsync("Choose a layout", components: message, ephemeral: true).ConfigureAwait(false);
    }
    
    [ComponentInteraction(TrackConfigByNameId)]
    // ReSharper disable once UnusedMember.Global
    public async Task HandleTrackConfigMenuAsync(string configId)
    {
        var isValid = int.TryParse(configId, out var id);
        if (!isValid)
        {
            await RespondAsync("Invalid track ID.  This is likely a bug.").ConfigureAwait(false);
            return;
        }

        var config = await mediator.Send(new GetTrackConfigById { Id = id }).ConfigureAwait(false);
        if (config is null)
        {
            await RespondAsync("Invalid track config ID.  This is likely a bug.").ConfigureAwait(false);
            return;
        }
        
        await ReturnConfigResultAsync(config).ConfigureAwait(false);
    }

    private async Task StartTrackSearch(string name, string customId)
    {
        var message = await BuildTrackComponent(name, $"track;{customId}").ConfigureAwait(false);
        if (message is null)
        {
            return;
        }

        await RespondAsync("Choose a track", components: message, ephemeral: true).ConfigureAwait(false);
    }
}