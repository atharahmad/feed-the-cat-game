using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;

    private const string USERS_KEY = "users";
    private const string GAME_DATA_KEY = "gameData";

    private FirebaseAuth auth;
    private DatabaseReference database;
    private FirebaseUser firebaseUser;

    public string PlayerId => firebaseUser.UserId;

    public bool IsLoggedIn => firebaseUser != null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init(Action _callBack)
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(_result =>
        {
            if (_result.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                database = FirebaseDatabase.DefaultInstance.RootReference;
                _callBack?.Invoke();
            }
            else
            {
                throw new Exception("Couldn't fix dependencies in FireBaseManager.cs");
            }
        });
    }

    public void Login(Credential _credential, Action<bool, string> _loginCallback)
    {
        auth.SignInWithCredentialAsync(_credential).ContinueWithOnMainThread(_result =>
        {
            if (_result.Exception != null)
            {
                _loginCallback?.Invoke(false, "Failed to login: " + _result.Exception.Message);
            }
            else
            {
                firebaseUser = _result.Result;
                DecideIfThisIsNewUser(_loginCallback);
            }
        });
    }

    public void LoginAnonymous(Action<bool, string> _loginCallback)
    {
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(_task =>
        {
            if (_task.IsCanceled)
            {
                _loginCallback(false,"SignIn was canceled.");
                return;
            }
            if (_task.IsFaulted)
            {
                _loginCallback?.Invoke(false,"SignIn encountered an error: " + _task.Exception);
                return;
            }
            firebaseUser = _task.Result;
            DecideIfThisIsNewUser(_loginCallback);
        });
    }

    private void DecideIfThisIsNewUser(Action<bool, string> _callback)
    {
        database.Child(USERS_KEY).Child(firebaseUser.UserId).GetValueAsync().ContinueWithOnMainThread(_task =>
        {
            if (_task.IsFaulted)
            {
                UIManager.Instance.OkDialog.Show("Error while checking if it is new user. Please contact support");
            }
            else if (_task.IsCompleted)
            {
                DataSnapshot _snapshot = _task.Result;
                bool _newUser = _snapshot.Child(DataManager.PAW_KEY).Exists == false;
                string _isNewAccountKey = string.Empty;
                if (_newUser)
                {
                    _isNewAccountKey = AccountManager.NEW_ACC_KEY;
                }
                _callback?.Invoke(true, _isNewAccountKey);
            }
        });
    }

    public void LoadPlayerData(Action<string> _callBack)
    {
        database.Child(USERS_KEY).Child(firebaseUser.UserId).GetValueAsync().ContinueWithOnMainThread(_result =>
        {
            DataSnapshot _snapshot = _result.Result;
            string _jsonData = _snapshot.GetRawJsonValue();
            _callBack?.Invoke(_jsonData);
        });
    }

    public void LoadGameData(Action<string> _callBack)
    {
        Debug.Log("----- Sending request");
        database.Child(GAME_DATA_KEY).GetValueAsync().ContinueWithOnMainThread(_result =>
        {
            DataSnapshot _snapshot = _result.Result;
            string _jsonData = _snapshot.GetRawJsonValue();
            Debug.Log("----- Got response: " + _jsonData);
            _callBack?.Invoke(_jsonData);
        });
    }

    public void SaveValue<T>(string _path, T _value)
    {
        database.Child(USERS_KEY)
            .Child(firebaseUser.UserId)
            .Child(_path)
            .SetValueAsync(_value);
    }

    public void SaveJsonValue(string _path, string _json)
    {
        database.Child(USERS_KEY)
            .Child(firebaseUser.UserId)
            .Child(_path)
            .SetRawJsonValueAsync(_json);
    }

    public void SaveEverything(Action<bool> _callback)
    {
        string _data = JsonConvert.SerializeObject(DataManager.Instance.PlayerData);
        database.Child(USERS_KEY).Child(firebaseUser.UserId).SetRawJsonValueAsync(_data).ContinueWithOnMainThread(_result =>
        {
            if (_result.IsFaulted)
            {
                _callback.Invoke(false);
            }
            else if (_result.IsCompleted)
            {
                _callback?.Invoke(true);
            }
        });
    }

    public void GetLeaderboardEntries(Action<List<LeaderBoardEntry>> _callback)
    {
        database.Child(USERS_KEY).GetValueAsync().ContinueWithOnMainThread(_result =>
        {
            DataSnapshot _snapshot = _result.Result;
            List<LeaderBoardEntry> _entries = new List<LeaderBoardEntry>();
            foreach (DataSnapshot _childSnapshot in _snapshot.Children)
            {
                string _name = _childSnapshot.Child(DataManager.USER_NAME_KEY).Value.ToString();
                int _score = Convert.ToInt32(_childSnapshot.Child(DataManager.HIGH_SCORE_KEY).Value.ToString());
                _entries.Add(new LeaderBoardEntry() { Nickname = _name, Score = _score });
            }
            _callback?.Invoke(_entries);
        });
    }

}
