namespace Luciferin.Core.Helpers;

public static class ExtensionHelper
{
    /// <summary>
    /// Gets a consistent hash from a two valued ValueTuple.
    /// </summary>
    /// <param name="valueTuple">The value tuple.</param>
    /// <typeparam name="T1">Value 1.</typeparam>
    /// <typeparam name="T2">Value 2.</typeparam>
    /// <returns></returns>
    public static ulong GetConsistentHash<T1, T2>(this ValueTuple<T1, T2> valueTuple)
    {
        var (item1, item2) = valueTuple;
        var hashableString = $"{item1}{item2}";
        return HashHelper.CalculateKnuthHash(hashableString);
    }

    /// <summary>
    /// Gets a consistent hash from a three valued ValueTuple.
    /// </summary>
    /// <param name="valueTuple">The value tuple.</param>
    /// <typeparam name="T1">Value 1.</typeparam>
    /// <typeparam name="T2">Value 2.</typeparam>
    /// <typeparam name="T3">Value 3.</typeparam>
    /// <returns></returns>
    public static ulong GetConsistentHash<T1, T2, T3>(this ValueTuple<T1, T2, T3> valueTuple)
    {
        var (item1, item2, item3) = valueTuple;
        var hashableString = $"{item1}{item2}{item3}";
        return HashHelper.CalculateKnuthHash(hashableString);
    }

    /// <summary>
    /// Gets a consistent hash from a three valued ValueTuple.
    /// </summary>
    /// <param name="valueTuple">The value tuple.</param>
    /// <typeparam name="T1">Value 1.</typeparam>
    /// <typeparam name="T2">Value 2.</typeparam>
    /// <typeparam name="T3">Value 3.</typeparam>
    /// <typeparam name="T4">Value 4.</typeparam>
    /// <returns></returns>
    public static ulong GetConsistentHash<T1, T2, T3, T4>(this ValueTuple<T1, T2, T3, T4> valueTuple)
    {
        var (item1, item2, item3, item4) = valueTuple;
        var hashableString = $"{item1}{item2}{item3}{item4}";
        return HashHelper.CalculateKnuthHash(hashableString);
    }

    /// <summary>
    /// Gets a consistent hash from a three valued ValueTuple.
    /// </summary>
    /// <param name="valueTuple">The value tuple.</param>
    /// <typeparam name="T1">Value 1.</typeparam>
    /// <typeparam name="T2">Value 2.</typeparam>
    /// <typeparam name="T3">Value 3.</typeparam>
    /// <typeparam name="T4">Value 4.</typeparam>
    /// <typeparam name="T5">Value 5.</typeparam>
    /// <returns></returns>
    public static ulong GetConsistentHash<T1, T2, T3, T4, T5>(this ValueTuple<T1, T2, T3, T4, T5> valueTuple)
    {
        var (item1, item2, item3, item4, item5) = valueTuple;
        var hashableString = $"{item1}{item2}{item3}{item4}{item5}";
        return HashHelper.CalculateKnuthHash(hashableString);
    }
}