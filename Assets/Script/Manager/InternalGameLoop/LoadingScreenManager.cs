using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI progressText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateProgress(float progress)
    {
        if (progressBar != null)
            progressBar.fillAmount = progress;
        if (progressText != null)
            progressText.text = (progress * 100f).ToString("F0") + "%";
    }
}