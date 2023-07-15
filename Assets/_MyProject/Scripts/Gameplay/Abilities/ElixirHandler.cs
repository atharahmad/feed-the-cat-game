using UnityEngine;

public class ElixirHandler : BoosterBase
{
    public static bool IsActive;

    [SerializeField] private float duration;

    protected override void SubscribeEvents()
    {
        SuperPowersHandler.ElixirClicked += Use;
        DataManager.Instance.PlayerData.UpdatedElixirs += ShowGraphics;
    }

    protected override void UnregisterEvents()
    {
        SuperPowersHandler.ElixirClicked -= Use;
        DataManager.Instance.PlayerData.UpdatedElixirs -= ShowGraphics;
    }

    protected override void Use()
    {
        if (IsActive)
        {
            return;
        }

        if (DataManager.Instance.PlayerData.Elixirs > 0)
        {
            AudioManager.Instance.Play(AudioManager.BONUS_ACTIVATED);
            DataManager.Instance.PlayerData.Elixirs--;
            IsActive = true;
            StartCoroutine(DurationBasedBooster(duration));
        }

        ShowGraphics();
    }
    
    protected override void FinishedDurationBasedBoost()
    {
        IsActive = false;
    }

    protected override void ShowGraphics()
    {
        displayImage.sprite = DataManager.Instance.PlayerData.Elixirs > 0 ? boosterSO.AvailableSprite : boosterSO.NotAvailableSprite;
        amountDisplay.text = DataManager.Instance.PlayerData.Elixirs.ToString();
    }
}