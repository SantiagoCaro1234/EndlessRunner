using UnityEngine;

public class HatManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform hatAnchor; // padre de todos los sombreros

    [Header("Datos de sombreros")]
    [SerializeField] private HatData[] hatData; // debe tener la misma longitud que los hijos

    [Header("Configuración inicial")]
    [SerializeField] private int defaultHatIndex = 0;

    private int currentHatIndex = -1;

    private void Start()
    {
        // Validar que la cantidad de datos coincida con los hijos
        if (hatAnchor.childCount != hatData.Length)
        {
            Debug.LogError("El número de hijos en HatAnchor no coincide con la cantidad de HatData");
            return;
        }

        DisableAllHats();
        if (defaultHatIndex >= 0 && defaultHatIndex < hatAnchor.childCount)
        {
            ActivateHat(defaultHatIndex);
        }
    }

    private void DisableAllHats()
    {
        foreach (Transform child in hatAnchor)
        {
            child.gameObject.SetActive(false);
        }
    }

    // Activa el sombrero en el índice dado y actualiza currentHatIndex
    public void ActivateHat(int index)
    {
        if (index < 0 || index >= hatAnchor.childCount)
        {
            Debug.LogWarning($"Índice {index} fuera de rango. Hay {hatAnchor.childCount} sombreros.");
            return;
        }

        // Desactiva todos
        foreach (Transform child in hatAnchor)
        {
            child.gameObject.SetActive(false);
        }

        // Activa el deseado
        hatAnchor.GetChild(index).gameObject.SetActive(true);
        currentHatIndex = index;
    }

    // Devuelve la data del sombrero actualmente activo (o null si no hay)
    public HatData GetCurrentHatData()
    {
        if (currentHatIndex >= 0 && currentHatIndex < hatData.Length)
            return hatData[currentHatIndex];
        return null;
    }

    // Devuelve true si el sombrero en el índice dado está activo
    public bool IsHatEquipped(int index)
    {
        return index == currentHatIndex;
    }

    // Devuelve el índice del sombrero activo
    public int GetCurrentHatIndex() => currentHatIndex;
}