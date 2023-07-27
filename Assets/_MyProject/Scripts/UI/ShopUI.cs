using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Button leaveButton;
    private void OnEnable()
    {
        leaveButton.onClick.AddListener(ShowMainMenu);
    }

    private void OnDisable()
    {
        leaveButton.onClick.RemoveListener(ShowMainMenu);
    }

    private void ShowMainMenu()
    {
        SceneController.LoadMainMenu();
    }

}
