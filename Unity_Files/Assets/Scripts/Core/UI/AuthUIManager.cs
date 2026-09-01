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
            loginButton.onClick.AddListener(OnLoginClicked);
            registerButton.onClick.AddListener(OnRegisterClicked);
            guestButton.onClick.AddListener(OnGuestClicked);
        }

        public void OnLoginClicked()
        {
            Debug.Log("Tentativo di login per: " + emailInput.text);
        }

        public void OnRegisterClicked()
        {
            Debug.Log("Tentativo di registrazione per: " + emailInput.text);
        }

        public async void OnGuestClicked()
        {
            Debug.Log("Tentativo di accesso come Ospite (Guest).");
            if (authNetworkManager == null)
            {
                Debug.LogError("AuthNetworkManager non collegato nell'Inspector!");
                return;
            }
            await authNetworkManager.SignInAnonymouslyAsync();
        }
    }
}
