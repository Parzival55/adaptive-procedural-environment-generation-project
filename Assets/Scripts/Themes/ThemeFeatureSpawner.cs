using System.Collections.Generic;
using UnityEngine;

public static class ThemeFeatureSpawner
{
    public static void SpawnFeatures(
        List<Room> rooms,
        EnvironmentTheme theme,
        Transform parent,
        float cellSize)
    {
        if (rooms == null || theme == null)
            return;

        foreach (Room room in rooms)
        {
            GameObject[] prefabs = GetPrefabs(room.Feature, theme);

            if (prefabs == null || prefabs.Length == 0)
                continue;

            GameObject prefab =
                prefabs[Random.Range(0, prefabs.Length)];

            if (prefab == null)
                continue;

            Vector2Int centre = room.Center;

            Vector2 randomOffset = Random.insideUnitCircle * 1.5f;

            Vector3 position = new Vector3(
                (centre.x + randomOffset.x) * cellSize,
                0f,
                (centre.y + randomOffset.y) * cellSize);

            Quaternion rotation =
                Quaternion.Euler(
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

    private static GameObject[] GetPrefabs(
        RoomFeature feature,
        EnvironmentTheme theme)
    {
        switch (feature)
        {
            case RoomFeature.Spawn:
                return theme.SpawnRoomPrefabs;

            case RoomFeature.Hub:
                return theme.HubRoomPrefabs;

            case RoomFeature.Reward:
                return theme.RewardRoomPrefabs;

            case RoomFeature.Objective:
                return theme.ObjectiveRoomPrefabs;

            case RoomFeature.Exit:
                return theme.ExitRoomPrefabs;

            default:
                return null;
        }
    }
}