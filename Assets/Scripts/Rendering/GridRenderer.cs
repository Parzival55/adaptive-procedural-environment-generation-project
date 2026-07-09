using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridManager gridManager;

    [Header("Prefabs")]
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject wallPrefab;

    public void ClearEnvironment()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RenderGrid()
    {

        Debug.Log("GridRenderer using GridManager: " + gridManager.gameObject.name);

        if (gridManager == null)
        {
            Debug.LogError("GridManager reference is missing!");
            return;
        }

        if (gridManager.Grid == null)
        {
            Debug.LogError("Grid has not been created!");
            return;
        }

        ClearEnvironment();

        // debugging object reference
   
    ClearEnvironment();

        for (int x = 0; x < gridManager.Width; x++)
        {
            for (int z = 0; z < gridManager.Height; z++)
            {
                GridCell cell = gridManager.Grid[x, z];

                GameObject prefab = null;
                Vector3 position = new Vector3(x, 0, z);

                switch (cell.Type)
                {
                    case CellType.Floor:
                    case CellType.Corridor:
                        prefab = floorPrefab;
                        break;

                    case CellType.Wall:
                        prefab = wallPrefab;
                        break;
                }

                if (prefab == null)
                    continue;

                Instantiate(
                    prefab,
                    position,
                    Quaternion.identity,
                    transform);
            }
        }
    }
}