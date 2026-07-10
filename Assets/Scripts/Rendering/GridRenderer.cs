using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridManager gridManager;

    [SerializeField] private EnvironmentTheme currentTheme;

    [Header("World Settings")]
    [SerializeField] private float cellSize = 1f;

    public void SetTheme(EnvironmentTheme theme)
    {
        currentTheme = theme;
    }

    public void ClearEnvironment()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    public void RenderGrid()
    {
        if (gridManager == null ||
            gridManager.Grid == null ||
            currentTheme == null)
            return;

        ClearEnvironment();

        GridCell[,] grid = gridManager.Grid;

        for (int x = 0; x < gridManager.Width; x++)
        {
            for (int z = 0; z < gridManager.Height; z++)
            {
                GridCell cell = grid[x, z];

                GameObject prefab = null;
                Quaternion rotation = Quaternion.identity;

                switch (cell.Type)
                {
                    case CellType.Floor:

                        prefab = currentTheme.FloorPrefab;

                        if (currentTheme.SecondaryFloorPrefab != null &&
                            Random.value < currentTheme.SecondaryFloorChance)
                        {
                            prefab = currentTheme.SecondaryFloorPrefab;
                        }

                        break;

                    case CellType.Corridor:

                        prefab = currentTheme.CorridorFloorPrefab != null
                            ? currentTheme.CorridorFloorPrefab
                            : currentTheme.FloorPrefab;

                        break;

                    case CellType.Wall:

                        if (currentTheme.CornerWallPrefab != null &&
                            IsCorner(grid, x, z))
                        {
                            prefab = currentTheme.CornerWallPrefab;
                            rotation = GetCornerRotation(grid, x, z);
                        }
                        else
                        {
                            prefab = currentTheme.WallPrefab;

                            if (currentTheme.SecondaryWallPrefab != null &&
                                Random.value < currentTheme.SecondaryWallChance)
                            {
                                prefab = currentTheme.SecondaryWallPrefab;
                            }

                            rotation = GetWallRotation(grid, x, z);
                        }

                        break;
                }

                if (prefab == null)
                    continue;

                Vector3 position = new Vector3(
                    x * cellSize,
                    0f,
                    z * cellSize);

                // Theme-specific wall offset
                if (cell.Type == CellType.Wall)
                {
                    position += rotation *
                                Vector3.forward *
                                currentTheme.WallForwardOffset;
                }

                GameObject instance = Instantiate(
                    prefab,
                    position,
                    rotation,
                    transform);

                if (cell.Type == CellType.Wall)
                {
                    Vector3 scale = instance.transform.localScale;

                    scale.y *= currentTheme.WallHeightMultiplier;

                    instance.transform.localScale = scale;

                    ThemeWallLighting.TryPlaceWallLight(
                        currentTheme,
                        transform,
                        position,
                        rotation);
                }

                if (cell.Type == CellType.Corridor &&
                    currentTheme.GatewayPrefab != null &&
                    IsCorridorEnd(grid, x, z))
                {
                    Instantiate(
                        currentTheme.GatewayPrefab,
                        position,
                        GetGatewayRotation(grid, x, z),
                        transform);
                }
            }
        }

        ThemeDecorator.Decorate(
            grid,
            currentTheme,
            transform,
            cellSize);
    }

    private bool IsCorridorEnd(GridCell[,] grid, int x, int z)
    {
        int corridorNeighbours = 0;

        if (IsCorridor(grid, x + 1, z)) corridorNeighbours++;
        if (IsCorridor(grid, x - 1, z)) corridorNeighbours++;
        if (IsCorridor(grid, x, z + 1)) corridorNeighbours++;
        if (IsCorridor(grid, x, z - 1)) corridorNeighbours++;

        return corridorNeighbours == 1;
    }

    private Quaternion GetGatewayRotation(GridCell[,] grid, int x, int z)
    {
        if (IsCorridor(grid, x + 1, z))
            return Quaternion.identity;

        if (IsCorridor(grid, x - 1, z))
            return Quaternion.Euler(0, 180, 0);

        if (IsCorridor(grid, x, z + 1))
            return Quaternion.Euler(0, 90, 0);

        if (IsCorridor(grid, x, z - 1))
            return Quaternion.Euler(0, 270, 0);

        return Quaternion.identity;
    }

    private Quaternion GetWallRotation(GridCell[,] grid, int x, int z)
    {
        if (IsWalkable(grid, x, z + 1))
            return Quaternion.Euler(0f, 180f, 0f);

        if (IsWalkable(grid, x, z - 1))
            return Quaternion.Euler(0f, 0f, 0f);

        if (IsWalkable(grid, x + 1, z))
            return Quaternion.Euler(0f, 270f, 0f);

        if (IsWalkable(grid, x - 1, z))
            return Quaternion.Euler(0f, 90f, 0f);

        return Quaternion.identity;
    }

    private bool IsCorner(GridCell[,] grid, int x, int z)
    {
        bool north = IsWall(grid, x, z + 1);
        bool south = IsWall(grid, x, z - 1);
        bool east = IsWall(grid, x + 1, z);
        bool west = IsWall(grid, x - 1, z);

        return (north && east) ||
               (east && south) ||
               (south && west) ||
               (west && north);
    }

    private Quaternion GetCornerRotation(GridCell[,] grid, int x, int z)
    {
        bool north = IsWall(grid, x, z + 1);
        bool south = IsWall(grid, x, z - 1);
        bool east = IsWall(grid, x + 1, z);
        bool west = IsWall(grid, x - 1, z);

        if (north && east)
            return Quaternion.Euler(0, 270, 0);

        if (east && south)
            return Quaternion.Euler(0, 0, 0);

        if (south && west)
            return Quaternion.Euler(0, 90, 0);

        if (west && north)
            return Quaternion.Euler(0, 180, 0);

        return Quaternion.identity;
    }

    private bool IsCorridor(GridCell[,] grid, int x, int z)
    {
        if (x < 0 || z < 0 ||
            x >= grid.GetLength(0) ||
            z >= grid.GetLength(1))
            return false;

        return grid[x, z].Type == CellType.Corridor;
    }

    private bool IsWall(GridCell[,] grid, int x, int z)
    {
        if (x < 0 || z < 0 ||
            x >= grid.GetLength(0) ||
            z >= grid.GetLength(1))
            return false;

        return grid[x, z].Type == CellType.Wall;
    }

    private bool IsWalkable(GridCell[,] grid, int x, int z)
    {
        if (x < 0 || z < 0 ||
            x >= grid.GetLength(0) ||
            z >= grid.GetLength(1))
            return false;

        return grid[x, z].Type == CellType.Floor ||
               grid[x, z].Type == CellType.Corridor;
    }
}