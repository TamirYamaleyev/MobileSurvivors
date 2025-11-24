using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveLoadPanelController : MonoBehaviour
{
    public SaveSlot[] slots;    // All 5 slots
    public GameObject panel;    // The panel itself
    public Transform player;    // Player object
    private PlayerController playerController;

    void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    void OnEnable()
    {
        RefreshSlots();
    }

    // Updates all slots UI
    void RefreshSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            string path = Path.Combine(Application.persistentDataPath, "Save" + i + ".json");
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                slots[i].SetSlot(data.saveName);
            }
            else
            {
                slots[i].SetSlot(null);
            }

            int index = i; // Capture for lambda
            slots[i].saveButton.onClick.RemoveAllListeners();
            slots[i].saveButton.onClick.AddListener(() => SaveToSlot(index));

            slots[i].loadButton.onClick.RemoveAllListeners();
            slots[i].loadButton.onClick.AddListener(() => LoadFromSlot(index));
        }
    }

    // Save current player data to slot
    public void SaveToSlot(int slotIndex)
    {
        SaveData data = new SaveData();
        data.saveName = "Save " + (slotIndex + 1);
        data.sceneName = SceneManager.GetActiveScene().name;
        data.playerPosition = new float[3]
        {
            player.position.x,
            player.position.y,
            player.position.z
        };
        data.playerHealth = playerController.CurrentHealth;
        data.playerScore = playerController.Score;

        SaveSystem.SaveGame(slotIndex, data);
        RefreshSlots();
    }

    // Load player data from slot
    public void LoadFromSlot(int slotIndex)
    {
        SaveData data = SaveSystem.LoadGame(slotIndex);
        if (data == null) return;

        // Load scene if different
        if (SceneManager.GetActiveScene().name != data.sceneName)
        {
            SceneManager.LoadScene(data.sceneName);
            // Optionally: load player position after scene loads
            // You can add a callback or use a singleton to store SaveData until scene is loaded
        }

        // Set player state
        player.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
        playerController.LoadHealth(data.playerHealth);
        playerController.LoadScore(data.playerScore);

        Debug.Log("Loaded slot " + slotIndex);
    }

    public void ShowPanel() => panel.SetActive(true);
    public void HidePanel() => panel.SetActive(false);
}
