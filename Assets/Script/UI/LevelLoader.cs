using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "MainScene";
    [SerializeField] private LoadingMenu loadingPanel; 
    private void Awake()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(LoadLevel);
    }

    public void LoadLevel()
    {
        if (SceneManager.Instance != null && loadingPanel != null)
        {
            SceneManager.Instance.LoadSceneWithLoading(sceneToLoad, loadingPanel);
        }
        else
        {
            Debug.LogError("Falta SceneManager.Instance o LoadingPanel");
        }
    }
}