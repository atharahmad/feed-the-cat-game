using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoreLivesDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI heartsDisplay;
    [SerializeField] private GameObject addMore;
    [SerializeField] private TextMeshProUGUI timerDisplay;
    [SerializeField] private Button freeHearts;
    [SerializeField] private Button refill;
    [SerializeField] private TextMeshProUGUI refillPriceDisplay;
    [SerializeField] private Button closeButton;
    [SerializeField] private int refillPrice;

    public void Setup()
    {
        gameObject.SetActive(true);
    }
    
    private void OnEnable()
    {
        freeHearts.onClick.AddListener(ShowFreeHearts);
        refill.onClick.AddListener(Refill);
        closeButton.onClick.AddListener(Close);

        refillPriceDisplay.text = refillPrice.ToString();
    }

    private void OnDisable()
    {
        freeHearts.onClick.RemoveListener(ShowFreeHearts);
        refill.onClick.RemoveListener(Refill);
        closeButton.onClick.RemoveListener(Close);
    }

    void ShowFreeHearts()
    {
        //todo implement me
    }

    void Refill()
    {
        if (DataManager.Instance.PlayerData.Coins>=refillPrice)
        {
            DataManager.Instance.PlayerData.Coins -= refillPrice;
            HeartsManager.Instance.Refill();
            Close();
        }
        else
        {
            UIManager.Instance.OkDialog.Show("You don't have enaught coins");
        }
    }

    void Close()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
       addMore.SetActive(!HeartsManager.Instance.IsFull);
       heartsDisplay.text = DataManager.Instance.PlayerData.Hearts.ToString();
       
       ShowTimer();
    }

    void ShowTimer()
    {
        if (HeartsManager.Instance.IsFull)
        {
            timerDisplay.text = "00:00:00";
            return;
        }

        TimeSpan _time = new TimeSpan(0,0,HeartsManager.Instance.SecondsLeftForAnotherHeart);
        string _hours = _time.Hours < 10 ? "0" + _time.Hours : _time.Hours.ToString();
        string _minutes = _time.Minutes < 10 ? "0" + _time.Minutes : _time.Minutes.ToString();
        string _seconds = _time.Seconds < 10 ? "0" + _time.Seconds : _time.Seconds.ToString(); 
        timerDisplay.text = $"{_hours}:{_minutes}:{_seconds}";
    }
}
