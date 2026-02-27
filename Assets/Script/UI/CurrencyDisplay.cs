using UnityEngine;
using TMPro;
public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;

    private void OnEnable()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.OnCurrencyChanged += UpdateDisplay;
            UpdateDisplay(CurrencyManager.Instance.GetCurrency());
        }
    }

    private void OnDisable()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnCurrencyChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(int newAmount)
    {
        if (currencyText != null)
            currencyText.text = newAmount.ToString();
        Debug.Log($"currency mostrandose {CurrencyManager.Instance.GetCurrency()}");
    }
}