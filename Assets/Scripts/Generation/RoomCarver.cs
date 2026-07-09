using UnityEngine;

public static class RoomCarver
{
    public static void CarveRoom(GridCell[,] grid, Room room)
    {
        switch (room.Shape)
        {
            case RoomShape.Rectangle:
                CarveRectangle(grid, room);
                break;

            case RoomShape.LShape:
                CarveLShape(grid, room);
                break;

            case RoomShape.LargeHall:
                CarveLargeHall(grid, room);
                break;

            default:
                CarveRectangle(grid, room);
                break;
        }

        ApplyFeature(grid, room);
    }

    private static void CarveRectangle(GridCell[,] grid, Room room)
    {
        for (int x = room.X; x < room.X + room.Width; x++)
        {
            for (int z = room.Z; z < room.Z + room.Height; z++)
            {
                grid[x, z].Type = CellType.Floor;
            }
        }
    }

    private static void CarveLShape(GridCell[,] grid, Room room)
    {
        CarveRectangle(grid, room);

        int cutX = room.X + room.Width / 2;
        int cutZ = room.Z + room.Height / 2;

        for (int x = cutX; x < room.X + room.Width; x++)
        {
            for (int z = cutZ; z < room.Z + room.Height; z++)
            {
                grid[x, z].Type = CellType.Empty;
            }
        }
    }

    private static void CarveLargeHall(GridCell[,] grid, Room room)
    {
        CarveRectangle(grid, room);
    }

    private static void ApplyFeature(GridCell[,] grid, Room room)
    {
        switch (room.Feature)
        {
            case RoomFeature.Pillars:
                ApplyPillars(grid, room);
                break;
        }
    }

    private static void ApplyPillars(GridCell[,] grid, Room room)
    {
        for (int x = room.X + 2; x < room.X + room.Width - 2; x += 3)
        {
            for (int z = room.Z + 2; z < room.Z + room.Height - 2; z += 3)
            {
                grid[x, z].Type = CellType.Wall;
            }
        }
    }
}