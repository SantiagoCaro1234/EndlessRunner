using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton
	public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    public float currentScore = 0f;
    public float scoreMultiplier = 1f;

    public bool isPlaying = default;

    private void Update()
    {
        if (isPlaying)
        {
            currentScore += (Time.deltaTime * scoreMultiplier);
        }

        if (Input.GetKeyDown(KeyCode.K)) isPlaying = true; // debug
    }

    public void StartGame()
    {
        isPlaying = true;
    }

    public void GameOver()
    {
        currentScore = 0f;
        isPlaying = false;
    }

    public string DisplayedScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }
}
