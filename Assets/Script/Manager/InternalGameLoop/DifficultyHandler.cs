using UnityEngine;

public class DifficultyHandler : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private DifficultyLevel[] levels; // ordenados por minScore ascendente

    private int currentLevelIndex = -1;

    private void Start()
    {
        // si no se asigno spawner, lo busca en la escena
        if (spawner == null)
            spawner = FindObjectOfType<Spawner>();

        if (spawner == null)
        {
            Debug.LogError("DifficultyHandler: no se encontro Spawner en la escena");
            return;
        }

        // se suscribe al evento de game manager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged += OnScoreChanged;
            // aplica el nivel correspondiente al score actual
            OnScoreChanged(GameManager.Instance.currentScore);
        }
        else
        {
            Debug.LogError("DifficultyHandler: GameManager.Instance es null");
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(float currentScore)
    {
        int newIndex = GetLevelIndexForScore(currentScore);
        if (newIndex != currentLevelIndex)
        {
            currentLevelIndex = newIndex;
            ApplyLevel(levels[newIndex]);
        }
    }

    private int GetLevelIndexForScore(float score)
    {
        // recorre de menor a mayor y devuelve el indice del ultimo nivel cuyo minScore sea <= score
        int index = 0;
        for (int i = 0; i < levels.Length; i++)
        {
            if (score >= levels[i].minScore)
                index = i;
            else
                break;
        }
        return index;
    }

    private void ApplyLevel(DifficultyLevel level)
    {
        if (spawner == null) return;
        spawner.obstacleSpeed = level.obstacleSpeed;
        spawner.obstacleSpawnTime = level.obstacleSpawnTime;
        Debug.Log($"Dificultad cambiada a nivel con minScore {level.minScore}: speed={level.obstacleSpeed}, spawnTime={level.obstacleSpawnTime}");
    }
}