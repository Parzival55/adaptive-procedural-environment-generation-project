using UnityEngine;

[System.Serializable]
public class GenerationStatistics
{
    public GenerationProfile Profile;

    public int GridWidth;
    public int GridHeight;

    public int RoomCount;

    public int FloorTiles;
    public int CorridorTiles;
    public int WallTiles;

    public float GenerationTime;
}