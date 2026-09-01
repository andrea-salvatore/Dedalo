using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Dedalo.Core.Network
{
    public class AuthNetworkManager : MonoBehaviour
    {
        public async Task<bool> SignInAnonymouslyAsync()
        {
            if (Unity.Services.Core.UnityServices.State == Unity.Services.Core.ServicesInitializationState.Uninitialized)
            {
                await Unity.Services.Core.UnityServices.InitializeAsync();
            }

            if (Unity.Services.Core.UnityServices.State != Unity.Services.Core.ServicesInitializationState.Initialized)
            {
                Debug.LogError("UGS non inizializzato: login anonimo annullato.");
                return false;
            }

            if (IsAlreadySignedIn())
            {
                return false;
            }

            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log("Login anonimo riuscito. PlayerId: " + AuthenticationService.Instance.PlayerId);
                return true;
            }
            catch (AuthenticationException e)
            {
                Debug.LogError("Errore di autenticazione anonima: " + e.Message);
                return false;
            }
            catch (RequestFailedException e)
            {
                Debug.LogError("Errore di rete durante il login anonimo: " + e.Message);
                return false;
            }
        }

        public async Task<bool> SignUpWithUsernamePasswordAsync(string username, string password)
        {
            if (Unity.Services.Core.UnityServices.State == Unity.Services.Core.ServicesInitializationState.Uninitialized)
            {
                await Unity.Services.Core.UnityServices.InitializeAsync();
            }

            if (Unity.Services.Core.UnityServices.State != Unity.Services.Core.ServicesInitializationState.Initialized)
            {
                Debug.LogError("UGS non inizializzato: registrazione annullata.");
                return false;
            }

            if (IsAlreadySignedIn())
            {
                return false;
            }

            try
            {
                await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
                Debug.Log("Registrazione completata con successo! PlayerId: " + AuthenticationService.Instance.PlayerId);
                return true;
            }
            catch (AuthenticationException e)
            {
                Debug.LogError("Errore di registrazione: " + TranslateAuthError(e));
                return false;
            }
            catch (RequestFailedException e)
            {
                Debug.LogError("Errore di registrazione: " + TranslateRequestError(e));
                return false;
            }
        }

        public async Task<bool> SignInWithUsernamePasswordAsync(string username, string password)
        {
            if (Unity.Services.Core.UnityServices.State == Unity.Services.Core.ServicesInitializationState.Uninitialized)
            {
                await Unity.Services.Core.UnityServices.InitializeAsync();
            }

            if (Unity.Services.Core.UnityServices.State != Unity.Services.Core.ServicesInitializationState.Initialized)
            {
                Debug.LogError("UGS non inizializzato: login annullato.");
                return false;
            }

            if (IsAlreadySignedIn())
            {
                return false;
            }

            try
            {
                await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
                Debug.Log("Login riuscito! Bentornato " + AuthenticationService.Instance.PlayerName + " (PlayerId: " + AuthenticationService.Instance.PlayerId + ")");
                return true;
            }
            catch (AuthenticationException e)
            {
                Debug.LogError("Errore di login: " + TranslateAuthError(e));
                return false;
            }
            catch (RequestFailedException e)
            {
                Debug.LogError("Errore di login: " + TranslateRequestError(e));
                return false;
            }
        }

        public void SignOut()
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.Log("Non sei loggato: logout non necessario.");
                return;
            }

            AuthenticationService.Instance.SignOut();
            Debug.Log("Logout effettuato");
        }

        private bool IsAlreadySignedIn()
        {
            if (AuthenticationService.Instance.IsSignedIn)
            {
                Debug.Log("Sei già loggato!");
                return true;
            }
            return false;
        }

        private string TranslateAuthError(AuthenticationException e)
        {
            string message = e.Message.ToLowerInvariant();
            if (message.Contains("already exists") || message.Contains("username exists"))
            {
                return "Questo username/email è già registrato. Prova ad accedere.";
            }
            if (message.Contains("not found"))
            {
                return "Nessun account trovato con questo username/email. Prima registrati.";
            }
            if (message.Contains("password"))
            {
                return "Password errata o non valida. Ricorda: almeno 8 caratteri, una maiuscola, una minuscola, un numero e un carattere speciale.";
            }
            return e.Message;
        }

        private string TranslateRequestError(RequestFailedException e)
        {
            if (e.Message.ToLowerInvariant().Contains("password"))
            {
                return "La password non rispetta i requisiti: almeno 8 caratteri, una maiuscola, una minuscola, un numero e un carattere speciale.";
            }
            if (e.Message.ToLowerInvariant().Contains("username"))
            {
                return "Lo username/email non è valido o è già in uso.";
            }
            return e.Message;
        }
    }
}
