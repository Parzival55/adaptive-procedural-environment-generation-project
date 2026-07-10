using UnityEngine;

public class GenerationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GridRenderer gridRenderer;

    [Header("Environment Theme")]
    [SerializeField] private EnvironmentTheme currentTheme;

    private void Start()
    {
        GenerateEnvironment();
    }

    public void GenerateEnvironment()
    {
        if (gridRenderer != null && currentTheme != null)
        {
            gridRenderer.SetTheme(currentTheme);
        }

        gridManager.GenerateEnvironment();
    }

    public void SetTheme(EnvironmentTheme theme)
    {
        currentTheme = theme;
    }
}