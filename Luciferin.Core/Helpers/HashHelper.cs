using System.Security.Cryptography;
using System.Text;

namespace Luciferin.Core.Helpers;

public static class HashHelper
{
    /// <summary>
    /// Calculates a Knuth hash from a string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns></returns>
    public static ulong CalculateKnuthHash(string input)
    {
        var hashedValue = 3074457345618258791ul;
        foreach (var c in input)
        {
            hashedValue += c;
            hashedValue *= 3074457345618258799ul;
        }

        return hashedValue;
    }

    /// <summary>
    /// Calculates the SHA-512 hash for an input string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns></returns>
    public static string CalculateSha512(string input)
    {
        using var sha = SHA512.Create();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sha.ComputeHash(inputBytes);
        return Convert.ToHexString(hashBytes);
    }
}