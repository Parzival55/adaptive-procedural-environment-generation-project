using UnityEngine;

public class GenerationController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private void Start()
    {
        GenerateEnvironment();
    }

    public void GenerateEnvironment()
    {
        gridManager.GenerateEnvironment();
    }
}