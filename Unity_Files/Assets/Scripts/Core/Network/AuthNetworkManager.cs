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
            if (e.ErrorCode == AuthenticationErrorCodes.UsernameExists)
            {
                return "Questo username/email è già registrato. Prova ad accedere.";
            }
            if (e.ErrorCode == AuthenticationErrorCodes.UsernameNotFound)
            {
                return "Nessun account trovato con questo username/email. Prima registrati.";
            }
            if (e.ErrorCode == AuthenticationErrorCodes.InvalidPassword)
            {
                return "Password errata. Riprova.";
            }
            if (e.ErrorCode == AuthenticationErrorCodes.PasswordTooShort)
            {
                return "La password è troppo corta: minimo 8 caratteri.";
            }
            if (e.ErrorCode == AuthenticationErrorCodes.PasswordTooLong)
            {
                return "La password è troppo lunga: massimo 64 caratteri.";
            }
            if (e.ErrorCode == AuthenticationErrorCodes.WeakPassword)
            {
                return "La password non rispetta i requisiti: almeno 8 caratteri, una maiuscola, una minuscola, un numero e un carattere speciale.";
            }
            if (e.ErrorCode == AuthenticationErrorCodes.InvalidUsername)
            {
                return "Lo username/email non è valido.";
            }
            if (e.ErrorCode == AuthenticationErrorCodes.ClientInvalidParameters ||
                e.ErrorCode == AuthenticationErrorCodes.InvalidParameters)
            {
                return "Dati inseriti non validi. Controlla email e password.";
            }
            if (e.ErrorCode == AuthenticationErrorCodes.ClientNoConnection)
            {
                return "Nessuna connessione a internet. Controlla la rete e riprova.";
            }
            if (e.ErrorCode == AuthenticationErrorCodes.TooManyRequests)
            {
                return "Troppi tentativi in poco tempo. Attendi qualche minuto e riprova.";
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
