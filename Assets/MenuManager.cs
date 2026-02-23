using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public List<GameObject> allMenus;

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

    /// <summary>
    /// Cambia al menú deseado y desactiva los demás.
    /// </summary>
    /// <param name="desiredMenu">El menú al que se desea ir.</param>
    public void GoToMenu(GameObject desiredMenu)
    {
        if (allMenus == null || allMenus.Count == 0) return;

        DisableRemainingMenus(desiredMenu);

        desiredMenu.SetActive(true);
    }

    /// <summary>
    /// Cambia al menú deseado después de un retraso.
    /// </summary>
    /// <param name="desiredMenu">El menú al que se desea ir.</param>
    /// <param name="delay">El tiempo en segundos antes de cambiar de menú.</param>
    public void GoToMenuWithDelay(GameObject desiredMenu, float delay = 3f)
    {
        StartCoroutine(GoToMenuAfterDelayCoroutine(desiredMenu, delay));
    }

    /// <summary>
    /// Corutina que espera antes de cambiar al menú deseado.
    /// </summary>
    /// <param name="desiredMenu">El menú al que se desea ir.</param>
    /// <param name="delay">El tiempo en segundos antes de cambiar de menú.</param>
    /// <returns></returns>
    private IEnumerator GoToMenuAfterDelayCoroutine(GameObject desiredMenu, float delay)
    {
        yield return new WaitForSeconds(delay);

        GoToMenu(desiredMenu);
    }

    /// <summary>
    /// Desactiva todos los menús excepto el especificado.
    /// </summary>
    /// <param name="menuToKeepActive">El menú que debe permanecer activo.</param>
    private void DisableRemainingMenus(GameObject menuToKeepActive)
    {
        foreach (GameObject menu in allMenus)
        {
            if (menu != menuToKeepActive)
            {
                menu.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Desactiva todos los menús en la lista.
    /// </summary>
    public void CloseAllMenus()
    {
        if (allMenus == null || allMenus.Count == 0) return;

        foreach (GameObject menu in allMenus)
        {
            menu.SetActive(false);
        }
    }

}
