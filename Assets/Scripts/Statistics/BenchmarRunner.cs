using UnityEngine;

public class BenchmarkRunner : MonoBehaviour
{

    private void Start()
    {
        RunBenchmark();
    }

    [SerializeField] private GridManager gridManager;

    [SerializeField] private int iterations = 100;

    public void RunBenchmark()
    {
        float totalTime = 0;

        float fastest = float.MaxValue;
        float slowest = 0;

        float totalRooms = 0;
        float totalFloor = 0;
        float totalCorridors = 0;
        float totalWalls = 0;

        for (int i = 0; i < iterations; i++)
        {
            GenerationStatistics stats =
                gridManager.GenerateEnvironment(false);

            totalTime += stats.GenerationTime;

            totalRooms += stats.RoomCount;
            totalFloor += stats.FloorTiles;
            totalCorridors += stats.CorridorTiles;
            totalWalls += stats.WallTiles;

            if (stats.GenerationTime < fastest)
                fastest = stats.GenerationTime;

            if (stats.GenerationTime > slowest)
                slowest = stats.GenerationTime;
        }

        BenchmarkResult result = new BenchmarkResult();

        result.Profile = GenerationProfile.Exploration;
        result.Iterations = iterations;

        result.AverageGenerationTime = totalTime / iterations;
        result.FastestGenerationTime = fastest;
        result.SlowestGenerationTime = slowest;

        result.AverageRooms = totalRooms / iterations;
        result.AverageFloorTiles = totalFloor / iterations;
        result.AverageCorridorTiles = totalCorridors / iterations;
        result.AverageWallTiles = totalWalls / iterations;

        PrintResult(result);
    }

    private void PrintResult(BenchmarkResult result)
    {
        Debug.Log(
$@"========== Benchmark ==========

Iterations: {result.Iterations}

Average Time:
{result.AverageGenerationTime:F2} ms

Fastest:
{result.FastestGenerationTime:F2} ms

Slowest:
{result.SlowestGenerationTime:F2} ms

Average Rooms:
{result.AverageRooms:F1}

Average Floor:
{result.AverageFloorTiles:F1}

Average Corridors:
{result.AverageCorridorTiles:F1}

Average Walls:
{result.AverageWallTiles:F1}

==============================");
    }
}