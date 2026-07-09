using TMPro;
using UnityEngine;

public class StatisticsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text statisticsText;

    public void UpdateDisplay(
        GenerationStatistics stats,
        int seed)
    {
        statisticsText.text =
$@"Adaptive Procedural Environment Generation Framework

Profile: {stats.Profile}

Seed: {seed}

Grid Size: {stats.GridWidth} x {stats.GridHeight}

Rooms: {stats.RoomCount}

Floor Tiles: {stats.FloorTiles}

Corridor Tiles: {stats.CorridorTiles}

Wall Tiles: {stats.WallTiles}

Generation Time: {stats.GenerationTime:F2} ms";
    }
}
