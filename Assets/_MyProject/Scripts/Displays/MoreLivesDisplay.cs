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

        int _secondsForNext = HeartsManager.Instance.SecondsLeftForAnotherHeart;
        TimeSpan _timeForNextHeart = new TimeSpan(0, 0, _secondsForNext);
        timerDisplay.text = _timeForNextHeart.TotalMinutes > 1 ? $"{Convert.ToInt32(_timeForNextHeart.TotalMinutes)}m:{Convert.ToInt32(_timeForNextHeart.Seconds)}s" : $"{Convert.ToInt32(_timeForNextHeart.Seconds)}s";
    }
}
