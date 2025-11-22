using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public float spawnInterval = 2f;
    private float timer;
    public float spawnHeight = 1f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnInterval)
        {
            timer = 0f;
            Vector3 randomPos = new Vector3(Random.Range(-10, 10), spawnHeight, Random.Range(-10, 10));
            ObjectPooler.Instance.SpawnFromPool(enemyTag, randomPos, Quaternion.identity);
        }
    }
}
