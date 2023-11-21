// using SheepBot.Models;
// using SheepBot.Models.Joins;
// using SheepBot.Repositories.Tests.Base;
//
// namespace SheepBot.Repositories.Tests.Repositories;
//
// [Collection("Transactional")]
// public class CarCarClassTests : ModelTestBase<CarClassJoin>
// {
//     public CarCarClassTests() : base(unitOfWork => unitOfWork.CarCarClassRepository)
//     {
//     }
//
//     protected override async Task<IEnumerable<CarClassJoin>> CreateEntityListAsync()
//     {
//         var size = NextRandom(5, 10);
//         var classes = Enumerable.Range(1, size)
//             .Select(i => new Class { Name = $"Test class {i}" })
//             .ToList();
//         await UnitOfWork.CarClassRepository.InsertRangeAsync(classes);
//         var actualClasses = await UnitOfWork.CarClassRepository.GetAllAsync();
//         
//         size = NextRandom();
//         var cars = Enumerable.Range(1, size)
//             .Select(i => new Car { Name = $"Test car {i}" })
//             .ToList();
//         await UnitOfWork.CarRepository.InsertRangeAsync(cars);
//         var actualCars = await UnitOfWork.CarRepository.GetAllAsync();
//
//         return actualClasses.SelectMany(_ => actualCars,
//             (car, carClass) => new CarClassJoin { CarId = car.Id, ClassId = carClass.Id });
//     }
//
//     protected override async Task<CarClassJoin> CreateEntityAsync()
//     {
//         var i = NextRandom();
//         var carClass = new Class { Name = $"Test Class {i}" };
//         var classId = await UnitOfWork.CarClassRepository.InsertAsync(carClass);
//         
//         var car = new Car { Name = $"Test Car {i}" };
//         var carId = await UnitOfWork.CarRepository.InsertAsync(car);
//
//         return new CarClassJoin { CarId = carId, ClassId = classId };
//     }
//
//     protected override async Task<CarClassJoin> UpdateEntity(CarClassJoin entity)
//     {
//         var newCarClass = new Class { Name = "New Car Class" };
//         var newCarClassId = await UnitOfWork.CarClassRepository.InsertAsync(newCarClass);
//
//         var newCar = new Car { Name = "New Car" };
//         var newCarId = await UnitOfWork.CarRepository.InsertAsync(newCar);
//
//         return entity with
//         {
//             ClassId = newCarClassId,
//             CarId = newCarId
//         };
//     }
// }