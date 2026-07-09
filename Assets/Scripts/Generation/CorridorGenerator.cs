using UnityEngine;
using System.Collections.Generic;

public class CorridorGenerator
{
    private const int CorridorWidth = 2;

    public void GenerateCorridors(GridCell[,] grid, List<Room> rooms)
    {
        if (rooms.Count < 2)
            return;

        // Main path
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            ConnectRooms(grid, rooms[i], rooms[i + 1]);
        }

        // Extra random loops
        int extraConnections = Mathf.Max(1, rooms.Count / 3);

        for (int i = 0; i < extraConnections; i++)
        {
            int a = Random.Range(0, rooms.Count);
            int b = Random.Range(0, rooms.Count);

            if (a == b)
                continue;

            ConnectRooms(grid, rooms[a], rooms[b]);
        }
    }

    private void ConnectRooms(GridCell[,] grid, Room roomA, Room roomB)
    {
        Vector2Int start = GetClosestDoor(roomA, roomB.Center);
        Vector2Int end = GetClosestDoor(roomB, roomA.Center);

        CreateCorridor(grid, start, end);
    }

    private Vector2Int GetClosestDoor(Room room, Vector2Int target)
    {
        Vector2Int[] doors =
        {
            room.NorthDoor,
            room.SouthDoor,
            room.EastDoor,
            room.WestDoor
        };

        Vector2Int closest = doors[0];
        float bestDistance = Vector2Int.Distance(closest, target);

        for (int i = 1; i < doors.Length; i++)
        {
            float distance = Vector2Int.Distance(doors[i], target);

            if (distance < bestDistance)
            {
                bestDistance = distance;
                closest = doors[i];
            }
        }

        return closest;
    }

    private void CreateCorridor(GridCell[,] grid, Vector2Int start, Vector2Int end)
    {
        int x = start.x;
        int z = start.y;

        while (x != end.x)
        {
            CarveCorridor(grid, x, z);
            x += (end.x > x) ? 1 : -1;
        }

        while (z != end.y)
        {
            CarveCorridor(grid, x, z);
            z += (end.y > z) ? 1 : -1;
        }

        CarveCorridor(grid, x, z);
    }

    private void CarveCorridor(GridCell[,] grid, int x, int z)
    {
        int halfWidth = CorridorWidth / 2;

        for (int offsetX = -halfWidth; offsetX <= halfWidth; offsetX++)
        {
            for (int offsetZ = -halfWidth; offsetZ <= halfWidth; offsetZ++)
            {
                int newX = x + offsetX;
                int newZ = z + offsetZ;

                if (newX < 0 || newX >= grid.GetLength(0))
                    continue;

                if (newZ < 0 || newZ >= grid.GetLength(1))
                    continue;

                grid[newX, newZ].Type = CellType.Corridor;
            }
        }
    }
}