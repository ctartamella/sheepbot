using SheepBot.Models;
using SheepBot.Repositories.Tests.Base;

namespace SheepBot.Repositories.Tests.Repositories;

[Collection("Transactional")]
public class CarCarClassTests : ModelTestBase<CarCarClass>
{
    public CarCarClassTests() : base(unitOfWork => unitOfWork.CarCarClassRepository)
    {
    }

    protected override async Task<IEnumerable<CarCarClass>> CreateEntityListAsync()
    {
        var size = NextRandom(5, 10);
        var classes = Enumerable.Range(1, size)
            .Select(i => new CarClass { Name = $"Test class {i}" })
            .ToList();
        await UnitOfWork.CarClassRepository.InsertRangeAsync(classes);
        var actualClasses = await UnitOfWork.CarClassRepository.GetAllAsync();
        
        size = NextRandom();
        var cars = Enumerable.Range(1, size)
            .Select(i => new Car { Name = $"Test car {i}" })
            .ToList();
        await UnitOfWork.CarRepository.InsertRangeAsync(cars);
        var actualCars = await UnitOfWork.CarRepository.GetAllAsync();

        return actualClasses.SelectMany(_ => actualCars,
            (car, carClass) => new CarCarClass { CarId = car.Id, ClassId = carClass.Id });
    }

    protected override async Task<CarCarClass> CreateEntityAsync()
    {
        var i = NextRandom();
        var carClass = new CarClass { Name = $"Test Class {i}" };
        var classId = await UnitOfWork.CarClassRepository.InsertAsync(carClass);
        
        var car = new Car { Name = $"Test Car {i}" };
        var carId = await UnitOfWork.CarRepository.InsertAsync(car);

        return new CarCarClass { CarId = carId, ClassId = classId };
    }

    protected override async Task<CarCarClass> UpdateEntity(CarCarClass entity)
    {
        var newCarClass = new CarClass { Name = "New Car Class" };
        var newCarClassId = await UnitOfWork.CarClassRepository.InsertAsync(newCarClass);

        var newCar = new Car { Name = "New Car" };
        var newCarId = await UnitOfWork.CarRepository.InsertAsync(newCar);

        entity.ClassId = newCarClassId;
        entity.CarId = newCarId;

        return entity;
    }
}