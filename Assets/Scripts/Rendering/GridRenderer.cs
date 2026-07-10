using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridManager gridManager;

    [SerializeField] private EnvironmentTheme currentTheme;

    public void SetTheme(EnvironmentTheme theme)
    {
        currentTheme = theme;
    }

    public void ClearEnvironment()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RenderGrid()
    {
        if (gridManager == null)
        {
            Debug.LogError("GridManager reference missing.");
            return;
        }

        if (gridManager.Grid == null)
        {
            Debug.LogError("Grid has not been generated.");
            return;
        }

        if (currentTheme == null)
        {
            Debug.LogError("No Environment Theme assigned.");
            return;
        }

        ClearEnvironment();

        for (int x = 0; x < gridManager.Width; x++)
        {
            for (int z = 0; z < gridManager.Height; z++)
            {
                GridCell cell = gridManager.Grid[x, z];

                GameObject prefab = null;

                switch (cell.Type)
                {
                    case CellType.Floor:
                    case CellType.Corridor:
                        prefab = currentTheme.FloorPrefab;
                        break;

                    case CellType.Wall:
                        prefab = currentTheme.WallPrefab;
                        break;
                }

                if (prefab == null)
                    continue;

                Instantiate(
                    prefab,
                    new Vector3(x, 0, z),
                    Quaternion.identity,
                    transform);
            }
        }
    }
}