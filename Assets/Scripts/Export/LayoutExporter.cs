using UnityEngine;
using System.IO;
public static class LayoutExporter
{
    //Exports the layout data to a JSON file in the Exports folder
    // Export for dev use elsewhere?
    public static void Export(LayoutData layout, string fileName)
    {
        string json = JsonUtility.ToJson(layout, true);

        string folder = Path.Combine(Application.dataPath, "../Exports");

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        string path = Path.Combine(folder, fileName + ".json");

        File.WriteAllText(path, json);

        Debug.Log($"Layout exported successfully:\n{path}");
    }
}