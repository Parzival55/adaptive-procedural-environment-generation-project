using UnityEngine;

[CreateAssetMenu(
    fileName = "New Environment Theme",
    menuName = "Environment Toolkit/Environment Theme")]
public class EnvironmentTheme : ScriptableObject
{
    [Header("General")]
    public string ThemeName = "New Theme";

    [Header("Materials")]
    public Material FloorMaterial;
    public Material WallMaterial;

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