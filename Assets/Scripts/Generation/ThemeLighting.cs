using UnityEngine;

public static class ThemeLighting
{
    public static void PlaceLights(
        GridCell[,] grid,
        EnvironmentTheme theme,
        Transform parent,
        float cellSize)
    {
        if (theme == null)
            return;

        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int x = 2; x < width - 2; x++)
        {
            for (int z = 2; z < height - 2; z++)
            {
                if (grid[x, z].Type != CellType.Floor)
                    continue;

                // Don't place beside walls

                if (AdjacentToWall(grid, x, z))
                    continue;

                // Small room lighting

                if (theme.RoomLights != null &&
                    theme.RoomLights.Length > 0 &&
                    Random.value < theme.RoomLightChance)
                {
                    SpawnRandom(
                        theme.RoomLights,
                        x,
                        z,
                        parent,
                        cellSize);

                    continue;
                }

                // Large feature lighting / asset store - Kenney's assets

                if (theme.LargeRoomLights != null &&
                    theme.LargeRoomLights.Length > 0 &&
                    Random.value < theme.LargeRoomLightChance * 0.05f)
                {
                    SpawnRandom(
                        theme.LargeRoomLights,
                        x,
                        z,
                        parent,
                        cellSize);

                    continue;
                }

                // Feature lights assets imported from the asset store - Kenneys Assets

                if (theme.FeatureLights != null &&
                    theme.FeatureLights.Length > 0 &&
                    Random.value < 0.01f)
                {
                    SpawnRandom(
                        theme.FeatureLights,
                        x,
                        z,
                        parent,
                        cellSize);
                }
            }
        }
    }

    private static void SpawnRandom(
        GameObject[] prefabs,
        int x,
        int z,
        Transform parent,
        float cellSize)
    {
        GameObject prefab =
            prefabs[Random.Range(0, prefabs.Length)];

        if (prefab == null)
            return;

        Vector3 position =
            new Vector3(
                x * cellSize,
                0,
                z * cellSize);

        Object.Instantiate(
            prefab,
            position,
            Quaternion.Euler(
                0,
                Random.Range(0, 4) * 90,
                0),
            parent);
    }

    private static bool AdjacentToWall(
        GridCell[,] grid,
        int x,
        int z)
    {
        for (int dx = -2; dx <= 2; dx++)
        {
            for (int dz = -2; dz <= 2; dz++)
            {
                if (grid[x + dx, z + dz].Type == CellType.Wall)
                    return true;
            }
        }

        return false;
    }
}