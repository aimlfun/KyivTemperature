using System.Diagnostics;

namespace Temperature;

/// <summary>
/// Provides pseudo-random numbers, so we can run the same neural network, with the same weights and biases.
/// </summary>
internal static class ReproduceablePseudoRandomNumberGenerator
{
    /// <summary>
    /// Seed for the pseudo-random number generator.
    /// </summary>
    internal const int c_seedForPseudoRandomNumberGenerator = 123; // 31415926 555 v

    /// <summary>
    /// Random number generator, that is predictable.
    /// </summary>
    private static Random s_random = new(c_seedForPseudoRandomNumberGenerator);

    internal static void SetSeed(int seed)
    {
        s_random = new Random(seed);
    }

    /// <summary>
    /// Returns a non-negative random integer.
    /// </summary>
    /// <returns></returns>
    internal static double GetNextRandomDouble()
    {
        return s_random.NextDouble();
    }

    /// <summary>
    /// Returns a non-negative random integer.
    /// </summary>
    /// <returns></returns>
    internal static int GetNextRandomInt()
    {
        return s_random.Next();
    }

    /// <summary>
    /// Generate a pseudo random number between -0.5...+0.5.
    /// </summary>
    /// <returns></returns>
    internal static float GetRandomFloatBetweenMinusHalfToPlusHalf()
    {
        return (float)(GetNextRandomDouble() - 0.5f);
    }

    /// <summary>
    /// Generate a pseudo random number between -1...+1.
    /// </summary>
    /// <returns></returns>
    internal static float GetRandomFloatBetweenPlusOrMinus1()
    {
        return (float)(GetNextRandomDouble() * 2 - 1f);
    }
}