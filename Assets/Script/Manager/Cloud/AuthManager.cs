using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance { get; private set; }

    private bool isAuthenticated = false;
    public bool IsAuthenticated => isAuthenticated;

    // evento para notificar cuando la autenticacion se completa
    public event System.Action OnAuthenticationComplete;

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

    async void Start()
    {
        await UnityServices.InitializeAsync();
        // opcional: auto-login al iniciar
        await SignInAnonymously();
    }

    public async void SignIn()
    {
        await SignInAnonymously();
    }

    async Task SignInAnonymously()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            isAuthenticated = true;
            Debug.Log("Sign In Successful");
            Debug.Log($"Player id: {AuthenticationService.Instance.PlayerId}");

            // dispara el evento para que otros sistemas (como remote config) reaccionen
            OnAuthenticationComplete?.Invoke();
        }
        catch (AuthenticationException ex)
        {
            Debug.Log("Sign In failed");
            Debug.LogException(ex);
        }
    }
}