using UnityEngine;

[CreateAssetMenu(
    fileName = "New Environment Theme",
    menuName = "Environment Toolkit/Environment Theme")]
public class EnvironmentTheme : ScriptableObject
{
    [Header("General")]
    public string ThemeName = "New Theme";

    [Header("Floor")]
    public GameObject FloorPrefab;
    public GameObject SecondaryFloorPrefab;

    [Range(0f, 1f)]
    public float SecondaryFloorChance = 0.15f;

    [Header("Corridors")]
    public GameObject CorridorFloorPrefab;

    public GameObject CorridorWallPrefab;

    [Tooltip("Placed where corridors meet rooms.")]
    public GameObject GatewayPrefab;

    [Header("Walls")]
    public GameObject WallPrefab;
    public GameObject SecondaryWallPrefab;
    public GameObject CornerWallPrefab;

    [Range(0f, 1f)]
    public float SecondaryWallChance = 0.15f;

    [Header("Room Features")]
    public GameObject PillarPrefab;

    public GameObject[] DecorationPrefabs;

    [Header("Generation")]

    [Range(0f, 1f)]
    public float LargeHallChance = 0.15f;

    [Range(0f, 1f)]
    public float LShapeChance = 0.20f;

    [Range(0f, 1f)]
    public float PillarChance = 0.25f;

    [Header("Supported Features")]
    public bool UsePillars = true;
    public bool UseHazards = true;
    public bool UseProps = true;

    [Header("Preview")]
    public Color PreviewColour = Color.white;
}