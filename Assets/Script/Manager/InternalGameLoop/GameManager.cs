using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float currentScore = 0f;
    public float scoreMultiplier = 1f;
    public bool isPlaying = false;

    [SerializeField] private GameObject gameOverMenu; // asignar en el inspector

    public event Action<float> OnScoreChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime * scoreMultiplier;
            OnScoreChanged?.Invoke(currentScore);
        }

        if (Input.GetKeyDown(KeyCode.K)) isPlaying = true; // debug
    }

    public void StartGame()
    {
        isPlaying = true;
    }

    public void GameOver()
    {
        if (gameOverMenu != null)
            MenuManager.Instance.GoToMenu(gameOverMenu);
        else
            Debug.LogError("GameOverMenu no asignado en el GameManager");

        currentScore = 0f;
        isPlaying = false;
    }

    public void Retry()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(currentScene.name);
    }

    public string DisplayedScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }
}