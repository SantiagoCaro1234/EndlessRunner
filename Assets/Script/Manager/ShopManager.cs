using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool TryBuyHat(HatData hat)
    {
        if (hat == null) return false;

        int hatId = HatPool.Instance.GetHatId(hat);
        if (hatId == -1)
        {
            Debug.LogError("Sombrero no encontrado en HatPool");
            return false;
        }

        if (HatPool.Instance.IsOwned(hatId))
        {
            Debug.Log("Ya tienes este sombrero");
            return false;
        }

        if (CurrencyManager.Instance != null && CurrencyManager.Instance.GetCurrency() >= hat.price)
        {
            CurrencyManager.Instance.DeductCurrency(hat.price);
            HatPool.Instance.AddOwned(hatId);
            Debug.Log($"Comprado: {hat.hatName}");
            return true;
        }
        else
        {
            Debug.Log("Moneda insuficiente");
            return false;
        }
    }
}