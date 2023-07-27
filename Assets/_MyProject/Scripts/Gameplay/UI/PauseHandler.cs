using UnityEngine;
using UnityEngine.UI;


public class PauseHandler : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(Resume);
        restartButton.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(MainMenu);
    }

    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(Resume);
        restartButton.onClick.RemoveListener(Restart);
        menuButton.onClick.RemoveListener(MainMenu);
        ResumeHandler.Instance.Resume();
    }

    public void Setup()
    {
        gameObject.SetActive(true);
        GamePlayManager.TimeScale = 0;
    }

    private void Resume()
    {
        gameObject.SetActive(false);
    }

    private void Restart()
    {
        SceneController.LoadGamePlay();
    }

    private void MainMenu()
    {
        SceneController.LoadMainMenu();
    }
}
