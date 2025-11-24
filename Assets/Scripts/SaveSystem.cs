using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, data.saveName + ".json");
        File.WriteAllText(path, json);
        Debug.Log("Saved to: " + path);
    }

    public static SaveData LoadGame(string saveName)
    {
        string path = Path.Combine(Application.persistentDataPath, saveName + ".json");
        if (!File.Exists(path))
        {
            Debug.LogWarning("Save file not found: " + path);
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public static string[] GetAllSaves()
    {
        return Directory.GetFiles(Application.persistentDataPath, "*.json");
    }
}
