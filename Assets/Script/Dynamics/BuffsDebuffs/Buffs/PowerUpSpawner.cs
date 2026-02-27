using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct PowerUpSpawnEntry
    {
        public PowerUpPool pool;
    }

    [SerializeField] private PowerUpSpawnEntry[] powerUps;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float minYOffset = -2f;
    [SerializeField] private float maxYOffset = 2f;
    [SerializeField] private float moveSpeed = 2f;

    private float timer;

    private void Update()
    {
        if (!GameManager.Instance.isPlaying) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp()
    {
        if (powerUps.Length == 0) return;

        int index = Random.Range(0, powerUps.Length);
        var entry = powerUps[index];
        if (entry.pool == null) return;

        float yOffset = Random.Range(minYOffset, maxYOffset);
        Vector3 spawnPos = transform.position + new Vector3(0, yOffset, 0);

        PowerUp pu = entry.pool.Get(spawnPos, Quaternion.identity);
        Rigidbody2D rb = pu.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.left * moveSpeed;
    }
}