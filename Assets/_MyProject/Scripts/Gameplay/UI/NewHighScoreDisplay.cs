using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewHighScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private Button closeButton;

    public void Setup()
    {
        scoreDisplay.text = DataManager.Instance.PlayerData.HighScore.ToString();
        gameObject.SetActive(true);
    }
    
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
        gameObject.SetActive(false);
    }
    
}
