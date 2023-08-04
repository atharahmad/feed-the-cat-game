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
    [SerializeField] private GameObject winHandler;
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
            levelNo = PlayerPrefs.GetInt("currentlevel");
            DesignLevel();
        }
    }
    private void DesignLevel()
    {
        targets.Clear();
        Level _level = null;
        if (IO.DeserializeFile<Level>(levelNo.ToString() + ".txt", ref _level))
        {
            for(int i = 0; i < _level.skinIndexes.Length; i++)
            {
                targetList[i].Setup(_level.skinIndexes[i], _level.targetValues[i]);
                targets.Add(targetList[i]);
            }
        }
        else
        {
            int noOfTargets = 0;
            _level = new Level();
            if (levelNo < 5)
            {
                noOfTargets = 1;
            } 
            else if(levelNo >= 5 && levelNo < 10)
            {
                noOfTargets = 2;
            }
            else if (levelNo >= 10 && levelNo < 20)
            {
                noOfTargets = 3;
            }
            else if (levelNo >= 20 )
            {
                noOfTargets = 4;
            }
            _level.skinIndexes = new int[noOfTargets];
            _level.targetValues = new int[noOfTargets];
            for (int i = 0; i < noOfTargets; i++)
            {
                int _val = Random.Range(3, 7);
                int index = Random.Range(0, 20);
                targetList[i].Setup(index, _val);
                targets.Add(targetList[i]);
                _level.skinIndexes[i] = index;
                _level.targetValues[i] = _val;
            }
            IO.SerializeFile<Level>(levelNo.ToString() + ".txt", _level);
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
    public void CheckLevelComplete()
    {
        bool leveComplete = true;
        for(int i = 0; i < targets.Count; i++)
        {
            if (!targets[i].IsComplete())
            {
                leveComplete = false;
                break;
            }
        }
        if (leveComplete)
        {
            if (levelNo == PlayerPrefs.GetInt("levelno"))
            {
                levelNo++;
                PlayerPrefs.SetInt("levelno", levelNo);
            }
        }
        GamePlayManager.Instance.Pause();
        AudioManager.Instance.Play(AudioManager.VICTORY);
        Routine.WaitAndCall(0.5f, ()=> winHandler.SetActive(true));
    }

    public void GoToMainMenu()
    {
        SceneController.LoadMainMenu();
    }

}
