[System.Serializable]
public class EnemyData
{
    public string enemyType;      // Optional if you have multiple enemy prefabs
    public float[] position;      // x, y, z
    public float health;
}

[System.Serializable]
public class SaveData
{
    public string saveName;
    public string sceneName;
    public float[] playerPosition; // x, y, z
    public float playerHealth;
    public int playerScore;

    // Enemies
    public EnemyData[] enemies;
}
