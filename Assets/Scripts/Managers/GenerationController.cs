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
        //Debug.Log("Generate button pressed!");

        gridManager.GenerateEnvironment();
    }
}