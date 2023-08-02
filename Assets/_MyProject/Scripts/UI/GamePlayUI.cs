using UnityEngine;
using System.Collections.Generic;
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
    [SerializeField] private GameObject levelPlay;
    [SerializeField] private GameObject infinitePlay;
    [SerializeField] List<Target> targetList;
    public List<Sprite> skins;
    public List<Target> targets;
    int levelNo = -1;
    private void Awake()
    {
        Instance = this;

        if (PlayerPrefs.GetInt("gametype") == 1)
        {
            infinitePlay.SetActive(false);
            levelPlay.SetActive(true);
            levelNo = PlayerPrefs.GetInt("levelno");
            DesignLevel();
        }
    }
    private void DesignLevel()
    {
        if (levelNo < 5)
        {
            int _val = 5 * (levelNo + 1);
            targetList[0].Setup(Random.Range(0, 20), _val);
            targets.Add(targetList[0]);
        }
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
