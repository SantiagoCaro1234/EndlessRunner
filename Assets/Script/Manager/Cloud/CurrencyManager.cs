using UnityEngine;
using System;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField] private string playerPrefsKey = "PlayerCurrency";
    [SerializeField] private int startingCurrency = 100; // valor por defecto si no hay remote config

    private int currentCurrency;

    // evento para notificar cambios en la ui
    public event Action<int> OnCurrencyChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // carga moneda guardada o usa el valor por defecto
        currentCurrency = PlayerPrefs.GetInt(playerPrefsKey, startingCurrency);
    }

    // llamado desde remote config para establecer el valor inicial
    public void InitializeCurrency(int initialAmount)
    {
        currentCurrency = initialAmount;
        PlayerPrefs.SetInt(playerPrefsKey, currentCurrency);
        PlayerPrefs.Save();
        OnCurrencyChanged?.Invoke(currentCurrency);
    }

    public void AddCurrency(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("usa deductCurrency para quitar moneda");
            return;
        }

        currentCurrency += amount;
        SaveAndNotify();
    }

    public bool DeductCurrency(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("usa addCurrency para agregar moneda");
            return false;
        }

        if (currentCurrency >= amount)
        {
            currentCurrency -= amount;
            SaveAndNotify();
            return true;
        }
        else
        {
            Debug.Log("moneda insuficiente");
            return false;
        }
    }

    public int GetCurrency()
    {
        return currentCurrency;
    }

    private void SaveAndNotify()
    {
        PlayerPrefs.SetInt(playerPrefsKey, currentCurrency);
        PlayerPrefs.Save();
        OnCurrencyChanged?.Invoke(currentCurrency);
    }
}