using UnityEngine;

public static class LayoutConverter
{
    public static LayoutData Convert(
        GridCell[,] grid,
        int seed,
        GenerationProfile profile)
    {
        LayoutData layout = new LayoutData();

        layout.Width = grid.GetLength(0);
        layout.Height = grid.GetLength(1);

        layout.Seed = seed;
        layout.Profile = profile;

        for (int x = 0; x < layout.Width; x++)
        {
            for (int z = 0; z < layout.Height; z++)
            {
                CellData cell = new CellData();

                cell.X = x;
                cell.Z = z;
                cell.Type = grid[x, z].Type;

                layout.Cells.Add(cell);
            }
        }

        return layout;
    }
}