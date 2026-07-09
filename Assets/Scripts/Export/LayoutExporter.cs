using UnityEngine;
using System.IO;

public static class LayoutExporter
{
    public static void Export(LayoutData layout, string fileName)
    {
        string json = JsonUtility.ToJson(layout, true);

        string directory = Path.Combine(Application.dataPath, "../Exports");

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        string path = Path.Combine(directory, fileName + ".json");

        File.WriteAllText(path, json);

        Debug.Log($"Layout exported to:\n{path}");
    }
}