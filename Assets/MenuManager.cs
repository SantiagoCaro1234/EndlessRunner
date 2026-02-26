using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public List<GameObject> allMenus;

    private void Awake()
    {
        Instance = this;
    }

    public void GoToMenu(GameObject desiredMenu)
    {
        if (allMenus == null || allMenus.Count == 0) return;

        DisableRemainingMenus(desiredMenu);
        desiredMenu.SetActive(true);
    }

    public void GoToMenuWithDelay(GameObject desiredMenu, float delay = 3f)
    {
        StartCoroutine(GoToMenuAfterDelayCoroutine(desiredMenu, delay));
    }

    private IEnumerator GoToMenuAfterDelayCoroutine(GameObject desiredMenu, float delay)
    {
        yield return new WaitForSeconds(delay);
        GoToMenu(desiredMenu);
    }

    private void DisableRemainingMenus(GameObject menuToKeepActive)
    {
        foreach (GameObject menu in allMenus)
        {
            if (menu != menuToKeepActive)
                menu.SetActive(false);
        }
    }

    public void CloseAllMenus()
    {
        if (allMenus == null || allMenus.Count == 0) return;
        foreach (GameObject menu in allMenus)
            menu.SetActive(false);
    }
}