using UnityEngine;

// Theme for the environment generation system. Contains prefabs and settings for floor, walls, corridors, lighting, and other features.
// 

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
    public GameObject GatewayPrefab;

    [Header("Walls")]
    public GameObject WallPrefab;
    public GameObject SecondaryWallPrefab;
    public GameObject CornerWallPrefab;

    [Header("Corner Placement")]

    [Tooltip("Offset applied only to corner wall prefabs.")]
    public Vector3 CornerOffset = Vector3.zero;

    [Tooltip("Chance of spawning a secondary wall variation.")]
    [Range(0f, 1f)]
    public float SecondaryWallChance = 0.15f;

    [Header("Rendering")]
    [Range(0.5f, 2f)]
    public float WallHeightMultiplier = 1f;

    [Header("Wall Placement")]

    [Tooltip("Moves wall meshes forwards/backwards to compensate for different pivots.")]
    public float WallForwardOffset = 0f;

    [Header("Room Features")]
    public GameObject PillarPrefab;

    [Header("Decorations")]
    public GameObject[] DecorationPrefabs;

    [Header("Lighting")]

    [Tooltip("Small room lighting such as candles or torches.")]
    public GameObject[] RoomLights;

    [Tooltip("Large room lighting such as campfires.")]
    public GameObject[] LargeRoomLights;

    [Tooltip("Theme-specific feature lights such as crystals.")]
    public GameObject[] FeatureLights;

    [Range(0f, 1f)]
    public float RoomLightChance = 0.30f;

    [Range(0f, 1f)]
    public float LargeRoomLightChance = 0.80f;

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

    [Header("Adaptive Room Features")]

    [Tooltip("Placed in Spawn rooms.")]
    public GameObject[] SpawnRoomPrefabs;

    [Tooltip("Placed in Hub rooms.")]
    public GameObject[] HubRoomPrefabs;

    [Tooltip("Placed in Reward rooms.")]
    public GameObject[] RewardRoomPrefabs;

    [Tooltip("Placed in Objective rooms.")]
    public GameObject[] ObjectiveRoomPrefabs;

    [Tooltip("Placed in Exit rooms.")]
    public GameObject[] ExitRoomPrefabs;

    [Header("Preview")]
    public Color PreviewColour = Color.white;

    [Header("Wall Lighting")]

    [Tooltip("Wall mounted torches, sconces etc.")]
    public GameObject[] WallLightPrefabs;

    [Range(0f, 1f)]
    public float WallLightChance = 0.15f;

    public float WallLightHeight = 1.8f;

    [Tooltip("Distance wall-mounted lights are placed away from the wall.")]
    public float WallLightOffset = 0.12f;

    [Header("Floor Lighting")]

    public GameObject[] FloorLightPrefabs;

}