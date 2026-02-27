using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float currentScore = 0f;
    public float scoreMultiplier = 1f;
    //public float currencyMultiplier = 1f;
    public bool isPlaying = false;

    public int earnedCoins;

    private int lastCoinScoreThreshold;

    [SerializeField] private GameObject gameOverMenu;

    public event Action<float> OnScoreChanged;

    private void Awake()
    {
        Instance = this;
        lastCoinScoreThreshold = 0;
    }

    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime * scoreMultiplier;

            // ganar monedas cada 5 puntos
            int currentThreshold = (Mathf.FloorToInt((currentScore / 5f)));
            if (currentThreshold > lastCoinScoreThreshold)
            {
                int coinsToAdd = currentThreshold - lastCoinScoreThreshold;
                earnedCoins = coinsToAdd;
                CurrencyManager.Instance?.AddCurrency(coinsToAdd);
                lastCoinScoreThreshold = currentThreshold;
            }

            OnScoreChanged?.Invoke(currentScore);
        }
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
        lastCoinScoreThreshold = 0; // reiniciar contador de monedas
    }

    public void Retry()
    {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(currentScene.name);
    }

    public string DisplayedScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }
}