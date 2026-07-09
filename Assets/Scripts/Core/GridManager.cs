using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int width = 50;
    [SerializeField] private int height = 50;

    [Header("Generation Settings")]
    [SerializeField] private int roomCount = 10;
    [SerializeField] private int minimumRoomSize = 4;
    [SerializeField] private int maximumRoomSize = 8;

    [Header("Adaptive Profile")]
    [SerializeField] private GenerationProfile profile = GenerationProfile.Exploration;

    [Header("Seed Settings")]
    [SerializeField] private bool useRandomSeed = true;
    [SerializeField] private int seed = 12345;

    [Header("References")]
    [SerializeField] private GridRenderer gridRenderer;
    [SerializeField] private StatisticsDisplay statisticsDisplay;

    [Header("Generation Request")]
    [SerializeField] private GenerationRequest request;

    public int Width => width;
    public int Height => height;

    public GridCell[,] Grid { get; private set; }

    private List<Room> rooms;

    private readonly GenerationPipeline generationPipeline = new GenerationPipeline();

    public GenerationStatistics GenerateEnvironment(bool render = true)
    {

        EnvironmentPlanner planner = new EnvironmentPlanner();

        GameplayDefinition gameplay = null;

        if (request != null)
        {
            gameplay = request.Gameplay;
        }

        EnvironmentPlan plan =
            planner.CreatePlan(gameplay);

        PlanningDebugger.Print(plan);

        UnityEngine.Debug.Log("Generating new environment...");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Initialise seed
        if (request.UseRandomSeed)
        {
            SeedManager.GenerateRandomSeed();
        }
        else
        {
            SeedManager.SetSeed(request.Seed);
        }

        // Get adaptive settings
        GenerationSettings settings = ProfileManager.GetSettings(profile);

        // Apply settings
        width = settings.GridWidth;
        height = settings.GridHeight;

        roomCount = settings.RoomCount;
        minimumRoomSize = settings.MinimumRoomSize;
        maximumRoomSize = settings.MaximumRoomSize;

        // Clear previous environment only if rendering
        if (render)
            gridRenderer.ClearEnvironment();

        // Generate environment
        Grid = generationPipeline.Generate(
            settings,
            out rooms);

        // Render only if requested
        if (render)
            gridRenderer.RenderGrid();

        stopwatch.Stop();

        // Calculate statistics
        GenerationStatistics statistics =
            StatisticsCalculator.Calculate(
                Grid,
                rooms.Count,
                profile,
                stopwatch.ElapsedMilliseconds);

        // Update UI only if rendering
        if (render && statisticsDisplay != null)
        {
            statisticsDisplay.UpdateDisplay(
                statistics,
                SeedManager.CurrentSeed);
        }

        LayoutData layout =
            LayoutConverter.Convert(
              Grid,
              SeedManager.CurrentSeed,
              profile);

        LayoutExporter.Export(
            layout,
            "LatestLayout");

        // Console output
        UnityEngine.Debug.Log(
$@"===== Generation Statistics =====

Profile: {statistics.Profile}

Seed: {SeedManager.CurrentSeed}

Grid Size: {statistics.GridWidth} x {statistics.GridHeight}

Rooms: {statistics.RoomCount}

Floor Tiles: {statistics.FloorTiles}

Corridor Tiles: {statistics.CorridorTiles}

Wall Tiles: {statistics.WallTiles}

Generation Time: {statistics.GenerationTime} ms

================================");

        return statistics;
    }
}