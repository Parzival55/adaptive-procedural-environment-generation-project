using UnityEngine;
using System.Collections.Generic;

public static class ThemeFloorLighting
{
    public static void SpawnFloorLights(
        List<Room> rooms,
        EnvironmentTheme theme,
        Transform parent,
        float cellSize)
    {
        if (rooms == null || theme == null)
            return;

        if (theme.FloorLightPrefabs == null ||
            theme.FloorLightPrefabs.Length == 0)
            return;

        foreach (Room room in rooms)
        {
            // Skip tiny rooms
            if (room.Width < 10 || room.Height < 10)
                continue;

            // Chance to place a floor light
            if (Random.value > 0.65f)
                continue;

            GameObject prefab =
                theme.FloorLightPrefabs[
                    Random.Range(0, theme.FloorLightPrefabs.Length)];

            if (prefab == null)
                continue;

            Vector2Int centre = room.Center;

            // Small random offset so everything isn't perfectly centred
            Vector2 offset = Random.insideUnitCircle * 2f;

            Vector3 position = new Vector3(
                (centre.x + offset.x) * cellSize,
                0f,
                (centre.y + offset.y) * cellSize);

            Quaternion rotation = Quaternion.Euler(
                0,
                Random.Range(0, 4) * 90,
                0);

            Object.Instantiate(
                prefab,
                position,
                rotation,
                parent);
        }
    }
}