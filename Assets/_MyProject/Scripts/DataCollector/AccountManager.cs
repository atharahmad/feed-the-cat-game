using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    public const string NEW_ACC_KEY = "NewAcc";
    public static bool isNewAccount;

    [SerializeField] private GameObject buttonsHolder;
    [SerializeField] private Button loginWithFacebookButton;
    [SerializeField] private Button loginWithAppleButton;
    [SerializeField] private Button loginWithGoogleButton;
    [SerializeField] private Button loginAsGuestButton;
    [SerializeField] private PlayerNamePanel playerNamePanel;
    
    private void OnEnable()
    {
        PlayerNamePanel.SavedName += LoadGameData;
        Initialization.Finished += CheckAccount;
        loginWithFacebookButton.onClick.AddListener(LoginWithFacebook);
        loginWithAppleButton.onClick.AddListener(LoginWithApple);
        loginWithGoogleButton.onClick.AddListener(LoginWithGoogle);
        loginAsGuestButton.onClick.AddListener(LoginAsGuest);
    }

    private void OnDisable()
    {
        PlayerNamePanel.SavedName -= LoadGameData;
        Initialization.Finished -= CheckAccount;
        loginWithFacebookButton.onClick.RemoveListener(LoginWithFacebook);
        loginWithAppleButton.onClick.RemoveListener(LoginWithApple);
        loginWithGoogleButton.onClick.RemoveListener(LoginWithGoogle);
        loginAsGuestButton.onClick.RemoveListener(LoginAsGuest);
    }

    private void CheckAccount()
    {
        if (FirebaseManager.Instance.IsLoggedIn)
        {
            FirebaseManager.Instance.LoadPlayerData(FinishLoadingPlayerData);
            return;
        }
        
        buttonsHolder.SetActive(true);
    }

    private void LoginWithFacebook()
    {
        //todo remove me
       // return;
        FacebookManager.Init(Login);

        void Login()
        {
            ManageButtons(false);
            FacebookManager.Login(LoginSuccess,LoginFailed);
        }
    }
    
    private void LoginWithGoogle()
    {
        GoogleManager.Init(Login);
        
        void Login()
        {
            ManageButtons(false);
            GoogleManager.Login(LoginSuccess, LoginFailed);
        }
    }

    private void LoginWithApple()
    {
        AppleManager.Init(Login);

        void Login()
        {
            ManageButtons(false);
            AppleManager.Login(LoginSuccess,LoginFailed);
        }
    }

    private void LoginAsGuest()
    {
        ManageButtons(false);
        FirebaseManager.Instance.LoginAnonymous(FirebaseLoginHandler);
    }

    private void LoginSuccess(Credential _credentials)
    {
        FirebaseManager.Instance.Login(_credentials, FirebaseLoginHandler);
    }

    private void FirebaseLoginHandler(bool _status, string _args)
    {
        if (_status)
        {
            if (_args == NEW_ACC_KEY)
            {
                isNewAccount =true;
                ShowEnterName();
            }
            else
            {
                FirebaseManager.Instance.LoadPlayerData(FinishLoadingPlayerData);
            }
        }
        else
        {
            ManageButtons(true);
            UIManager.Instance.OkDialog.Show(_args);
        }
    }

    private void FinishLoadingPlayerData(string _data)
    {
        DataManager.Instance.SetPlayerData(_data);
        LoadGameData();
    }

    private void LoadGameData()
    {
        FirebaseManager.Instance.LoadGameData(FinishedLoadingGameData);
    }

    private void FinishedLoadingGameData(string _data)
    {
        DataManager.Instance.SetGameData(_data);
        HeartsManager.Instance.Init();
        SceneManager.LoadMainMenu();
    }

    private void LoginFailed(string _reason)
    {
        UIManager.Instance.OkDialog.Show(_reason);
        Debug.Log(_reason);
    }

    private void ShowEnterName()
    {
        playerNamePanel.Setup();
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            loginWithGoogleButton.gameObject.SetActive(true);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            loginWithAppleButton.gameObject.SetActive(true);
        }
    }

    void ManageButtons(bool _status)
    {
        loginAsGuestButton.interactable = _status;
        loginWithAppleButton.interactable = _status;
        loginWithFacebookButton.interactable = _status;
        loginWithGoogleButton.interactable = _status;
    }
}
