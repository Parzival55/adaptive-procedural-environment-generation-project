using UnityEngine;
using TMPro;

public class ToolkitUIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Dropdown themeDropdown;
    [SerializeField] private TMP_Dropdown profileDropdown;

    [Header("Themes")]
    [SerializeField] private EnvironmentTheme medievalTheme;
    [SerializeField] private EnvironmentTheme caveTheme;

    [Header("References")]
    [SerializeField] private GridRenderer gridRenderer;
    [SerializeField] private GridManager gridManager;

    public void Generate()
    {
        // Theme
        switch (themeDropdown.value)
        {
            case 0:
                gridRenderer.SetTheme(medievalTheme);
                break;

            case 1:
                gridRenderer.SetTheme(caveTheme);
                break;
        }

        // Gameplay Profile
        switch (profileDropdown.value)
        {
            case 0:
                gridManager.SetProfile(GenerationProfile.Combat);
                break;

            case 1:
                gridManager.SetProfile(GenerationProfile.Exploration);
                break;

            case 2:
                gridManager.SetProfile(GenerationProfile.Survival);
                break;

            case 3:
                gridManager.SetProfile(GenerationProfile.Extraction);
                break;
        }

        gridManager.GenerateEnvironment();
 
}

    private void SetProfile(GenerationProfile profile)
    {
        var field = typeof(GridManager).GetField(
            "profile",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance);

        if (field != null)
        {
            field.SetValue(gridManager, profile);
        }
    }
}