using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake() => Instance = this;

    public PlayerController player;

    public void SetPlayerFromSave(SaveData data)
    {
        player.transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
        player.LoadHealth(data.playerHealth);
    }
}
