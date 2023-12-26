using Discord.Interactions;

namespace SheepBot.Modules;

public class VersionModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly string _version;
    private readonly string _environment;

    public VersionModule(Func<(string Version, string Environment)> appInfoFunc)
    {
        var appInfo = appInfoFunc();
        _version = appInfo.Version;
        _environment = appInfo.Environment;
    }
    
    [SlashCommand("version", "Get the bot version")]
    public async Task GetVersion()
    {
        await RespondAsync($"SheepBot {_environment}: v{_version}").ConfigureAwait(false);
    }
}