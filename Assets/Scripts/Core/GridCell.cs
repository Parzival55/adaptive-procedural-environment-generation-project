using UnityEngine;

public class GridCell
{
    public Vector2Int Position;
    public CellType Type;

    public GridCell(int x, int z)
    {
        Position = new Vector2Int(x, z);
        Type = CellType.Empty;
    }
}