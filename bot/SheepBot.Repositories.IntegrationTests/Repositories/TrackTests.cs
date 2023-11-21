// using SheepBot.Models;
// using SheepBot.Repositories.Tests.Base;
//
// namespace SheepBot.Repositories.Tests.Repositories;
//
// [Collection("Transactional")]
// public class TrackTests : ModelTestBase<Track>
// {
//     public TrackTests() : base(unitOfWork => unitOfWork.TrackRepository)
//     {
//     }
//
//     protected override Task<IEnumerable<Track>> CreateEntityListAsync()
//     {
//         var dataSize = NextRandom();
//         var result = Enumerable.Range(1, dataSize)
//             .Select(i => new Track
//             {
//                 Name = $"Track {i}",
//                 IsFree = Convert.ToBoolean(i % 2),
//                 IsLegacy = !Convert.ToBoolean(i % 2)
//             });
//
//         return Task.FromResult(result);
//     }
//
//     protected override Task<Track> CreateEntityAsync()
//     {
//         var idx = NextRandom();
//         var boolValue = Convert.ToBoolean(idx % 2);
//         var result = new Track
//         {
//             Name = $"Track {idx}",
//             IsFree = boolValue,
//             IsLegacy = !boolValue
//         };
//
//         return Task.FromResult(result);
//     }
//     
//     protected override Task<Track> UpdateEntity(Track entity)
//     {
//         return Task.FromResult(entity with
//         {
//             Name = "New Car Class",
//             IsFree = entity.IsLegacy,
//             IsLegacy = entity.IsFree
//         });
//     }
// }