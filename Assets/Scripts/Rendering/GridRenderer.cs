using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    [Header("References")]
    public GridManager gridManager;

    [Header("Visuals")]
    public GameObject floorPrefab;

    public void RenderGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int x = 0; x < gridManager.Width; x++)
        {
            for (int z = 0; z < gridManager.Height; z++)
            {
                GridCell cell = gridManager.Grid[x, z];

                GameObject tile = Instantiate(
                    floorPrefab,
                    new Vector3(x, 0, z),
                    Quaternion.identity,
                    transform);

                tile.name = $"Cell ({x},{z})";
            }
        }
    }
}