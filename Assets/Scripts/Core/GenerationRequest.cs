using UnityEngine;

[System.Serializable]
public class GenerationRequest
{
    public GenerationProfile Profile;

    public GameplayDefinition Gameplay;

    public EnvironmentTheme Theme;

    public bool UseRandomSeed = true;

    public int Seed = 12345;
}