using UnityEngine;
using Newtonsoft.Json;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public const string PAW_KEY = "Paws";
    public const string HIGH_SCORE_KEY = "HighScore";
    public const string EXP_KEY = "Exp";
    public const string GAMEPLAY_LEVEL_KEY = "GameplayLevel";
    public const string USER_NAME_KEY = "UserName";
    private const string ELIXIR_KEY = "Elixirs";
    private const string BISCUIT_KEY = "Biscuits";
    private const string EXTRA_LIVES_KEY = "ExtraLivesPackage";
    private const string OWNED_CATS_KEY = "OwnedCatIds";
    private const string HARTS_KEY = "Hearts";
    private const string KEYS_KEY = "Keys";
    private const string LAST_TIME_CLOSED_APP_KEY = "LastTimeClosedApp";
    private const string SECONDS_LEFT_FOR_ANOTHER_HART = "SecondsLeftForAnotherHeart";
    private const string COINS = "Coins";
    private const string VIBRATION = "Vibration";
    private const string PLAY_SOUND = "PlaySound";
    private const string PLAY_MUSIC = "PlayMusic";
    private const string NOTIFICATIONS = "Notifications";


    private PlayerData playerData;
    private GameData gameData;

    public PlayerData PlayerData => playerData;
    public GameData GameData => gameData;

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

    public void SetPlayerData(string _playerData)
    {
        playerData = JsonConvert.DeserializeObject<PlayerData>(_playerData);
        SubscribeEvents();
    }

    public void SetGameData(string _gameData)
    {
        gameData = JsonConvert.DeserializeObject<GameData>(_gameData);
    }

    public void CreateNewPlayer()
    {
        playerData = new PlayerData();
        playerData.AddOwnedCat(0);
        playerData.LastTimeClosedApp = DateTime.UtcNow;
        playerData.SecondsLeftForAnotherHeart = 0;
        playerData.ChangeHearts(5);
        playerData.Paws = 5;
        playerData.ExtraLivesPackage = 5;
        playerData.Biscuits = 5;
        playerData.Elixirs = 5;
    }

    public void CreateNewGameData()
    {
        gameData = new GameData();
        GameData.MaxAmountOfHearts = 5;
        GameData.ReduceHearts = true;
    }

    private void SubscribeEvents()
    {
        playerData.UpdatedPaws += SavePaws;
        playerData.UpdatedElixirs += SaveElixir;
        playerData.UpdatedBiscuits += SaveBiscuit;
        playerData.UpdatedHighScore += SaveHighScore;
        playerData.UpdatedExtraLives += SaveExtraLives;
        playerData.UpdatedUserName += SaveUserName;
        playerData.UpdatedOwnedCatsList += SaveOwnedCats;
        playerData.UpdatedKeys += SaveKeys;
        playerData.UpdatedHearts += SaveHarts;
        playerData.UpdatedCoins += SaveCoins;
        PlayerData.UpdatedSounds += SaveSound;
        PlayerData.UpdatedMusic += SaveMusic;
        playerData.UpdatedVibration += SaveVibration;
        playerData.UpdatedNotifications += SaveNotifications;
    }

    private void SavePaws()
    {
        FirebaseManager.Instance.SaveValue(PAW_KEY, playerData.Paws);
    }

    private void SaveElixir()
    {
        FirebaseManager.Instance.SaveValue(ELIXIR_KEY, playerData.Elixirs);
    }

    private void SaveBiscuit()
    {
        FirebaseManager.Instance.SaveValue(BISCUIT_KEY, playerData.Biscuits);
    }

    private void SaveHighScore()
    {
        FirebaseManager.Instance.SaveValue(HIGH_SCORE_KEY, playerData.HighScore);
    }

    private void SaveExtraLives()
    {
        FirebaseManager.Instance.SaveValue(EXTRA_LIVES_KEY, playerData.ExtraLivesPackage);
    }

    private void SaveUserName()
    {
        FirebaseManager.Instance.SaveValue(USER_NAME_KEY, playerData.UserName);
    }

    private void SaveOwnedCats()
    {
        FirebaseManager.Instance.SaveJsonValue(OWNED_CATS_KEY, JsonConvert.SerializeObject(playerData.OwnedCatIds));
    }

    private void SaveKeys()
    {
        FirebaseManager.Instance.SaveValue(KEYS_KEY, playerData.Keys);
    }

    private void SaveHarts()
    {
        FirebaseManager.Instance.SaveValue(HARTS_KEY, playerData.Hearts);
    }

    private void SaveLastTimeClosedApp()
    {
        FirebaseManager.Instance.SaveValue(LAST_TIME_CLOSED_APP_KEY, DateToString(PlayerData.LastTimeClosedApp));
        FirebaseManager.Instance.SaveValue(SECONDS_LEFT_FOR_ANOTHER_HART, PlayerData.SecondsLeftForAnotherHeart);
    }

    void SaveCoins()
    {
        FirebaseManager.Instance.SaveValue(COINS,PlayerData.Coins);
    }

    void SaveVibration()
    {
        FirebaseManager.Instance.SaveValue(VIBRATION,PlayerData.Vibration);
    }

    void SaveSound()
    {
        FirebaseManager.Instance.SaveValue(PLAY_SOUND,playerData.PlaySound);
    }

    void SaveMusic()
    {
        FirebaseManager.Instance.SaveValue(PLAY_MUSIC,playerData.PlayMusic);
    }

    void SaveNotifications()
    {
        FirebaseManager.Instance.SaveValue(NOTIFICATIONS,playerData.Notifications);
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void UnsubscribeEvents()
    {
        playerData.UpdatedPaws -= SavePaws;
        playerData.UpdatedElixirs -= SaveElixir;
        playerData.UpdatedBiscuits -= SaveBiscuit;
        playerData.UpdatedHighScore -= SaveHighScore;
        playerData.UpdatedExtraLives -= SaveExtraLives;
        playerData.UpdatedUserName -= SaveUserName;
        playerData.UpdatedOwnedCatsList -= SaveOwnedCats;
        playerData.UpdatedKeys -= SaveKeys;
        playerData.UpdatedHearts -= SaveHarts;
        playerData.UpdatedCoins -= SaveCoins;
        PlayerData.UpdatedSounds -= SaveSound;
        PlayerData.UpdatedMusic -= SaveMusic;
        playerData.UpdatedVibration -= SaveVibration;
        playerData.UpdatedNotifications -= SaveNotifications;
    }

    private void OnApplicationPause(bool _pause)
    {
        if (PlayerData == null)
        {
            return;
        }

        if (_pause)
        {
            PlayerData.LastTimeClosedApp = DateTime.UtcNow;
            PlayerData.SecondsLeftForAnotherHeart = HeartsManager.Instance.SecondsLeftForAnotherHeart;
            SaveLastTimeClosedApp();
        }
        else
        {
            HeartsManager.Instance.CalculateHarts();
        }
    }

    private string DateToString(DateTime _dateTime)
    {
        string _month = _dateTime.Month < 10 ? "0" + _dateTime.Month : "" + _dateTime.Month;
        string _day = _dateTime.Day < 10 ? "0" + _dateTime.Day : "" + _dateTime.Day;
        string _hour = _dateTime.Hour < 10 ? "0" + _dateTime.Hour : "" + _dateTime.Hour;
        string _minute = _dateTime.Minute < 10 ? "0" + _dateTime.Minute : "" + _dateTime.Minute;
        string _seconds = _dateTime.Second < 10 ? "0" + _dateTime.Second : "" + _dateTime.Second;
        return $"{_dateTime.Year}-{_month}-{_day}T{_hour}:{_minute}:{_seconds}";
    }
}
