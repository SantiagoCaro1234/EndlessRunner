using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ObstacleSpawnData[] obstacleData;
    public float obstacleSpawnTime = 2f;
    public float obstacleSpeed = 1f;

    private float timeUntilObstacleSpawn;
    private ObstacleSpawnData lastSelected; // para controlar repeticion

    private void Update()
    {
        if (!GameManager.Instance.isPlaying) return;

        timeUntilObstacleSpawn += Time.deltaTime;
        if (timeUntilObstacleSpawn >= obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        if (obstacleData.Length == 0) return;

        ObstacleSpawnData selected = null;
        int attempts = 0;
        int maxAttempts = 10;

        do
        {
            selected = obstacleData[Random.Range(0, obstacleData.Length)];
            attempts++;
            if (attempts >= maxAttempts)
            {
                selected = lastSelected ?? obstacleData[0];
                break;
            }
        } while (!selected.config.repeatable && selected == lastSelected && obstacleData.Length > 1);

        if (selected.pool == null || selected.config == null) return;

        Vector3 spawnPos = transform.position + new Vector3(0, selected.config.yOffset, 0);
        Obstacle obstacle = selected.pool.GetObstacle(spawnPos, Quaternion.identity);

        Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.left * obstacleSpeed;

        lastSelected = selected;
    }
}