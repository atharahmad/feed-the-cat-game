using UnityEngine;
using TMPro;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsDisplay;

    private void OnEnable()
    {
        DataManager.Instance.PlayerData.UpdatedCoins += ShowCoins;
    }

    private void OnDisable()
    {
        DataManager.Instance.PlayerData.UpdatedCoins -= ShowCoins;
    }

    private void Start()
    {
        ShowCoins();
    }

    private void ShowCoins()
    {
       coinsDisplay.text = DataManager.Instance.PlayerData.Coins.ToString();
    }
}
