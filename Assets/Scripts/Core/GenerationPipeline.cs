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

        // Generate rooms
        RoomGenerator roomGenerator =
            new RoomGenerator(
                settings.GridWidth,
                settings.GridHeight);

        rooms = roomGenerator.GenerateRooms(
            settings.RoomCount,
            settings.MinimumRoomSize,
            settings.MaximumRoomSize);

        // Carve rooms
        foreach (Room room in rooms)
        {
            RoomCarver.CarveRoom(grid, room);
        }

        // Corridors
        CorridorGenerator corridorGenerator =
            new CorridorGenerator();

        corridorGenerator.GenerateCorridors(
            grid,
            rooms);

        // Walls
        WallGenerator wallGenerator =
            new WallGenerator();

        wallGenerator.GenerateWalls(grid);

        return grid;
    }
}