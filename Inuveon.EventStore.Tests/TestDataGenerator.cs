using System.Text;

namespace Inuveon.EventStore.Tests;

public static class TestDataGenerator
{
    private static readonly Random random = new();

    private static readonly string[] firstNames =
    {
        "Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace", "Hannah", "Ivan", "Julia", "Kyle", "Liam", "Mia",
        "Noah", "Olivia", "Pablo", "Quinn", "Rachel", "Sophia", "Thomas", "Uma", "Victor", "Wendy", "Xavier", "Yara",
        "Zach"
    };

    /// <summary>
    ///     Generates a random first name.
    /// </summary>
    /// <returns>A random first name.</returns>
    public static string RandomFirstName()
    {
        return firstNames[random.Next(firstNames.Length)];
    }

    /// <summary>
    ///     Generates a random email address.
    /// </summary>
    /// <returns>A random email address.</returns>
    public static string RandomEmail()
    {
        var prefix = RandomString(5);
        var domain = RandomString(5);
        return $"{prefix}@{domain}.com";
    }

    /// <summary>
    ///     Generates a random string of a given length.
    /// </summary>
    /// <param name="length">The length of the string to generate.</param>
    /// <returns>A random string of the specified length.</returns>
    private static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        var builder = new StringBuilder();
        for (var i = 0; i < length; i++) builder.Append(chars[random.Next(chars.Length)]);
        return builder.ToString();
    }
}