using System;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;

    private int maxAmountOfLives;
    private int score;
    private int currentAmountOfLives;

    public int Multiplier { get; private set; } = 1;
    public Transform foodHolder;
    public GameObject iceCreamTrailPrefab;

    public static Action UpdatedScore;
    public static Action UpdatedCurrentAmountOfLives;
    public static Action<bool> GameEnded;
    public static Action UpdatedMultiplier;
    public static int TimeScale = 1;
    public static InputSystem input;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            UpdatedScore?.Invoke();
        }
    }

    public int MaxAmountOfLives => maxAmountOfLives;

    public int CurrentAmountOfLives => currentAmountOfLives;

    private void Awake()
    {
        print("SCREEN DPI :: " + Screen.dpi);
        Instance = this;
    }

    private void Start()
    {
        TimeScale = 1;
        input = new InputStandalone();
        maxAmountOfLives = DataManager.Instance.GameData.MaxAmountOfHearts;

        if (!DataManager.Instance.PlayerData.ReduceHearts())
        {
            SceneController.LoadMainMenu();
            UIManager.Instance.OkDialog.Show("You are out of Hearts!");
            return;
        }

        Score = 0;
        currentAmountOfLives = maxAmountOfLives;
        AudioManager.Instance.PlayBackgroundMusic(AudioManager.GAMEPLAY);
    }

    public void RecoverToFullHealth()
    {
        currentAmountOfLives = maxAmountOfLives;
        UpdatedCurrentAmountOfLives?.Invoke();
    }

    public void TakeDamage(int _amount)
    {
        currentAmountOfLives -= _amount;
        UpdatedCurrentAmountOfLives?.Invoke();
        if (currentAmountOfLives <= 0)
        {
            GameEnded?.Invoke(false);
        }
    }

    public void Pause ()
    {
        FoodSpawner.Instance.enabled = false;
        CoinsSpawner.Instance.enabled = false;
        RewardingIceCreamSpawner.Instance.enabled = false;
    }

    public void Play()
    {
        FoodSpawner.Instance.enabled = true;
        CoinsSpawner.Instance.enabled = true;
        RewardingIceCreamSpawner.Instance.enabled = true;
    }

    public void Drown()
    {
        GameEnded?.Invoke(false);
    }

    public void IncreaseMultiplier()
    {
        Multiplier++;
        UpdatedMultiplier?.Invoke();
    }

    public static string ScoreToString(long _score)
    {
        if (_score >= 1_000_000_000)
        {
            return (_score / 1_000_000_000f).ToString("0.00") + "B";
        }
        else if (_score >= 1_000_000)
        {
            return (_score / 1_000_000f).ToString("0.00") + "M";
        }
        else if (_score >= 1_000)
        {
            return (_score / 1_000f).ToString("0.00") + "k";
        }
        else
        {
            return _score.ToString("0");
        }
    }
}
