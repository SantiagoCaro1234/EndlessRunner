using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    [SerializeField] private string loadingScreenSceneName = "LoadingScreen"; // nombre de la escena de carga


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
    }

    // Método para cargar una escena con pantalla de carga
    public void LoadSceneWithLoadingScreen(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncWithLoading(sceneName));
    }

    private IEnumerator LoadSceneAsyncWithLoading(string sceneName)
    {
        // 1. Cargar la escena de carga de forma aditiva
        AsyncOperation loadLoadingScreen = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadingScreenSceneName, LoadSceneMode.Additive);
        yield return loadLoadingScreen;

        // 2. Obtener el LoadingScreenManager de la escena recién cargada
        LoadingScreenManager loadingManager = FindObjectOfType<LoadingScreenManager>();
        if (loadingManager == null)
        {
            Debug.LogError("No se encontró LoadingScreenManager en la escena de carga");
            yield break;
        }

        // 3. Iniciar la carga de la escena destino (sin activación automática)
        AsyncOperation loadTargetScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        loadTargetScene.allowSceneActivation = false;

        // 4. Mientras se carga, actualizar la barra de progreso
        while (!loadTargetScene.isDone)
        {
            // El progreso va de 0 a 0.9 mientras carga; 0.9 significa que está listo para activar
            float progress = Mathf.Clamp01(loadTargetScene.progress / 0.9f);
            loadingManager.UpdateProgress(progress);

            // Si ya está listo para activar (progreso >= 0.9)
            if (loadTargetScene.progress >= 0.9f)
            {
                // Opcional: esperar un momento para que el usuario vea el 100%
                yield return new WaitForSeconds(0.5f);
                // Activar la escena
                loadTargetScene.allowSceneActivation = true;
            }

            yield return null;
        }

        // 5. Descargar la escena de carga
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(loadingScreenSceneName);
    }

    public void LoadSceneWithLoading(string sceneName, LoadingMenu loadingPanel)
    {
        StartCoroutine(LoadSceneAsyncWithPanel(sceneName, loadingPanel));
    }

    private IEnumerator LoadSceneAsyncWithPanel(string sceneName, LoadingMenu loadingPanel)
    {
        // activar el panel de carga
        loadingPanel.gameObject.SetActive(true);

        // iniciar la carga de la escena (sin activación automática)
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // mientras se carga, actualizar el panel
        while (!operation.isDone)
        {
            // el progreso va de 0 a 0.9; cuando llega a 0.9 significa que está listo
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingPanel.UpdateProgress(progress);

            // si la carga está completa (progreso >= 0.9)
            if (operation.progress >= 0.9f)
            {
                // opcional: esperar un momento para que el usuario vea el 100%
                yield return new WaitForSeconds(0.2f);
                // activar la nueva escena
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    // Método de compatibilidad para cargas simples (sin pantalla)
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void ReloadCurrentScene()
    {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.name);
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}