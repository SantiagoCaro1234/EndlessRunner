//using UnityEngine;
//using UnityEngine.Advertisements;
//using System;

//public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
//{
//    public static AdsManager Instance { get; private set; }

//    [SerializeField] private string androidGameId = "5734395";
//    [SerializeField] private string iosGameId = "5734394";
//    [SerializeField] private bool testMode = true;

//    private string gameId;

//    // Nombres de los placements (deben coincidir con el dashboard de Unity Ads)
//    public const string InterstitialPlacement = "Interstitial_Android";
//    public const string RewardedPlacement = "Rewarded_Android";
//    public const string BannerPlacement = "Banner_Android";

//    // Eventos para recompensas
//    public event Action OnRewardedAdCompleted;
//    public event Action OnRewardedAdSkipped;
//    public event Action OnInterstitialAdCompleted;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void Start()
//    {
//#if UNITY_IOS
//            gameId = iosGameId;
//#else
//        gameId = androidGameId;
//#endif

//        if (!Advertisement.isInitialized && Advertisement.isSupported)
//        {
//            Advertisement.Initialize(gameId, testMode, this);
//        }
//    }

//    // Callback de inicialización completada
//    public void OnInitializationComplete()
//    {
//        Debug.Log("Ads initialized successfully");
//        // Cargar anuncios para tenerlos listos
//        LoadAd(InterstitialPlacement);
//        LoadAd(RewardedPlacement);
//        // El banner se maneja aparte (mostrar/ocultar)
//    }

//    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
//    {
//        Debug.LogError($"Ads initialization failed: {error} - {message}");
//    }

//    // Métodos públicos para cargar y mostrar
//    public void LoadAd(string placementId)
//    {
//        Advertisement.Load(placementId, this);
//    }

//    public void ShowAd(string placementId)
//    {
//        Advertisement.Show(placementId, this);
//    }

//    // Callbacks de carga
//    public void OnUnityAdsAdLoaded(string placementId)
//    {
//        Debug.Log($"Ad loaded: {placementId}");
//    }

//    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
//    {
//        Debug.LogError($"Ad failed to load: {placementId} - {error} - {message}");
//    }

//    // Callbacks de visualización
//    public void OnUnityAdsShowStart(string placementId)
//    {
//        Debug.Log($"Ad started: {placementId}");
//    }

//    public void OnUnityAdsShowClick(string placementId)
//    {
//        Debug.Log($"Ad clicked: {placementId}");
//    }

//    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
//    {
//        Debug.Log($"Ad completed: {placementId} - {showCompletionState}");

//        if (placementId == RewardedPlacement)
//        {
//            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
//                OnRewardedAdCompleted?.Invoke();
//            else if (showCompletionState == UnityAdsShowCompletionState.SKIPPED)
//                OnRewardedAdSkipped?.Invoke();
//        }
//        else if (placementId == InterstitialPlacement)
//        {
//            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
//                OnInterstitialAdCompleted?.Invoke();
//            // Recargar el interstitial para el próximo uso
//            LoadAd(InterstitialPlacement);
//        }
//    }

//    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
//    {
//        Debug.LogError($"Ad show failed: {placementId} - {error} - {message}");
//        // Intentar recargar
//        LoadAd(placementId);
//    }
//}