using System.Collections.Generic;
using UnityEngine;

public class GenerationPipeline
{
    public GridCell[,] Generate(
        GenerationSettings settings,
        out List<Room> rooms)
    {
        GridCell[,] grid = new GridCell[
            settings.GridWidth,
            settings.GridHeight];

        // Create empty grid
        for (int x = 0; x < settings.GridWidth; x++)
        {
            for (int z = 0; z < settings.GridHeight; z++)
            {
                grid[x, z] = new GridCell(x, z);
            }
        }

        // -----------------------------
        // Create a default gameplay definition
        // (Temporary until requests are fully integrated)
        // -----------------------------

        GameplayDefinition gameplay =
            ScriptableObject.CreateInstance<GameplayDefinition>();

        gameplay.GenerateSpawn = true;
        gameplay.GenerateExit = false;
        gameplay.GenerateObjective = false;
        gameplay.GenerateRewardAreas = true;
        gameplay.GenerateHubSpaces = true;
        gameplay.GenerateLandmarks = true;

        gameplay.AllowBranches = true;
        gameplay.AllowLoops = true;

        gameplay.MinimumSpaces = settings.RoomCount - 2;
        gameplay.MaximumSpaces = settings.RoomCount + 2;

        // -----------------------------
        //        logical plan
        // -----------------------------

        EnvironmentPlanner planner =
            new EnvironmentPlanner();

        EnvironmentPlan plan =
            planner.CreatePlan(gameplay);

        // Debug
        PlanningDebugger.Print(plan);

        // -----------------------------
        // Build rooms from the plan
        // -----------------------------

        EnvironmentBuilder builder =
            new EnvironmentBuilder();

        rooms = builder.Build(
            plan,
            settings.GridWidth,
            settings.GridHeight);

        // -----------------------------
        // Carve rooms
        // -----------------------------

        foreach (Room room in rooms)
        {
            RoomCarver.CarveRoom(grid, room);
        }

        // -----------------------------
        // Corridors
        // -----------------------------

        CorridorGenerator corridorGenerator =
            new CorridorGenerator();

        corridorGenerator.GenerateCorridors(
            grid,
            rooms);

        // -----------------------------
        // Walls
        // -----------------------------

        WallGenerator wallGenerator =
            new WallGenerator();

        wallGenerator.GenerateWalls(grid);

        return grid;
    }
}