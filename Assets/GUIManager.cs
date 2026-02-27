using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI hpUI;

    GameManager gm;
    [SerializeField] PlayerStats playerStats;

    private void Start()
    {
        gm = GameManager.Instance;
        playerStats = FindAnyObjectByType<PlayerStats>();
    }

    private void OnGUI()
    {
        scoreUI.text = gm.DisplayedScore();
        hpUI.text = (playerStats.healthPoints).ToString();
    }
}
