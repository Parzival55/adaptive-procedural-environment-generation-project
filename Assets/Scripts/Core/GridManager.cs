using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int width = 50;
    [SerializeField] private int height = 50;

    public int Width => width;
    public int Height => height;

    public GridCell[,] Grid { get; private set; }

    [Header("References")]
    [SerializeField] private GridRenderer renderer;

    // Store the generated rooms so other systems can use them
    private List<Room> rooms;

    private void Start()
    {
        CreateGrid();

        renderer.RenderGrid();
    }

    private void CreateGrid()
    {
        // Create the grid
        Grid = new GridCell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Grid[x, z] = new GridCell(x, z);
            }
        }

        Debug.Log("Grid Created");

        // Generate rooms - depending on theme choices later on
        RoomGenerator roomGenerator = new RoomGenerator(width, height);

        rooms = roomGenerator.GenerateRooms(
            roomCount: 10,
            minSize: 4,
            maxSize: 8
        );

        Debug.Log($"Generated {rooms.Count} rooms.");

        // Carve rooms into the grid - rooms dependent on theme choices later on
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

        // Connect rooms together
        CorridorGenerator corridorGenerator = new CorridorGenerator();
        corridorGenerator.GenerateCorridors(Grid, rooms);
    }
}