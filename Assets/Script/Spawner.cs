using UnityEngine;

[System.Serializable]
public class PooledObstacleData
{
    public ObstaclePool pool;
    public float yOffset;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private PooledObstacleData[] obstacleData;
    public float obstacleSpawnTime = 2f;
    public float obstacleSpeed = 1f;

    private float timeUntilObstacleSpawn;

    private void Update()
    {
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

        var selected = obstacleData[Random.Range(0, obstacleData.Length)];
        if (selected.pool == null) return;

        Vector3 spawnPos = transform.position + new Vector3(0, selected.yOffset, 0);
        Obstacle obstacle = selected.pool.GetObstacle(spawnPos, Quaternion.identity);

        Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.left * obstacleSpeed;
    }
}