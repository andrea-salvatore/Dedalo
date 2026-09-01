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

            string email = emailInput.text;
            string password = passwordInput.text;

            bool success = await authNetworkManager.SignInWithUsernamePasswordAsync(email, password);

            if (success)
            {
                Debug.Log("Login riuscito: carico MainMenuScene.");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
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

            string email = emailInput.text;
            string password = passwordInput.text;

            bool success = await authNetworkManager.SignUpWithUsernamePasswordAsync(email, password);

            if (success)
            {
                Debug.Log("Registrazione riuscita: carico MainMenuScene.");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
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

            bool success = await authNetworkManager.SignInAnonymouslyAsync();

            if (success)
            {
                Debug.Log("Accesso Ospite riuscito: carico MainMenuScene.");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
            }
        }
    }
}
