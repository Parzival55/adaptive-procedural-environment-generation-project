using TMPro;
using UnityEngine;

public class StatisticsUI : MonoBehaviour
{
    [Header("UI References")]

    [SerializeField] private TMP_Text profileText;
    [SerializeField] private TMP_Text roomText;
    [SerializeField] private TMP_Text floorText;
    [SerializeField] private TMP_Text corridorText;
    [SerializeField] private TMP_Text wallText;
    [SerializeField] private TMP_Text generationTimeText;

    public void UpdateStatistics(GenerationStatistics stats)
    {
        profileText.text = $"Profile: {stats.Profile}";

        roomText.text = $"Rooms: {stats.RoomCount}";

        floorText.text = $"Floor Tiles: {stats.FloorTiles}";

        corridorText.text = $"Corridor Tiles: {stats.CorridorTiles}";

        wallText.text = $"Wall Tiles: {stats.WallTiles}";

        generationTimeText.text =
            $"Generation Time: {stats.GenerationTime:F2} ms";
    }
}