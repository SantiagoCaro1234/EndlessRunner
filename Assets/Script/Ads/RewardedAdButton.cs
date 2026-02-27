//using UnityEngine;
//using UnityEngine.UI;

//public class RewardedAdButton : MonoBehaviour
//{
//    [SerializeField] private Button button;

//    private void Awake()
//    {
//        if (button == null) button = GetComponent<Button>();
//        button.onClick.AddListener(ShowRewardedAd);
//    }

//    private void ShowRewardedAd()
//    {
//        if (AdsManager.Instance != null)
//        {
//            // Suscribirse a los eventos de recompensa (solo para esta ocasión)
//            AdsManager.Instance.OnRewardedAdCompleted += GiveFullReward;
//            AdsManager.Instance.OnRewardedAdSkipped += GivePartialReward;
//            AdsManager.Instance.ShowAd(AdsManager.RewardedPlacement);
//        }
//    }

//    private void GiveFullReward()
//    {
//        Debug.Log("Anuncio completado: recompensa completa");
//        // Aquí da la recompensa completa
//        CurrencyManager.Instance?.AddCurrency(50);
//        StaminaManager.Instance?.AddStamina(50);

//        // Desuscribirse
//        AdsManager.Instance.OnRewardedAdCompleted -= GiveFullReward;
//        AdsManager.Instance.OnRewardedAdSkipped -= GivePartialReward;
//    }

//    private void GivePartialReward()
//    {
//        Debug.Log("Anuncio saltado: recompensa parcial");
//        // Aquí da la recompensa reducida
//        CurrencyManager.Instance?.AddCurrency(20);
//        StaminaManager.Instance?.AddStamina(30);

//        // Desuscribirse
//        AdsManager.Instance.OnRewardedAdCompleted -= GiveFullReward;
//        AdsManager.Instance.OnRewardedAdSkipped -= GivePartialReward;
//    }

//    private void OnDestroy()
//    {
//        // Por si acaso, limpiar suscripciones
//        if (AdsManager.Instance != null)
//        {
//            AdsManager.Instance.OnRewardedAdCompleted -= GiveFullReward;
//            AdsManager.Instance.OnRewardedAdSkipped -= GivePartialReward;
//        }
//    }
//}