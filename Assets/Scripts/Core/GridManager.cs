using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int width = 50;
    [SerializeField] private int height = 50;

    [Header("Generation Settings")]
    [SerializeField] private int roomCount = 10;
    [SerializeField] private int minimumRoomSize = 4;
    [SerializeField] private int maximumRoomSize = 8;

    [Header("References")]
    [SerializeField] private GridRenderer gridRenderer;

    public int Width => width;
    public int Height => height;

    public GridCell[,] Grid { get; private set; }

    private List<Room> rooms;

    /// <summary>
    /// Creates an empty logical grid.
    /// </summary>
    private void CreateGrid()
    {
        Grid = new GridCell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Grid[x, z] = new GridCell(x, z);
            }
        }

        Debug.Log("Grid Created");
    }

    /// <summary>
    /// Generates a complete procedural environment.
    /// </summary>
    public void GenerateEnvironment()
    {

        Debug.Log("Generating new environment...");
        // Clear the previous environment
        gridRenderer.ClearEnvironment();

        // Create a fresh logical grid
        CreateGrid();

        // Generate rooms
        RoomGenerator roomGenerator = new RoomGenerator(width, height);

        rooms = roomGenerator.GenerateRooms(
            roomCount,
            minimumRoomSize,
            maximumRoomSize);

        // Carve rooms into the grid
        foreach (Room room in rooms)
        {
            for (int x = room.X; x < room.X + room.Width; x++)
            {
                for (int z = room.Z; z < room.Z + room.Height; z++)
                {
                    Grid[x, z].Type = CellType.Floor;
                }
            }
        }

        // Generate corridors
        CorridorGenerator corridorGenerator = new CorridorGenerator();
        corridorGenerator.GenerateCorridors(Grid, rooms);

        // Generate walls
        WallGenerator wallGenerator = new WallGenerator();
        wallGenerator.GenerateWalls(Grid);

        // Render the completed environment
        gridRenderer.RenderGrid();
    }
}