using AutoMapper;
using Dapper;
using MediatR;
using SheepBot.Application.Application.Car.Commands;
using SheepBot.Application.Interfaces;
using SheepBot.Domain.Entities;

namespace SheepBot.Application.Application.Car.CommandHandlers;

public class UpdateCarsHandler(IMapper mapper, IConnectionFactory connectionFactory) : IRequestHandler<UpdateCars>
{
    private const string UpdateCarsQuery = "[dbo].[merge_cars]";
    private const string CarType = "CarType";
    
    private const string UpdateClassesQuery = "[dbo].[merge_classes]";
    private const string ClassType = "ClassType";
    
    private const string UpdateCarClassesQuery = "[dbo].[merge_car_classes]";
    private const string CarClassType = "CarClassType";
    
    public async Task Handle(UpdateCars request, CancellationToken cancellationToken)
    {
        var cars = mapper.Map<IEnumerable<Domain.Entities.Car>>(request.Cars).ToList();
        var classes = mapper.Map<IEnumerable<Class>>(request.CarClasses).ToList();
        var joins = request.CarClasses
            .SelectMany(c => c.CarsInClass.Select(d => new CarClass { CarId = d.CarId, ClassId = c.CarClassId}))
            .Join(cars, x => x.CarId, x => x.CarId, (join, _) => join)
            .Join(classes, x => x.ClassId, x => x.ClassId, (join, _) => join)
            .ToList();
        
        var carTable = Domain.Entities.Car.CreateDataTable(cars);
        var classTable = Class.CreateDataTable(classes);
        var joinsTable = CarClass.CreateDataTable(joins);

        using var connection = connectionFactory.GetConnection();

        await connection
            .ExecuteAsync(UpdateCarsQuery, new {Cars = carTable.AsTableValuedParameter(CarType)})
            .ConfigureAwait(false);
        
        await connection
            .ExecuteAsync(UpdateClassesQuery, new {Classes = classTable.AsTableValuedParameter(ClassType)})
            .ConfigureAwait(false);

        await connection
            .ExecuteAsync(UpdateCarClassesQuery, new {CarClasses = joinsTable.AsTableValuedParameter(CarClassType)})
            .ConfigureAwait(false);
    }
}