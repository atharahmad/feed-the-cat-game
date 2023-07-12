using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseHandler : MonoBehaviour
{
    [SerializeField] private Button resetButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private NewHighScoreDisplay newHighScoreDisplay;
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI highScoreDisplay;
    [SerializeField] private TextMeshProUGUI bonusPawsDisplay;
    [SerializeField] private TextMeshProUGUI bonusElixirsDisplay;
    [SerializeField] private TextMeshProUGUI bonusExtraLivesDisplay;
    [SerializeField] private TextMeshProUGUI bonusBiscuitsDisplay;
    [SerializeField] private LevelUpHandler levelUpHandler;

    public void Setup()
    {
        if (GamePlayManager.Instance.Score > DataManager.Instance.PlayerData.HighScore)
        {
            DataManager.Instance.PlayerData.HighScore = GamePlayManager.Instance.Score;
            newHighScoreDisplay.Setup();
        }
        scoreDisplay.text = GamePlayManager.Instance.Score.ToString();
        highScoreDisplay.text = DataManager.Instance.PlayerData.HighScore.ToString();
        bonusPawsDisplay.text = "+"+levelUpHandler.PawsEarned;
        bonusElixirsDisplay.text = "+"+levelUpHandler.ElixirsEarned;
        bonusExtraLivesDisplay.text = "+"+levelUpHandler.ExtraLivesEarned;
        bonusBiscuitsDisplay.text = "+"+levelUpHandler.BiscuitsEarned;
        
        levelUpHandler.Check();
        
        
        gameObject.SetActive(true);

        resetButton.onClick.AddListener(Replay);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void OnDisable()
    {
        ResumeHandler.Instance.Resume();
        resetButton.onClick.RemoveListener(Replay);
        mainMenuButton.onClick.RemoveListener(MainMenu);
    }

    private void Replay()
    {
        SceneManager.LoadGamePlay();
    }

    private void MainMenu()
    {
        SceneManager.LoadMainMenu();
    }
}
