using Dedalo.Core.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dedalo.Core.UI
{
    public class AuthUIManager : MonoBehaviour
    {
        [Header("Input Fields")]
        [SerializeField] private TMP_InputField emailInput;
        [SerializeField] private TMP_InputField passwordInput;

        [Header("Buttons")]
        [SerializeField] private Button loginButton;
        [SerializeField] private Button registerButton;
        [SerializeField] private Button guestButton;

        [Header("Status UI")]
        [SerializeField] private TextMeshProUGUI statusText;

        [Header("Network")]
        [SerializeField] private AuthNetworkManager authNetworkManager;

        private void Start()
        {
            loginButton.onClick.AddListener(OnLoginButtonClicked);
            registerButton.onClick.AddListener(OnRegisterButtonClicked);
            guestButton.onClick.AddListener(OnGuestButtonClicked);
        }

        public async void OnLoginButtonClicked()
        {
            Debug.Log("Tentativo di login per: " + emailInput.text);
            if (authNetworkManager == null)
            {
                Debug.LogError("AuthNetworkManager non collegato nell'Inspector!");
                return;
            }
            if (string.IsNullOrEmpty(emailInput.text) || string.IsNullOrEmpty(passwordInput.text))
            {
                Debug.LogWarning("Compila sia il campo Email sia il campo Password.");
                return;
            }

            SetButtonsInteractable(false);
            ShowStatus("Accesso in corso...", false);

            bool success;
            try
            {
                Debug.Log("[UI] Chiamo il NetworkManager e ATTENDO la risposta...");
                success = await authNetworkManager.SignInWithUsernamePasswordAsync(emailInput.text, passwordInput.text);
                Debug.Log($"[UI] Risposta ricevuta dal NetworkManager. Il valore di success è: {success}");
            }
            finally
            {
                SetButtonsInteractable(true);
                Debug.Log("[UI] Stato UI ripristinato.");
            }

            if (success == true)
            {
                Debug.Log("[UI] Teletrasporto autorizzato! Carico il Menu.");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
            }
            else
            {
                ShowStatus("Login fallito. Controlla i dati e riprova.", true);
                Debug.Log("[UI] Login fallito. Blocco il teletrasporto.");
            }
        }

        public async void OnRegisterButtonClicked()
        {
            Debug.Log("Tentativo di registrazione per: " + emailInput.text);
            if (authNetworkManager == null)
            {
                Debug.LogError("AuthNetworkManager non collegato nell'Inspector!");
                return;
            }
            if (string.IsNullOrEmpty(emailInput.text) || string.IsNullOrEmpty(passwordInput.text))
            {
                Debug.LogWarning("Compila sia il campo Email sia il campo Password.");
                return;
            }

            SetButtonsInteractable(false);
            ShowStatus("Registrazione in corso...", false);

            bool success;
            try
            {
                Debug.Log("[UI] Chiamo il NetworkManager e ATTENDO la risposta...");
                success = await authNetworkManager.SignUpWithUsernamePasswordAsync(emailInput.text, passwordInput.text);
                Debug.Log($"[UI] Risposta ricevuta dal NetworkManager. Il valore di success è: {success}");
            }
            finally
            {
                SetButtonsInteractable(true);
                Debug.Log("[UI] Stato UI ripristinato.");
            }

            if (success == true)
            {
                Debug.Log("[UI] Teletrasporto autorizzato! Carico il Menu.");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
            }
            else
            {
                ShowStatus("Registrazione fallita. Controlla i dati e riprova.", true);
                Debug.Log("[UI] Registrazione fallita. Blocco il teletrasporto.");
            }
        }

        public async void OnGuestButtonClicked()
        {
            Debug.Log("Tentativo di accesso come Ospite (Guest).");
            if (authNetworkManager == null)
            {
                Debug.LogError("AuthNetworkManager non collegato nell'Inspector!");
                return;
            }

            SetButtonsInteractable(false);
            ShowStatus("Accesso Ospite in corso...", false);

            bool success;
            try
            {
                Debug.Log("[UI] Chiamo il NetworkManager e ATTENDO la risposta...");
                success = await authNetworkManager.SignInAnonymouslyAsync();
                Debug.Log($"[UI] Risposta ricevuta dal NetworkManager. Il valore di success è: {success}");
            }
            finally
            {
                SetButtonsInteractable(true);
                Debug.Log("[UI] Stato UI ripristinato.");
            }

            if (success == true)
            {
                Debug.Log("[UI] Teletrasporto autorizzato! Carico il Menu.");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
            }
            else
            {
                ShowStatus("Accesso Ospite fallito. Riprova.", true);
                Debug.Log("[UI] Accesso Ospite fallito. Blocco il teletrasporto.");
            }
        }

        private void SetButtonsInteractable(bool interactable)
        {
            loginButton.interactable = interactable;
            registerButton.interactable = interactable;
            guestButton.interactable = interactable;
        }

        private void ShowStatus(string message, bool isError)
        {
            if (statusText == null)
            {
                return;
            }
            statusText.text = message;
            statusText.color = isError ? Color.red : Color.white;
        }
    }
}
