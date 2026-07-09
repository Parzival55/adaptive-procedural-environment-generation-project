using UnityEngine;

public class WallGenerator
{
    public void GenerateWalls(GridCell[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int x = 1; x < width - 1; x++)
        {
            for (int z = 1; z < height - 1; z++)
            {
                if (grid[x, z].Type != CellType.Empty)
                    continue;

                if (ShouldBecomeWall(grid, x, z))
                {
                    grid[x, z].Type = CellType.Wall;
                }
            }
        }
    }

    private bool ShouldBecomeWall(GridCell[,] grid, int x, int z)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dz = -1; dz <= 1; dz++)
            {
                CellType neighbour = grid[x + dx, z + dz].Type;

                if (neighbour == CellType.Floor ||
                    neighbour == CellType.Corridor)
                {
                    return true;
                }
            }
        }

        return false;
    }
}