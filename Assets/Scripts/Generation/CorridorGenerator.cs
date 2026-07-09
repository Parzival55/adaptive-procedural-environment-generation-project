using System.Collections.Generic;
using UnityEngine;

public class CorridorGenerator
{
    public void GenerateCorridors(GridCell[,] grid, List<Room> rooms)
    {
        if (rooms.Count < 2)
            return;

        for (int i = 0; i < rooms.Count - 1; i++)
        {
            Vector2Int start = GetRoomCentre(rooms[i]);
            Vector2Int end = GetRoomCentre(rooms[i + 1]);

            CreateCorridor(grid, start, end);
        }
    }

    private Vector2Int GetRoomCentre(Room room)
    {
        return new Vector2Int(
            room.X + room.Width / 2,
            room.Z + room.Height / 2);
    }

    private void CreateCorridor(
        GridCell[,] grid,
        Vector2Int start,
        Vector2Int end)
    {
        int x = start.x;
        int z = start.y;

        while (x != end.x)
        {
            grid[x, z].Type = CellType.Corridor;

            x += (end.x > x) ? 1 : -1;
        }

        while (z != end.y)
        {
            grid[x, z].Type = CellType.Corridor;

            z += (end.y > z) ? 1 : -1;
        }

        grid[x, z].Type = CellType.Corridor;
    }
}