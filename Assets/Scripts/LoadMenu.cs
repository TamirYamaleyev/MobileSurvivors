using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadMenu : MonoBehaviour
{
    public Transform buttonParent; // Assign ScrollView -> Content
    public GameObject buttonPrefab;

    void OnEnable()
    {
        PopulateSaveButtons();
    }

    void PopulateSaveButtons()
    {
        // Clear previous buttons, but keep Back button
        foreach (Transform child in buttonParent)
        {
            if (child.CompareTag("KeepButton")) // Tag the Back button as "KeepButton"
                continue;
            Destroy(child.gameObject);
        }

        // Get all save files
        string[] saves = Directory.GetFiles(Application.persistentDataPath, "*.json");

        foreach (string path in saves)
        {
            string fileName = Path.GetFileNameWithoutExtension(path);

            // Instantiate button
            GameObject btn = Instantiate(buttonPrefab, buttonParent);
            btn.GetComponentInChildren<Text>().text = fileName;

            // Add click listener
            btn.GetComponent<Button>().onClick.AddListener(() => LoadGame(fileName));
        }
    }

    void LoadGame(string saveName)
    {
        string path = Path.Combine(Application.persistentDataPath, saveName + ".json");
        if (!File.Exists(path))
        {
            Debug.LogWarning("Save not found: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // Load the saved scene
        SceneManager.LoadScene(data.sceneName);

        // Apply player data after scene loads (via GameManager or singleton)
        GameManager.Instance.SetPlayerFromSave(data);
    }
}
