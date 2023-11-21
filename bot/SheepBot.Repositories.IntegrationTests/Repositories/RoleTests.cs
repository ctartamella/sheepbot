// using SheepBot.Models;
// using SheepBot.Repositories.Tests.Base;
//
// namespace SheepBot.Repositories.Tests.Repositories;
//
// [Collection("Transactional")]
// public class RoleTests : ModelTestBase<Role>
// {
//     public RoleTests() : base(unitOfWork => unitOfWork.RoleRepository)
//     {
//     }
//
//     protected override Task<IEnumerable<Role>> CreateEntityListAsync()
//     {
//         var dataSize = NextRandom();
//         var results = Enumerable.Range(1, dataSize)
//             .Select(i => new Role
//             {
//                 DiscordId = i,
//                 RoleName = $"Test Role {i}"
//             });
//
//         return Task.FromResult(results);
//     }
//
//     protected override Task<Role> CreateEntityAsync()
//     {
//         var idx = NextRandom();
//         var result = new Role
//         {
//             DiscordId = idx,
//             RoleName = $"Test Role {idx}"
//         };
//         
//         return Task.FromResult(result);
//     }
//     
//     protected override Task<Role> UpdateEntity(Role entity)
//     {
//         return Task.FromResult(entity with
//         {
//             DiscordId = + 1,
//             RoleName = "New Role"
//         });
//     }
// }