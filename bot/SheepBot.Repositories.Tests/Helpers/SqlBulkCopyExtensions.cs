using System.Reflection;
using Microsoft.Data.SqlClient;

namespace SheepBot.Repositories.Tests.Helpers;

public static class SqlBulkCopyExtensions
{
    private static FieldInfo? _rowsCopiedField = null;

    /// <summary>
    /// Gets the rows copied from the specified SqlBulkCopy object
    /// </summary>
    /// <param name="bulkCopy">The bulk copy.</param>
    /// <returns></returns>
    public static int GetRowsCopied(this SqlBulkCopy bulkCopy)
    {
        if (_rowsCopiedField == null)
        {
            _rowsCopiedField = typeof(SqlBulkCopy).GetField("_rowsCopied", BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance);
        }

        return (int?) _rowsCopiedField?.GetValue(bulkCopy) ?? 0;
    }
}