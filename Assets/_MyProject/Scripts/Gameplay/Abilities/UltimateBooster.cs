using UnityEngine;
using UnityEngine.UI;

public class UltimateBooster : MonoBehaviour
{
    [SerializeField] private Image foreground;
    [SerializeField] private Button useButton;
    [SerializeField] private int amountOfIceCreamsNeeded;

    private int eatenIceCreams;

    private void OnEnable()
    {
        useButton.onClick.AddListener(Use);
        CharacterController.OnEatenIceCream += EatenIceCream;
    }

    private void OnDisable()
    {
        useButton.onClick.RemoveListener(Use);
        CharacterController.OnEatenIceCream -= EatenIceCream;
    }

    private void Use()
    {
        if (eatenIceCreams<amountOfIceCreamsNeeded)
        {
            return;
        }

        eatenIceCreams = 0;
        UpdateVisual();
    }

    private void EatenIceCream()
    {
        if (eatenIceCreams>=amountOfIceCreamsNeeded)
        {
            return;
        }
        
        eatenIceCreams++;
        UpdateVisual();
    }

    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (eatenIceCreams==0)
        {
            foreground.fillAmount = 0;
            return;
        }

        foreground.fillAmount = (float)eatenIceCreams / amountOfIceCreamsNeeded;
    }
}
