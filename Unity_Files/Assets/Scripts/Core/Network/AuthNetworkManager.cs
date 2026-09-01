using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Dedalo.Core.Network
{
    public class AuthNetworkManager : MonoBehaviour
    {
        public async Task SignInAnonymouslyAsync()
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                await InitializeAsync();
            }

            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                Debug.LogError("UGS non inizializzato: login anonimo annullato.");
                return;
            }

            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Login anonimo riuscito. PlayerId: " + AuthenticationService.Instance.PlayerId);
            }
            catch (AuthenticationException e)
            {
                Debug.LogError("Errore di autenticazione anonima: " + e.Message);
            }
            catch (RequestFailedException e)
            {
                Debug.LogError("Errore di rete durante il login anonimo: " + e.Message);
            }
        }

        private async Task InitializeAsync()
        {
            try
            {
                await UnityServices.InitializeAsync();
                Debug.Log("Unity Gaming Services inizializzati correttamente.");
            }
            catch (ServicesInitializationException e)
            {
                Debug.LogError("Errore di inizializzazione UGS: " + e.Message);
            }
        }
    }
}
