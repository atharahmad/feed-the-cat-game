using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    
    [SerializeField] private Button playButton;
    [SerializeField] private Transform levelHolder;
    [SerializeField] private Button coinsButton;
    [SerializeField] private Button livesButton;
    [SerializeField] private Button keysButton;
    [SerializeField] private Button settingsButton;
    
    [SerializeField] private MoreLivesDisplay moreLivesDisplay;
    [SerializeField] private KeysPanel keysDisplay;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Image loadingBar;

    private void OnEnable()
    {

        settingsButton.onClick.AddListener(ShowSettings);
        playButton.onClick.AddListener(ShowPickCharacter);
        coinsButton.onClick.AddListener(ShowShop);
        livesButton.onClick.AddListener(ShowMoreLives);
        keysButton.onClick.AddListener(ShowKeys);
    }

    private void OnDisable()
    {
        settingsButton.onClick.AddListener(ShowSettings);
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
        SceneController.LoadGamePlay();
    }

    private void ShowShop()
    {
        SceneController.LoadShop();
    }

  

    private void Start()
    {
        Routine.LerpConstant(loadingBar.fillAmount, 1, 0.02f, (fill) => loadingBar.fillAmount = fill, () => { loadingBar.fillAmount = 1; loadingPanel.SetActive(false); });
        AudioManager.Instance.PlayBackgroundMusic(AudioManager.MAIN_THEME_SONG);
        var _levelHolderTransform = levelHolder.transform;
        _levelHolderTransform.localPosition = new Vector3(_levelHolderTransform.localPosition.x,10000, levelHolder.localPosition.z);
    }
}
