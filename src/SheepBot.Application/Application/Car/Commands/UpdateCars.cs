using MediatR;
using SheepBot.iRacing.Client.Models;

namespace SheepBot.Application.Application.Car.Commands;

public class UpdateCars : IRequest
{
    public IEnumerable<iRacingCar> Cars { get; init; } = null!;
    public IEnumerable<iRacingCarClass> CarClasses { get; init; } = null!;
}