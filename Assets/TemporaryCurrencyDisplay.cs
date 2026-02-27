using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TemporaryCurrencyDisplay : MonoBehaviour
{
    public int earnedCurrency;

    [SerializeField] private TextMeshProUGUI currencyEarnedtxt;

    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    private void OnGUI()
    {
        earnedCurrency = gm.earnedCoins;
        currencyEarnedtxt.text = "+" + earnedCurrency.ToString() + "!";
    }
}
