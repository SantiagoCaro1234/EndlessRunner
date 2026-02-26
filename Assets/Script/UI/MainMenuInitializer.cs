using UnityEngine;

public class MainMenuInitializer : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject authPanel;

    private void Start()
    {
        if (AuthManager.Instance != null && AuthManager.Instance.IsAuthenticated)
        {
            // jugador ya autenticado: va al menu principal
            if (mainMenuPanel != null)
                MenuManager.Instance.GoToMenu(mainMenuPanel);
        }
        else
        {
            // no autenticado: muestra pantalla de auth
            if (authPanel != null)
                authPanel.SetActive(true);
        }
    }
}