using SheepBot.Models;
using SheepBot.Repositories.Interfaces;
using SheepBot.Services.Interfaces;

namespace SheepBot.Services.Services;

public class CarService : ICarService
{
    private readonly IUnitOfWork _unitOfWork;

    public CarService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        var cars = (await _unitOfWork.CarRepository.GetAllAsync().ConfigureAwait(false)).ToList();
        var carIds = cars.Select(c => c.Id);

        var joins = await _unitOfWork.CarCarClassRepository.GetJoinsForCarIdsAsync(carIds).ConfigureAwait(false);
        var classIds = joins.Values.SelectMany(x => x);
        var classes = await _unitOfWork.CarClassRepository.GetClassesForIds(classIds).ConfigureAwait(false);
        var classDict = new Dictionary<int, CarClass>(classes.Select(c => new KeyValuePair<int, CarClass>(c.Id, c)));
        
        foreach (var car in cars)
        {
            car.Classes.Clear();

            var carClasses = joins[car.Id].Select(carClassId => classDict[carClassId]);
            car.Classes.AddRange(carClasses);
        }

        return new List<Car>();
    }
    
}