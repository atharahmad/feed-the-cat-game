using UnityEngine;
using UnityEngine.UI;

public class AdHandler : MonoBehaviour
{
    public static AdHandler Instance;

    [SerializeField] private GameObject holder;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button reduceMeltedIceButton;
    [SerializeField] private Button getMultiplierButton;
    [SerializeField] private Button getMoreRewardsButton;
    [SerializeField] private Button increaseHealthButton;
    [Space()]
    [SerializeField]
    private MeltedIceCreamHandler meltedIceCreamHandler;

    private AdRewardTypes selectedRewardType;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        closeButton.onClick.AddListener(Close);
        reduceMeltedIceButton.onClick.AddListener(ReduceMeltedIceLevel);
        getMultiplierButton.onClick.AddListener(GetMultiplier);
        getMoreRewardsButton.onClick.AddListener(GetMoreRewards);
        increaseHealthButton.onClick.AddListener(IncreaseHealth);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(Close);
        reduceMeltedIceButton.onClick.RemoveListener(ReduceMeltedIceLevel);
        getMultiplierButton.onClick.RemoveListener(GetMultiplier);
        getMoreRewardsButton.onClick.RemoveListener(GetMoreRewards);
        increaseHealthButton.onClick.RemoveListener(IncreaseHealth);
    }

    public void Setup()
    {
        GamePlayManager.TimeScale = 0;
        holder.SetActive(true);
    }

    private void Close()
    {
        holder.SetActive(false);
        ResumeHandler.Instance.Resume();
    }

    private void ReduceMeltedIceLevel()
    {
        selectedRewardType = AdRewardTypes.ReduceMeltedIceLevel;
        PlayAd();
    }

    private void GetMultiplier()
    {
        selectedRewardType = AdRewardTypes.GetMultiplier;
        PlayAd();
    }

    private void GetMoreRewards()
    {
    }

    private void IncreaseHealth()
    {
        selectedRewardType = AdRewardTypes.IncreaseHealth;
        PlayAd();
    }

    private void PlayAd()
    {
        if (!AdManager.Instance.IsRewardedAdAvailable)
        {
            UIManager.Instance.OkDialog.Show("Ad is not ready");
            UIManager.Instance.OkDialog.OkPressed.AddListener(ResumeHandler.Instance.Resume);
            return;
        }
        AdManager.Instance.WatchedAd.AddListener(Reward);
        AdManager.Instance.PlayRewardedAd();
    }

    private void Reward()
    {
        switch (selectedRewardType)
        {
            case AdRewardTypes.ReduceMeltedIceLevel:
                meltedIceCreamHandler.SetToZero();
                break;
            case AdRewardTypes.GetMultiplier:
                GamePlayManager.Instance.IncreaseMultiplier();
                break;
            case AdRewardTypes.GetMoreRewards:
                throw new System.Exception("Don`t know how to handle get more rewards");
            case AdRewardTypes.IncreaseHealth:
                GamePlayManager.Instance.RecoverToFullHealth();
                break;
        }
        Close();
    }
}
