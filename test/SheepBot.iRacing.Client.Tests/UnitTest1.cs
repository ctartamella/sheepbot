using Microsoft.Extensions.DependencyInjection;
using SheepBot.iRacing.Client.Helpers;
using SheepBot.iRacing.Client.Interfaces;

namespace SheepBot.iRacing.Client.Tests;

public class UnitTest1
{
    private readonly IServiceProvider _provider;
    
    public UnitTest1()
    {
        var services = new ServiceCollection();
        //services.AddIRacingClient("https://members-ng.iracing.com", "chris@tartamella.me", "1qaz@WSX3edc");

        _provider = services.BuildServiceProvider();
    }
    
    [Fact]
    public async Task Test1()
    {
        var service = _provider.GetRequiredService<IiRacingService>();
        var results = await service.GetAllTracks().ConfigureAwait(true);
        
        Assert.NotEmpty(results);
    }
}