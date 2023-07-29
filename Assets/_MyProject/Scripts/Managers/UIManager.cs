using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Image loadingBar;
    public static UIManager Instance;
    [field: SerializeField] public OkDialog OkDialog { get; private set; }
    [field: SerializeField] public WaitingPanel WaitPanel { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {

        Routine.LerpConstant(loadingBar.fillAmount, 1, 0.02f, (fill) => loadingBar.fillAmount = fill, () => { loadingBar.fillAmount = 1; loadingPanel.SetActive(false); });

    }
}
