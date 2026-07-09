using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width = 50;
    [SerializeField] private int height = 50;

    public int Width => width;
    public int Height => height;

    public GridCell[,] Grid { get; private set; }

    [SerializeField] private GridRenderer renderer;

    void Start()
    {
        CreateGrid();

        renderer.RenderGrid();
    }

    void CreateGrid()
    {
        Grid = new GridCell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Grid[x, z] = new GridCell(x, z);
            }
        }

        RoomGenerator roomGenerator = new RoomGenerator(width, height);

        List<Room> rooms = roomGenerator.GenerateRooms(
            10,
            4,
            8
        );

        Debug.Log($"Room Count: {rooms.Count}");

        Debug.Log("Grid Created");
    }

}