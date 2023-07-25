using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI Instance;
    [SerializeField] private Button pauseButton;
    [SerializeField] private PauseHandler pauseHandler;
    [SerializeField] private ContinueHandler continueHandler;
    [SerializeField] private TextMeshProUGUI highScoreDisplay;
    [SerializeField] private TextMeshProUGUI timerDisplay;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        GamePlayManager.GameEnded += HandleGameEnded;
        pauseButton.onClick.AddListener(Pause);
    }

    private void OnDisable()
    {
        GamePlayManager.GameEnded -= HandleGameEnded;
        pauseButton.onClick.RemoveListener(Pause);
    }

    private void Pause()
    {
        if (GamePlayManager.TimeScale==0)
        {
            return;
        }
        pauseHandler.Setup();
    }

    private void HandleGameEnded(bool _result)
    {
        if (_result)
        {
            //to-do handle game won
        }
        else
        {
            continueHandler.Setup();
        }
    }


    private void Start()
    {
        ShowHighScore();
    }

    private void ShowHighScore()
    {
        highScoreDisplay.text = GamePlayManager.ScoreToString(DataManager.Instance.PlayerData.HighScore);
    }
    public void SetTimer(float _time)
    {
        timerDisplay.text = _time.ToString()+" Sec";
    }

}
