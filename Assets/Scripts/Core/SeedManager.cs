using UnityEngine;

public static class SeedManager
{
    public static int CurrentSeed { get; private set; }

    public static void SetSeed(int seed)
    {
        CurrentSeed = seed;
        Random.InitState(seed);
    }

    public static void GenerateRandomSeed()
    {
        SetSeed(System.Environment.TickCount);
    }
}