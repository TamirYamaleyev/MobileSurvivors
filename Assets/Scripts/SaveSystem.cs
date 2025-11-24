using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static void SaveGame(int slotIndex, SaveData data)
    {
        string path = Path.Combine(Application.persistentDataPath, "Save" + slotIndex + ".json");
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Saved slot " + slotIndex + " to: " + path);
    }

    public static SaveData LoadGame(int slotIndex)
    {
        string path = Path.Combine(Application.persistentDataPath, "Save" + slotIndex + ".json");
        if (!File.Exists(path))
        {
            Debug.LogWarning("No save found in slot " + slotIndex);
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }
}
