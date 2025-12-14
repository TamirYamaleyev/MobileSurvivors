using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public string[] enemyTags;
    public float spawnInterval = 2f;

    [Header("References")]
    public Transform player;
    public Camera mainCamera;

    [Header("Spawn Settings")]
    [Tooltip("Fixed height for enemies")]
    public float spawnHeight = 1f;

    private float timer;

    // Cached camera size
    private float camWidth;
    private float camHeight;

    // Track screen orientation
    private ScreenOrientation lastOrientation;

    void Start()
    {
        if (!mainCamera)
            mainCamera = Camera.main;

        if (!player)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        lastOrientation = Screen.orientation;
        RecalculateSpawnBounds();
    }

    void Update()
    {
        if (!player || enemyTags.Length == 0)
            return;

        // Check for orientation change
        if (Screen.orientation != lastOrientation)
        {
            lastOrientation = Screen.orientation;
            RecalculateSpawnBounds();
        }

        // Spawn timer
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    void RecalculateSpawnBounds()
    {
        camHeight = mainCamera.orthographicSize * 2f;
        camWidth = camHeight * mainCamera.aspect;
        // Optional: Debug.Log($"Camera bounds updated: width={camWidth}, height={camHeight}");
    }

    void SpawnEnemy()
    {
        string tagToSpawn = enemyTags[Random.Range(0, enemyTags.Length)];
        Vector3 spawnPos = GetSpawnPositionOutsideScreen();

        ObjectPooler.Instance.SpawnFromPool(tagToSpawn, spawnPos, Quaternion.identity);
    }

    Vector3 GetSpawnPositionOutsideScreen()
    {
        Vector3 camCenter = mainCamera.transform.position;
        float halfWidth = camWidth / 2f;

        // Left or right spawn
        bool spawnLeft = Random.value < 0.5f;

        float spawnX = spawnLeft
            ? camCenter.x - halfWidth - camWidth   // spawn one camera width left
            : camCenter.x + halfWidth + camWidth;  // spawn one camera width right

        return new Vector3(spawnX, spawnHeight, 0f);
    }

    // Optional: visualize spawn bounds in editor
    void OnDrawGizmosSelected()
    {
        if (!mainCamera) return;

        Vector3 camCenter = mainCamera.transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(camCenter, new Vector3(camWidth * 3f, camHeight, 1f));
    }
}
