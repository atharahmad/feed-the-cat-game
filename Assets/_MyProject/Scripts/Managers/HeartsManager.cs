using System;
using UnityEngine;

public class HeartsManager : MonoBehaviour
{
    public static HeartsManager Instance;

    [SerializeField] private float amountOfSecondsForNextHeart;

    private bool isInit;
    private float counter;

    public int SecondsLeftForAnotherHeart => (int)counter;
    public bool IsFull => DataManager.Instance.PlayerData.Hearts >= DataManager.Instance.GameData.MaxAmountOfHearts;

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

    public void Init()
    {
        CalculateHarts();
        isInit = true;
    }

    public void CalculateHarts()
    {
        int _secondsThatPassedSinceLastLogin = (int)(DateTime.UtcNow - DataManager.Instance.PlayerData.LastTimeClosedApp).TotalSeconds;
        counter = amountOfSecondsForNextHeart;
        if (_secondsThatPassedSinceLastLogin > DataManager.Instance.PlayerData.SecondsLeftForAnotherHeart)
        {
            _secondsThatPassedSinceLastLogin -= DataManager.Instance.PlayerData.SecondsLeftForAnotherHeart;
            DataManager.Instance.PlayerData.ChangeHearts(1);
            counter = _secondsThatPassedSinceLastLogin;
        }
        else
        {
            counter = DataManager.Instance.PlayerData.SecondsLeftForAnotherHeart - _secondsThatPassedSinceLastLogin;
        }

        while (counter >= amountOfSecondsForNextHeart)
        {
            if (IsFull)
            {
                break;
            }
            counter -= amountOfSecondsForNextHeart;
            DataManager.Instance.PlayerData.ChangeHearts(1);
        }

        counter = Mathf.Clamp(counter, 0, amountOfSecondsForNextHeart);
    }

    private void Update()
    {
        if (!isInit)
        {
            return;
        }

        if (IsFull)
        {
            return;
        }

        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            DataManager.Instance.PlayerData.ChangeHearts(1);
            counter = amountOfSecondsForNextHeart;
        }
    }

    public void Refill()
    {
        DataManager.Instance.PlayerData.ChangeHearts(DataManager.Instance.GameData.MaxAmountOfHearts-DataManager.Instance.PlayerData.Hearts);
    }
}
