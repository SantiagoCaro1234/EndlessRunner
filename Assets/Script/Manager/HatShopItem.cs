using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HatShopItem : MonoBehaviour
{
    [Header("Datos")]
    [SerializeField] private HatData hatData; // asignar en inspector

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button equipButton;

    private int hatId = -1;

    private void Start()
    {
        if (hatData == null)
        {
            Debug.LogError("HatData no asignado en HatShopItem");
            return;
        }

        hatId = HatPool.Instance.GetHatId(hatData);
        if (hatId == -1)
        {
            Debug.LogError("HatData no encontrado en HatPool");
            return;
        }

        // Mostrar información
        if (nameText != null) nameText.text = hatData.hatName;
        if (priceText != null) priceText.text = hatData.price.ToString();

        // Asignar listeners
        buyButton.onClick.AddListener(OnBuy);
        equipButton.onClick.AddListener(OnEquip);

        // Actualizar estado inicial
        UpdateUI();
    }

    private void OnEnable()
    {
        // Al activar, refrescar UI (por si se compró desde otro lado)
        UpdateUI();
    }

    private void UpdateUI()
    {
        bool owned = HatPool.Instance.IsOwned(hatId);
        buyButton.interactable = !owned;         // botón comprar se deshabilita si ya es dueño
        equipButton.gameObject.SetActive(owned); // botón equipar solo aparece si es dueño
    }

    private void OnBuy()
    {
        if (ShopManager.Instance != null)
        {
            bool success = ShopManager.Instance.TryBuyHat(hatData);
            if (success)
            {
                UpdateUI(); // actualizar botones tras compra
            }
        }
        else
        {
            Debug.LogError("No hay ShopManager en la escena");
        }
    }

    private void OnEquip()
    {
        HatPool.Instance.Equip(hatId);
        Debug.Log($"Equipado: {hatData.hatName}");
    }
}