// using SheepBot.Models;
// using SheepBot.Repositories.Tests.Base;
//
// namespace SheepBot.Repositories.Tests.Repositories;
//
// [Collection("Transactional")]
// public class CarClassTests : ModelTestBase<Class>
// {
//     public CarClassTests() : base(unitOfWork => unitOfWork.CarClassRepository)
//     {
//         
//     }
//
//     protected override Task<IEnumerable<Class>> CreateEntityListAsync()
//     {
//         var dataSize = NextRandom();
//         var result = Enumerable.Range(1, dataSize)
//             .Select(i => new Class
//             {
//                 Name = $"Test Class {i}"
//             });
//         return Task.FromResult(result);
//     }
//
//     protected override Task<Class> CreateEntityAsync()
//     {
//         var idx = NextRandom();
//         var result = new Class
//         {
//             Name = $"Test Class {idx}"
//         };
//
//         return Task.FromResult(result);
//     }
//     
//     protected override Task<Class> UpdateEntity(Class entity)
//     {
//         return Task.FromResult(entity with { Name = "New Car Class"});
//     }
// }