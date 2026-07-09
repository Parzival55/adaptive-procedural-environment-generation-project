using UnityEngine;

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

        Debug.Log("Grid Created");
    }
}