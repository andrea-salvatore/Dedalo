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
            if (IsAlreadySignedIn() || !await EnsureInitializedAsync())
            {
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

        public async Task SignUpWithUsernamePasswordAsync(string username, string password)
        {
            if (IsAlreadySignedIn() || !await EnsureInitializedAsync())
            {
                return;
            }

            try
            {
                await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
                Debug.Log("Registrazione completata con successo! PlayerId: " + AuthenticationService.Instance.PlayerId);
            }
            catch (AuthenticationException e)
            {
                Debug.LogError("Errore di registrazione: " + TranslateAuthError(e));
            }
            catch (RequestFailedException e)
            {
                Debug.LogError("Errore di registrazione: " + TranslateRequestError(e));
            }
        }

        public async Task SignInWithUsernamePasswordAsync(string username, string password)
        {
            if (IsAlreadySignedIn() || !await EnsureInitializedAsync())
            {
                return;
            }

            try
            {
                await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
                Debug.Log("Login riuscito! Bentornato " + AuthenticationService.Instance.PlayerName + " (PlayerId: " + AuthenticationService.Instance.PlayerId + ")");
            }
            catch (AuthenticationException e)
            {
                Debug.LogError("Errore di login: " + TranslateAuthError(e));
            }
            catch (RequestFailedException e)
            {
                Debug.LogError("Errore di login: " + TranslateRequestError(e));
            }
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

        private async Task<bool> EnsureInitializedAsync()
        {
            if (UnityServices.State == ServicesInitializationState.Initialized)
            {
                return true;
            }

            try
            {
                await UnityServices.InitializeAsync();
                Debug.Log("Unity Gaming Services inizializzati correttamente.");
                return true;
            }
            catch (ServicesInitializationException e)
            {
                Debug.LogError("Errore di inizializzazione UGS: " + e.Message);
                return false;
            }
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
