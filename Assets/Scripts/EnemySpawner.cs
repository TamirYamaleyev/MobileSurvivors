using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public string[] enemyTags = new string[] { };
    public float spawnInterval = 2f;
    private float timer;

    public Transform player; // reference to the player
    public Vector3 spawnOffset = new Vector3(5f, 0f, 0f); // configurable offset from player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null)
            return;

        timer += Time.deltaTime;
        if (timer > spawnInterval)
        {
            timer = 0f;

            // Pick a random tag from the list
            string tagToSpawn = enemyTags[Random.Range(0, enemyTags.Length)];

            // Randomize offset slightly
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnOffset.x, spawnOffset.x),
                spawnOffset.y,
                Random.Range(-spawnOffset.z, spawnOffset.z)
            );

            Vector3 spawnPos = player.position + randomOffset;

            ObjectPooler.Instance.SpawnFromPool(tagToSpawn, spawnPos, Quaternion.identity);
        }
    }
}
