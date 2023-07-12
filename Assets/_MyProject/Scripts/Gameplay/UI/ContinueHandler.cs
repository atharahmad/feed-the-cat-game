using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContinueHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerDisplay;
    [SerializeField] private Image timerBar;
    [SerializeField] private Button continueWithCoinsButton;
    [SerializeField] private Button watchAdButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI priceDisplay; 

    [SerializeField] private LoseHandler loseHandler;
    [SerializeField] private int price;
    [SerializeField] private int continueTime;

    private float counter;
    bool hasUsed;

    public void Setup()
    {
        GamePlayManager.TimeScale = 0;
        counter = 0;
        gameObject.SetActive(true);
        if (hasUsed)
        {
            Lose();   
        }
    }
    
    private void OnEnable()
    {
        continueWithCoinsButton.onClick.AddListener(ContinueWithCoins);
        watchAdButton.onClick.AddListener(WatchAddToContinue);
        closeButton.onClick.AddListener(Lose);
        
        StartCoroutine(TimerRoutine());
        //priceDisplay.text = price+" Coins";
    }

    private void OnDisable()
    {
        continueWithCoinsButton.onClick.RemoveListener(ContinueWithCoins);
        watchAdButton.onClick.RemoveListener(WatchAddToContinue);
        closeButton.onClick.RemoveListener(Lose);
    }

    void ContinueWithCoins()
    {
        if (DataManager.Instance.PlayerData.Coins>=price)
        {
            DataManager.Instance.PlayerData.Coins -= price;
            Continue();
        }
    }

    void WatchAddToContinue()
    {
        if (!AdManager.Instance.IsInterstitialAdAvailable)
        {
            watchAdButton.interactable = false;
            return;
        }
        
        AdManager.Instance.WatchedAd.AddListener(Continue);
        AdManager.Instance.PlayInterstitialAd();
    }

    void Continue()
    {
        hasUsed = true;
        MeltedIceCreamHandler.Instance.SetToZero();
        GamePlayManager.Instance.RecoverToFullHealth();
        ResumeHandler.Instance.Resume();
        gameObject.SetActive(false);
    }

    IEnumerator TimerRoutine()
    {
        while (gameObject.activeSelf)
        {
            if (counter>=continueTime)
            {
                Lose();
                break;
            }
            counter += Time.deltaTime;
            //timerDisplay.text = (int)(continueTime - counter) + "...";
            timerBar.fillAmount = counter / continueTime;
            yield return null;
        }
    }

    void Lose()
    {
        loseHandler.Setup();
        gameObject.SetActive(false);
    }
}
