using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class LayoutData
{
    public int Width;
    public int Height;

    public int Seed;

    public GenerationProfile Profile;

    public List<CellData> Cells = new List<CellData>();
}
