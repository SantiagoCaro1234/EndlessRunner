using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetDataButton : MonoBehaviour
{
    [SerializeField] private Button resetButton;

    private void Awake()
    {
        if (resetButton == null)
            resetButton = GetComponent<Button>();

        resetButton.onClick.AddListener(ResetData);
    }

    private void ResetData()
    {
        // Borrar las claves específicas que usa el juego
        PlayerPrefs.DeleteKey("PlayerCurrency");  // moneda
        PlayerPrefs.DeleteKey("OwnedHats");       // sombreros comprados
        PlayerPrefs.DeleteKey("EquippedHat");     // sombrero equipado

        PlayerPrefs.Save();

        Application.Quit();
    }
}