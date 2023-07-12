using TMPro;
using UnityEngine;


public class ScoreDisplayHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;

    private void OnEnable()
    {
        GamePlayManager.UpdatedScore += ShowScore;
    }

    private void OnDisable()
    {
        GamePlayManager.UpdatedScore -= ShowScore;
    }

    private void ShowScore()
    {
        scoreDisplay.text = GamePlayManager.ScoreToString(GamePlayManager.Instance.Score);
    }
}
