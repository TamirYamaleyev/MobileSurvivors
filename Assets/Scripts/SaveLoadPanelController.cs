using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveLoadPanelController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panel;                 // The Save/Load panel
    public Transform slotContainer;          // Parent of all slots
    public Button backButton;                // Back button to hide panel

    [Header("Player Reference")]
    public PlayerController player;          // Assign your player object here

    private const int slotCount = 5;
    private string saveFolder;

    void Start()
    {
        saveFolder = Application.persistentDataPath + "/saves";
        if (!Directory.Exists(saveFolder))
            Directory.CreateDirectory(saveFolder);

        panel.SetActive(false);
        backButton.onClick.AddListener(HidePanel);

        // Initialize slots
        for (int i = 0; i < slotCount; i++)
        {
            int index = i;
            Transform slot = slotContainer.GetChild(i);

            Button saveBtn = slot.Find("SaveButton").GetComponent<Button>();
            Button loadBtn = slot.Find("LoadButton").GetComponent<Button>();

            saveBtn.onClick.AddListener(() => SaveToSlot(index));
            loadBtn.onClick.AddListener(() => LoadFromSlot(index));
        }
    }

    #region Panel Controls
    public void ShowPanel() => panel.SetActive(true);
    public void HidePanel() => panel.SetActive(false);
    #endregion

    #region Save / Load
    private void SaveToSlot(int slotIndex)
    {
        SaveData data = new SaveData();

        // Player
        data.saveName = "Slot " + (slotIndex + 1);
        data.sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        data.playerPosition = new float[3] { player.transform.position.x, player.transform.position.y, player.transform.position.z };
        data.playerHealth = player.CurrentHealth;
        data.playerScore = player.Score; // make sure your player has a Score field

        // Timer
        data.elapsedTime = FindAnyObjectByType<LevelTimerHUD>().ElapsedTime; // make a public getter

        // Audio
        data.sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        data.bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);

        // Enemies
        EnemyAI[] activeEnemies = ObjectPooler.Instance.GetActiveEnemies();
        data.enemies = new EnemyData[activeEnemies.Length];

        for (int i = 0; i < activeEnemies.Length; i++)
        {
            EnemyAI enemy = activeEnemies[i];
            data.enemies[i] = new EnemyData
            {
                enemyType = enemy.GetComponent<PooledObject>().poolTag,
                position = new float[3] { enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z },
                health = enemy.CurrentHealth
            };
        }

        string json = JsonUtility.ToJson(data, true);
        string path = GetSavePath(slotIndex);
        System.IO.File.WriteAllText(path, json);

        UpdateSlotUI();
    }


    private void LoadFromSlot(int slotIndex)
    {
        string path = GetSavePath(slotIndex);
        if (!System.IO.File.Exists(path)) return;

        string json = System.IO.File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // Player
        player.transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
        player.LoadHealth(data.playerHealth);
        player.LoadScore(data.playerScore);

        // Timer
        LevelTimerHUD timer = FindAnyObjectByType<LevelTimerHUD>();
        if (timer != null)
            timer.SetElapsedTime(data.elapsedTime); // make a public setter

        // Audio
        PlayerPrefs.SetFloat("SFXVolume", data.sfxVolume);
        PlayerPrefs.SetFloat("BGMVolume", data.bgmVolume);
        PlayerPrefs.Save();

        SFXVolumeSlider sfxSlider = FindAnyObjectByType<SFXVolumeSlider>();
        if (sfxSlider != null) sfxSlider.SetSliderValue(data.sfxVolume);

        BGMVolumeSlider bgmSlider = FindAnyObjectByType<BGMVolumeSlider>();
        if (bgmSlider != null) bgmSlider.SetSliderValue(data.bgmVolume);

        // Enemies
        ObjectPooler.Instance.ReturnAllEnemiesToPool();

        foreach (EnemyData eData in data.enemies)
        {
            Vector3 pos = new Vector3(eData.position[0], eData.position[1], eData.position[2]);
            GameObject enemyObj = ObjectPooler.Instance.SpawnEnemy(eData.enemyType, pos, Quaternion.identity);
            EnemyAI ai = enemyObj.GetComponent<EnemyAI>();
            if (ai != null)
                ai.CurrentHealth = eData.health;
        }
    }


    private string GetSavePath(int slotIndex) => Path.Combine(saveFolder, $"slot_{slotIndex + 1}.json");
    #endregion

    #region Slot UI
    private void UpdateSlotUI()
    {
        for (int i = 0; i < slotCount; i++)
        {
            string path = GetSavePath(i);
            Transform slot = slotContainer.GetChild(i);

            if (slot == null)
            {
                Debug.LogWarning($"Slot {i} is missing!");
                continue;
            }

            TMP_Text label = slot.Find("Background/Text")?.GetComponent<TMP_Text>();
            if (label == null)
            {
                Debug.LogWarning($"Label missing in slot {i}");
                continue;
            }

            if (File.Exists(path))
                label.text = $"{i + 1}\n(Saved)";
            else
                label.text = $"{i + 1}\n(Empty)";
        }
    }
    #endregion
}
