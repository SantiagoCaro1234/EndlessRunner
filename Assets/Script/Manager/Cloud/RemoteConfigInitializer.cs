using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Core;
using UnityEngine;

public class RemoteConfigInitializer : MonoBehaviour
{
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private string defaultCurrencyKey = "initial_currency";
    [SerializeField] private int defaultValue = 100;

    private async void Start()
    {
        // espera a que auth este listo antes de obtener remote config
        if (AuthManager.Instance != null)
        {
            if (AuthManager.Instance.IsAuthenticated)
            {
                await FetchRemoteConfig();
            }
            else
            {
                // suscribete al evento de autenticacion
                AuthManager.Instance.OnAuthenticationComplete += async () => await FetchRemoteConfig();
            }
        }
        else
        {
            // si no hay auth manager, igual intenta obtener config
            await FetchRemoteConfig();
        }
    }

    private async Task FetchRemoteConfig()
    {
        // asegura que unity services este inicializado
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
            await UnityServices.InitializeAsync();

        RemoteConfigService.Instance.SetCustomUserID(SystemInfo.deviceUniqueIdentifier);

        // fetch de configuracion
        await RemoteConfigService.Instance.FetchConfigsAsync(new userAttributes(), new appAttributes());

        // obtiene el valor o usa default
        int initialCurrency = RemoteConfigService.Instance.appConfig.GetInt(defaultCurrencyKey, defaultValue);

        if (currencyManager != null)
            currencyManager.InitializeCurrency(initialCurrency);
    }

    private struct userAttributes { }
    private struct appAttributes { }
}