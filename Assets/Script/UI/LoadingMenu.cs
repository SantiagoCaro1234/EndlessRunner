using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingMenu : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI progressText;

    public void UpdateProgress(float progress)
    {
        if (progressBar != null)
            progressBar.fillAmount = progress;
        if (progressText != null)
            progressText.text = (progress * 100f).ToString("F0") + "%";
    }
}