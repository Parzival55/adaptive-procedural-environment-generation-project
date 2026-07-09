using UnityEngine;

[CreateAssetMenu(
    fileName = "Gameplay Definition",
    menuName = "Environment Toolkit/Gameplay Definition")]
public class GameplayDefinition : ScriptableObject
{
    [Header("Core Spaces")]

    public bool GenerateSpawn = true;

    public bool GenerateExit = false;

    public bool GenerateObjective = false;

    public bool GenerateRewardAreas = false;

    public bool GenerateHubSpaces = true;

    public bool GenerateLandmarks = false;

    [Header("Connectivity")]

    public bool AllowBranches = true;

    public bool AllowLoops = true;

    [Range(0f, 1f)]
    public float Connectivity = 0.5f;

    [Header("Environment Size")]

    public int MinimumSpaces = 8;

    public int MaximumSpaces = 15;
}