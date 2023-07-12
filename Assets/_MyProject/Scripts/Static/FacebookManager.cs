using System;
using System.Collections.Generic;
using Facebook.Unity;
using Firebase.Auth;

public static class FacebookManager 
{
    private static bool isInit;

    public static void Init(Action _callBack)
    {
        if (isInit)
        {
            _callBack?.Invoke();
            return;
        }
        
        isInit = true;
        //FB.Init(() => _callBack?.Invoke());
        FB.Init("1267546517462265", null, true, true, true, false, true, null, "en_US", null, () => _callBack?.Invoke());
    }

    public static void Login(Action<Credential> _loginSuccess, Action<string> _loginFailed)
    {
        var _parameters = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(_parameters, (_result) => OnFacebookLoginResult(_result, _loginSuccess, _loginFailed));
    }

    private static void OnFacebookLoginResult(ILoginResult _result, Action<Credential> _loginSuccess, Action<string> _loginFailed)
    {
        if (_result.Error != null)
        {
            _loginFailed?.Invoke(_result.Error);
            return;
        }

        if (FB.IsLoggedIn)
        {
            Credential _credentials = FacebookAuthProvider.GetCredential(AccessToken.CurrentAccessToken.TokenString);
            _loginSuccess?.Invoke(_credentials);
        }
        else
        {
            _loginFailed?.Invoke("Something went wrong while signing in with facebook");
        }
    }
}
