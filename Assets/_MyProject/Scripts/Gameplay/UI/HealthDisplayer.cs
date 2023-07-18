using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayer : MonoBehaviour
{
    [SerializeField] private Image healthDisplay;

    private void OnEnable()
    {
        GamePlayManager.UpdatedCurrentAmountOfLives += ShowHealth;
    }

    private void OnDisable()
    {
        GamePlayManager.UpdatedCurrentAmountOfLives -= ShowHealth;
    }

    private void ShowHealth()
    {
        Routine.Lerp(healthDisplay.fillAmount, (float)GamePlayManager.Instance.CurrentAmountOfLives / GamePlayManager.Instance.MaxAmountOfLives, .2f, (fill) => healthDisplay.fillAmount = fill);
        //healthDisplay.fillAmount =  (float)GamePlayManager.Instance.CurrentAmountOfLives / GamePlayManager.Instance.MaxAmountOfLives;
    }
}
