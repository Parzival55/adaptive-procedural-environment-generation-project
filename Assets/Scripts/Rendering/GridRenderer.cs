using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridManager gridManager;

    [Header("Prefabs")]
    [SerializeField] private GameObject floorPrefab;

    public void RenderGrid()
    {
        // Clear previous generation
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Render only floor cells
        for (int x = 0; x < gridManager.Width; x++)
        {
            for (int z = 0; z < gridManager.Height; z++)
            {
                GridCell cell = gridManager.Grid[x, z];

                if (cell.Type != CellType.Floor &&
                    cell.Type != CellType.Corridor)
                {
                    continue;
                }

                GameObject tile = Instantiate(
                    floorPrefab,
                    new Vector3(x, 0f, z),
                    Quaternion.identity,
                    transform);

                tile.name = $"Floor ({x}, {z})";
            }
        }
    }
}