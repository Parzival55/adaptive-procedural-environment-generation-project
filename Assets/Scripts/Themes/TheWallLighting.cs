using UnityEngine;

public static class ThemeWallLighting
{
    public static void TryPlaceWallLight(
        EnvironmentTheme theme,
        Transform parent,
        Vector3 wallPosition,
        Quaternion wallRotation)
    {
        if (theme == null)
            return;

        if (theme.WallLightPrefabs == null)
            return;

        if (theme.WallLightPrefabs.Length == 0)
            return;

        if (Random.value > theme.WallLightChance)
            return;

        GameObject prefab =
            theme.WallLightPrefabs[
                Random.Range(0, theme.WallLightPrefabs.Length)];

        if (prefab == null)
            return;

        Vector3 forward = wallRotation * Vector3.forward;

        float height =
            theme.WallLightHeight * theme.WallHeightMultiplier;

        Vector3 position =
            wallPosition +
            forward * theme.WallLightOffset +
            Vector3.up * height;

        Quaternion rotation = wallRotation;

        Object.Instantiate(
            prefab,
            position,
            rotation,
            parent);
    }
}