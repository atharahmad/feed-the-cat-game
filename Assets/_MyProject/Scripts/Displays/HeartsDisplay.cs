using UnityEngine;
using TMPro;
using System;

public class HeartsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI heartsDisplay;
    [SerializeField] private TextMeshProUGUI timerDisplay;
    [SerializeField] private GameObject infiniteLivesHolder;

    private void OnEnable()
    {
        DataManager.Instance.PlayerData.UpdatedHearts += ShowHearts;
    }

    private void OnDisable()
    {
        DataManager.Instance.PlayerData.UpdatedHearts -= ShowHearts;
    }

    private void Start()
    {
        ShowHearts();
        if (!DataManager.Instance.GameData.ReduceHearts)
        {
            infiniteLivesHolder.SetActive(true);
        }
    }

    private void ShowHearts()
    {
        heartsDisplay.text = DataManager.Instance.PlayerData.Hearts.ToString();
    }

    private void Update()
    {
        ShowTimer();
    }

    private void ShowTimer()
    {
        if (HeartsManager.Instance.IsFull)
        {
            timerDisplay.text = "Full";
            return;
        }

        int _secondsForNext = HeartsManager.Instance.SecondsLeftForAnotherHeart;
        TimeSpan _timeForNextHeart = new TimeSpan(0, 0, _secondsForNext);
        timerDisplay.text = _timeForNextHeart.TotalMinutes > 1 ? $"{Convert.ToInt32(_timeForNextHeart.TotalMinutes)}m:{Convert.ToInt32(_timeForNextHeart.Seconds)}s" : $"{Convert.ToInt32(_timeForNextHeart.Seconds)}s";
    }
}
