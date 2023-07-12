using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI Instance;
    [SerializeField] private TextMeshProUGUI highScoreDisplay;
    [SerializeField] private Button pauseButton;

    [SerializeField] private PauseHandler pauseHandler;
    [SerializeField] private ContinueHandler continueHandler;

    [field: SerializeField] public Transform LeftSpawnBoundary { get; private set; }
    [field: SerializeField] public Transform RightSpawnBoundary { get; private set; }

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


}
