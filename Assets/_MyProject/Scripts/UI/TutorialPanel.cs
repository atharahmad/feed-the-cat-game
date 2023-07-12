using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject loadingHolder;
    [SerializeField] private Image loadingDisplay;
    [SerializeField] private GameObject moveHolder;
    [SerializeField] private GameObject dontEatHolder;
    [SerializeField] private GameObject eatHolder;
    [SerializeField] private GameObject clickToPlayHolder;
    
    [SerializeField] private TextMeshProUGUI timerDisplay;

    private readonly int timePerSection = 5;
    private Button button;
    
    private void Awake()
    {
        if (!AccountManager.isNewAccount)
        {
            Destroy(gameObject);
        }
        else
        {
            button = GetComponent<Button>();
            holder.SetActive(true);
        }
    }

    private void Start()
    {
        holder.SetActive(true);
        loadingHolder.SetActive(true);
        float _duration = timePerSection;
        Sequence _sequence = DOTween.Sequence();
        _sequence.Append(DOTween.To(() => 0f, _x => loadingDisplay.fillAmount = _x, 1f, _duration));
        _sequence.OnComplete(() =>
        {
            loadingHolder.SetActive(false);
            moveHolder.SetActive(true);
            StartCoroutine(FeedTutorialRoutine());
        });
    }

    IEnumerator FeedTutorialRoutine()
    {
        moveHolder.SetActive(true);
        StartCoroutine(ShowTimer());
        yield return new WaitForSeconds(timePerSection+0.5f);
        moveHolder.SetActive(false);
        dontEatHolder.SetActive(true);
        StartCoroutine(ShowTimer());
        yield return new WaitForSeconds(timePerSection+0.5f);
        dontEatHolder.SetActive(false);
        eatHolder.SetActive(true);
        StartCoroutine(ShowTimer());
        yield return new WaitForSeconds(timePerSection+0.5f);
        eatHolder.SetActive(false);
        clickToPlayHolder.SetActive(true);
        timerDisplay.text = string.Empty;
        button.onClick.AddListener(Play);
    }

    IEnumerator ShowTimer()
    {
        for (int _i = timePerSection; _i > 0; _i--)
        {
            timerDisplay.text = _i.ToString();
            yield return new WaitForSeconds(1);
        }
    }

    void Play()
    {
        AccountManager.isNewAccount = false;
        CatSO.SelectedCat = CatSO.Get(DataManager.Instance.PlayerData.SelectedCat);
        SceneManager.LoadGamePlay();
    }
}
