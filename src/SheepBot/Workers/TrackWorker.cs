using MediatR;
using Microsoft.Extensions.Hosting;
using SheepBot.Application.Application.Car.Commands;
using SheepBot.Application.Application.Track.Commands;
using SheepBot.iRacing.Client.Interfaces;

namespace SheepBot.SyncWorkers.Workers;

// ReSharper disable once InconsistentNaming
public class TrackWorker(IMediator mediator, IiRacingService iracingService) : IHostedService
{
    private DateTime _lastRun;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var isFirstTime = true;

        while (!cancellationToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;
        
            if (isFirstTime || _lastRun.Day != now.Day)
            {
                var tracks = await iracingService.GetAllTracks().ConfigureAwait(false);
                var cars = await iracingService.GetAllCars().ConfigureAwait(false);
                var carClasses = await iracingService.GetAllCarClasses().ConfigureAwait(false);
                var filteredCarClasses = carClasses.Where(c => c.CarClassId > 0);
                
                await mediator.Send(new UpdateTracks { Tracks = tracks }, cancellationToken).ConfigureAwait(false);
                await mediator.Send(new UpdateCars { Cars = cars, CarClasses = filteredCarClasses }, cancellationToken).ConfigureAwait(false);
                
                _lastRun = now;
                isFirstTime = false;
            }
            
            // 30 minute sleep.
            Thread.Sleep(1800000);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}