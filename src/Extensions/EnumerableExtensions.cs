namespace XperienceCommunity.Essentials.Extensions;

public static class EnumerableExtensions
{
    /// <summary>
    /// Determines whether any element in the first collection exists in the second collection.
    /// </summary>
    /// <param name="source">The source collection</param>
    /// <param name="target">The target collection to compare against</param>
    /// <returns>True if any element from source exists in target; otherwise, false</returns>
    public static bool ContainsAny(this IEnumerable<string> source, IEnumerable<string> target)
    {
        if (source == null || target == null)
        {
            return false;
        }

        return source.Any(item => target.Contains(item));
    }

    /// <summary>
    /// Determines whether any element in either collection exists in the other collection (bidirectional check).
    /// </summary>
    /// <param name="source">The source collection</param>
    /// <param name="target">The target collection to compare against</param>
    /// <returns>True if any element from source exists in target OR any element from target exists in source; otherwise, false</returns>
    public static bool HasAnyMatch(this IEnumerable<string> source, IEnumerable<string> target)
    {
        if (source == null || target == null)
        {
            return false;
        }

        return source.Any(item => target.Contains(item)) || target.Any(item => source.Contains(item));
    }

    /// <summary>
    /// Gets a property value from the first item in the collection using a selector function.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection</typeparam>
    /// <typeparam name="TResult">The type of the result</typeparam>
    /// <param name="source">The source collection</param>
    /// <param name="selector">Function to select the desired property from the first item</param>
    /// <param name="defaultValue">Default value to return if collection is null/empty or selector returns null</param>
    /// <returns>The selected value or default value</returns>
    public static TResult GetFirstOrDefault<T, TResult>(
        this IEnumerable<T>? source,
        Func<T, TResult?> selector,
        TResult defaultValue = default!)
    {
        if (source == null)
        {
            return defaultValue;
        }

        var firstItem = source.FirstOrDefault();
        if (firstItem == null)
        {
            return defaultValue;
        }

        var result = selector(firstItem);
        return result ?? defaultValue;
    }

    /// <summary>
    /// Gets a string property value from the first item in the collection using a selector function.
    /// Returns empty string if null or not found.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection</typeparam>
    /// <param name="source">The source collection</param>
    /// <param name="selector">Function to select the desired property from the first item</param>
    /// <returns>The selected string value or empty string</returns>
    public static string GetFirstOrEmpty<T>(
        this IEnumerable<T>? source,
        Func<T, object?> selector)
    {
        if (source == null)
        {
            return string.Empty;
        }

        var firstItem = source.FirstOrDefault();
        if (firstItem == null)
        {
            return string.Empty;
        }

        object? result = selector(firstItem);
        return result?.ToString() ?? string.Empty;
    }
}
