using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    public Text logTxt;

    public GameObject nextMenu;

    async void Start()
    {
        await UnityServices.InitializeAsync();
    }

    public async void SignIn()
    {
        await signInAnonymous();
    }

    async Task signInAnonymous()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            Debug.Log("Sign In Successful");
            Debug.Log($"Player id: {AuthenticationService.Instance.PlayerId}");
        }
        catch (AuthenticationException ex)
        {
            Debug.Log($"Sign In failed");
            Debug.LogException(ex);
        }
    }

    public void SignedIn()
    {
        if (AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log("Player Signed in");
            //MenuManager.Instance.GoToMenu(nextMenu);
        };
    }
}
