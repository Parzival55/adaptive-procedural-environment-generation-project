using UnityEngine;
using System;

[Serializable]
public class BenchmarkResult
{
    public GenerationProfile Profile;

    public int Iterations;

    public float AverageGenerationTime;
    public float FastestGenerationTime;
    public float SlowestGenerationTime;

    public float AverageRooms;

    public float AverageFloorTiles;
    public float AverageCorridorTiles;
    public float AverageWallTiles;
}