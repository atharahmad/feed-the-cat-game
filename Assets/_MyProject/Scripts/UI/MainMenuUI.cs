using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI instance;
    [SerializeField] private Button playlevelButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Transform levelHolder;
    [SerializeField] private Button coinsButton;
    [SerializeField] private Button livesButton;
    [SerializeField] private Button keysButton;
    [SerializeField] private Button settingsButton;
    
    [SerializeField] private MoreLivesDisplay moreLivesDisplay;
    [SerializeField] private KeysPanel keysDisplay;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private TextMeshProUGUI levelDisplay;
    private int levelNo;
    private void OnEnable()
    {
        playlevelButton.onClick.AddListener(PlayLevel);
        settingsButton.onClick.AddListener(ShowSettings);
        playButton.onClick.AddListener(ShowPickCharacter);
        coinsButton.onClick.AddListener(ShowShop);
        livesButton.onClick.AddListener(ShowMoreLives);
        keysButton.onClick.AddListener(ShowKeys);
    }

    private void OnDisable()
    {
        playlevelButton.onClick.RemoveListener(PlayLevel);
        settingsButton.onClick.RemoveListener(ShowSettings);
        playButton.onClick.RemoveListener(ShowPickCharacter);
        coinsButton.onClick.RemoveListener(ShowShop);
        livesButton.onClick.RemoveListener(ShowMoreLives);
        keysButton.onClick.RemoveListener(ShowKeys);
    }

    void ShowSettings()
    {
        settingsUI.Setup();
    }
    
    void ShowMoreLives()
    {
        moreLivesDisplay.Setup();
    }

    void ShowKeys()
    {
        keysDisplay.Setup();
    }
    
    private void ShowPickCharacter()
    {
        if (DataManager.Instance.PlayerData.Hearts<=0)
        {
            UIManager.Instance.OkDialog.Show("You don’t have enough hearts!");
            return;
        }
        AudioManager.Instance.Play(AudioManager.CAT_SELECT);
        CatSO.SelectedCat = CatSO.Get(DataManager.Instance.PlayerData.SelectedCat);
        PlayerPrefs.SetInt("gametype", 0);
        SceneController.LoadGamePlay();
    }
    private void PlayLevel()
    {
        if (DataManager.Instance.PlayerData.Hearts <= 0)
        {
            UIManager.Instance.OkDialog.Show("You don’t have enough hearts!");
            return;
        }
        AudioManager.Instance.Play(AudioManager.CAT_SELECT);
        CatSO.SelectedCat = CatSO.Get(DataManager.Instance.PlayerData.SelectedCat);
        PlayerPrefs.SetInt("gametype", 1);
        PlayerPrefs.SetInt("currentlevel", levelNo);
        SceneController.LoadGamePlay();
    }
    public void PlayLevel(int _val)
    {
        if (DataManager.Instance.PlayerData.Hearts <= 0)
        {
            UIManager.Instance.OkDialog.Show("You don’t have enough hearts!");
            return;
        }
        AudioManager.Instance.Play(AudioManager.CAT_SELECT);
        CatSO.SelectedCat = CatSO.Get(DataManager.Instance.PlayerData.SelectedCat);
        PlayerPrefs.SetInt("gametype", 1);
        PlayerPrefs.SetInt("currentlevel", _val);
        SceneController.LoadGamePlay();
    }
    private void ShowShop()
    {
        SceneController.LoadShop();
    }

  

    private void Start()
    {
        levelNo = PlayerPrefs.GetInt("levelno");
        levelDisplay.text = "Level " + (levelNo + 1).ToString();
        AudioManager.Instance.PlayBackgroundMusic(AudioManager.MAIN_THEME_SONG);
        var _levelHolderTransform = levelHolder.transform;
        _levelHolderTransform.localPosition = new Vector3(_levelHolderTransform.localPosition.x,10000, levelHolder.localPosition.z);
    }
    private void Awake()
    {
        instance = this;
    }
}
