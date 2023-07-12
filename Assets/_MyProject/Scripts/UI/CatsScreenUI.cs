using UnityEngine;
using UnityEngine.UI;

public class CatsScreenUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(Close);
    }

    void Close()
    {
        SceneManager.LoadMainMenu();
    }
}
