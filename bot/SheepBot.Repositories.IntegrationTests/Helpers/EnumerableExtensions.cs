namespace SheepBot.Repositories.Tests.Helpers;

public static class EnumerableExtensions
{
    // Returns true if the lists are equal.
    public static bool CompareUnordered<TModel, TKey>(this IEnumerable<TModel> first, IEnumerable<TModel> second, Func<TModel, TKey> keySelector)
    {
        var sortedFirst = first.OrderBy(keySelector);
        var sortedSecond = second.OrderBy(keySelector);

        var tuples = sortedFirst.Zip(sortedSecond).ToList();
        var temp = tuples.Where(t => t.First != null && !t.First.Equals(t.Second));
        return !tuples.Any(t => t.First != null && !t.First.Equals(t.Second));
    }
}