using UnityEngine;

public static class ThemeDecorator
{
    public static void Decorate(
        GridCell[,] grid,
        EnvironmentTheme theme,
        Transform parent,
        float cellSize)
    {
        if (theme == null)
            return;

        if (theme.DecorationPrefabs == null)
            return;

        if (theme.DecorationPrefabs.Length == 0)
            return;

        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        bool[,] occupied = new bool[width, height];

        for (int x = 2; x < width - 2; x++)
        {
            for (int z = 2; z < height - 2; z++)
            {
                GridCell cell = grid[x, z];

                if (cell.Type != CellType.Floor)
                    continue;

                if (occupied[x, z])
                    continue;

                // Lower chance = cleaner rooms
                if (Random.value > 0.03f)
                    continue;

                GameObject prefab =
                    theme.DecorationPrefabs[
                        Random.Range(0,
                        theme.DecorationPrefabs.Length)];

                if (prefab == null)
                    continue;

                Vector3 position =
                    new Vector3(
                        x * cellSize,
                        0f,
                        z * cellSize);

                Object.Instantiate(
                    prefab,
                    position,
                    Quaternion.Euler(
                        0f,
                        Random.Range(0, 4) * 90f,
                        0f),
                    parent);

                // Reserve nearby cells
                for (int ox = -2; ox <= 2; ox++)
                {
                    for (int oz = -2; oz <= 2; oz++)
                    {
                        int nx = x + ox;
                        int nz = z + oz;

                        if (nx < 0 || nz < 0 ||
                            nx >= width || nz >= height)
                            continue;

                        occupied[nx, nz] = true;
                    }
                }
            }
        }
    }
}