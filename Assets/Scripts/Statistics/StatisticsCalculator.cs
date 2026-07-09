using UnityEngine;
using System.Diagnostics;

public static class StatisticsCalculator
{
    public static GenerationStatistics Calculate(
        GridCell[,] grid,
        int roomCount,
        GenerationProfile profile,
        float generationTime)
    {
        GenerationStatistics stats = new GenerationStatistics();

        stats.Profile = profile;

        stats.GridWidth = grid.GetLength(0);
        stats.GridHeight = grid.GetLength(1);

        stats.RoomCount = roomCount;

        foreach (GridCell cell in grid)
        {
            switch (cell.Type)
            {
                case CellType.Floor:
                    stats.FloorTiles++;
                    break;

                case CellType.Corridor:
                    stats.CorridorTiles++;
                    break;

                case CellType.Wall:
                    stats.WallTiles++;
                    break;
            }
        }

        stats.GenerationTime = generationTime;

        return stats;
    }
}
