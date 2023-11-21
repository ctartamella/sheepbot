// using SheepBot.Models;
// using SheepBot.Models.Enums;
// using SheepBot.Repositories.Tests.Base;
//
// namespace SheepBot.Repositories.Tests.Repositories;
//
// [Collection("Transactional")]
// public class RaceTests : ModelTestBase<Race>
// {
//     public RaceTests() : base(unitOfWork => unitOfWork.RaceRepository)
//     {
//     }
//
//     protected override Task<IEnumerable<Race>> CreateEntityListAsync()
//     {
//         var dataSize = NextRandom();
//         var results = Enumerable.Range(1, dataSize)
//             .Select(i => new Race
//             {
//                 Length = i,
//                 Unit = (LengthUnit)(i % Enum.GetValues<LengthUnit>().Length) + 1,
//                 PracticeTime = DateTimeOffset.UtcNow.AddDays(1),
//                 QualiTime = DateTimeOffset.UtcNow.AddDays(2)
//             });
//         
//         return Task.FromResult(results);
//     }
//
//     protected override async Task<Race> CreateEntityAsync()
//     {
//         var idx = NextRandom();
//         var role = new Role { RoleName = $"Test Role {idx}" };
//         var roleId = await UnitOfWork.RoleRepository.InsertAsync(role);
//         
//         var series = new Series { Name = $"Test Series {idx}", Type = SeriesType.Oval, RoleId = roleId };
//         var seriesId = await UnitOfWork.SeriesRepository.InsertAsync(series);
//         
//         var track = new Track { Name = $"Test Track {idx}" };
//         var trackId = await UnitOfWork.TrackRepository.InsertAsync(track);
//         
//         var result = new Race
//         {
//             Length = idx,
//             Unit = (LengthUnit)((idx % Enum.GetValues<LengthUnit>().Length) + 1),
//             PracticeTime = DateTimeOffset.UtcNow.AddDays(1),
//             QualiTime = DateTimeOffset.UtcNow.AddDays(2),
//             SeriesId = seriesId,
//             TrackId = trackId
//         };
//         
//         return result;
//     }
//     
//     protected override async Task<Race> UpdateEntity(Race entity)
//     {
//         var role = new Role { RoleName = "New Role" };
//         var roleId = await UnitOfWork.RoleRepository.InsertAsync(role);
//
//         var newSeries = new Series { Name = "New Series", RoleId = roleId, Type = SeriesType.Oval};
//         var newSeriesId = await UnitOfWork.SeriesRepository.InsertAsync(newSeries);
//
//         var newTrack = new Track { Name = "New Track" };
//         var newTrackId = await UnitOfWork.TrackRepository.InsertAsync(newTrack);
//
//         return entity with
//         {
//             PracticeTime = entity.PracticeTime?.AddDays(1),
//             QualiTime = entity.QualiTime.AddDays(1),
//             Length = +1,
//             SeriesId = newSeriesId,
//             TrackId = newTrackId,
//             Unit = entity.Unit is null
//                 ? null
//                 : (LengthUnit)(((int)entity.Unit + 1) % Enum.GetValues<LengthUnit>().Length + 1)
//         };
//     }
// }