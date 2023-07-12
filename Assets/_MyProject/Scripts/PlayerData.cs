using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    private int paws;
    private int elixirs;
    private int biscuits;
    private int highScore;
    private int extraLivesPackage;
    private string userName = string.Empty;
    private int selectedCat;
    private List<int> ownedCatIds = new List<int>();
    private bool playMusic;
    private bool playSound;
    private int coins;
    private int keys;
    private int hearts;
    private DateTime lastTimeClosedApp;
    private int secondsLeftForAnotherHart;
    private List<int> unlockedBoosters = new List<int>();
    private bool vibration;
    private bool notifications;

    public Action UpdatedPaws;
    public Action UpdatedElixirs;
    public Action UpdatedBiscuits;
    public Action UpdatedHighScore;
    public Action UpdatedExp;
    public Action UpdatedGamePlayLevel;
    public Action UpdatedExtraLives;
    public Action UpdatedUserName;
    public Action UpdatedOwnedCatsList;
    public Action UpdatedCoins;
    public Action UpdatedKeys;
    public Action UpdatedHearts;
    public Action UpdatedVibration;
    public Action UpdatedNotifications;
    public static Action UpdatedMusic;
    public static Action UpdatedSounds;

    public int Paws
    {
        get => paws;
        set
        {
            paws = value;
            UpdatedPaws?.Invoke();
        }
    }

    public int Elixirs
    {
        get => elixirs;
        set
        {
            elixirs = value;
            UpdatedElixirs?.Invoke();
        }
    }

    public int Biscuits
    {
        get => biscuits;
        set
        {
            biscuits = value;
            UpdatedBiscuits?.Invoke();
        }
    }

    public int HighScore
    {
        get => highScore;
        set
        {
            highScore = value;
            UpdatedHighScore?.Invoke();
        }
    }

    public int ExtraLivesPackage
    {
        get => extraLivesPackage;
        set
        {
            extraLivesPackage = value;
            UpdatedExtraLives?.Invoke();
        }
    }

    public string UserName
    {
        get => userName;
        set
        {
            userName = value;
            UpdatedUserName?.Invoke();
        }
    }

    public List<int> OwnedCatIds
    {
        get => ownedCatIds;
        set
        {
            ownedCatIds = value;
            UpdatedOwnedCatsList?.Invoke();
        }
    }

    public void AddOwnedCat(int _id)
    {
        ownedCatIds.Add(_id);
        UpdatedOwnedCatsList?.Invoke();
    }
    
    public bool PlayMusic
    {
        get => playMusic;
        set
        {
            playMusic = value;
            UpdatedMusic?.Invoke();
        }
    }

    public bool PlaySound
    {
        get => playSound;
        set
        {
            playSound = value;
            UpdatedSounds?.Invoke();
        }
    }

    public int Coins
    {
        get => coins;
        set
        {
            coins = value;
            UpdatedCoins?.Invoke();
        }
    }

    public int Keys
    {
        get => keys;
        set
        {
            keys = value;
            UpdatedKeys?.Invoke();
        }
    }

    public int Hearts
    {
        get => hearts;
        set => ChangeHearts(value);
    }

    public DateTime LastTimeClosedApp
    {
        get => lastTimeClosedApp;
        set => lastTimeClosedApp = value;
    }

    public int SecondsLeftForAnotherHeart
    {
        get => secondsLeftForAnotherHart;
        set => secondsLeftForAnotherHart = value;
    }

    public bool ReduceHearts()
    {
        if (!DataManager.Instance.GameData.ReduceHearts)
        {
            return true;
        }

        if (hearts == 0)
        {
            return false;
        }

        ChangeHearts(-1);
        return true;
    }

    public int SelectedCat
    {
        get => selectedCat;
        set => selectedCat = value;
    }

    public List<int> UnlockedBoosters
    {
        get => unlockedBoosters;
        set => unlockedBoosters = value;
    }

    public void ChangeHearts(int _amount)
    {
        hearts += _amount;
        if (DataManager.Instance.GameData != null)
        {
            hearts = Math.Clamp(hearts, 0, DataManager.Instance.GameData.MaxAmountOfHearts);
        }
        UpdatedHearts?.Invoke();
    }

    public bool Vibration
    {
        get => vibration;
        set
        {
            vibration = value;
            UpdatedVibration?.Invoke();
        }
    }

    public bool Notifications
    {
        get => notifications;
        set
        {
            notifications = value;
            UpdatedNotifications?.Invoke();
        }
    }
}
