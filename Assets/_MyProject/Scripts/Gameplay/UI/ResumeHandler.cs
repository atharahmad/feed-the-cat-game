using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeHandler : MonoBehaviour
{

    public static ResumeHandler Instance;
    [SerializeField] private Image display;
    [SerializeField] private GameObject holder;
    [SerializeField] private List<SpriteArray> resumeTexts;

    private Sprite[] selectedSprites;

    private void Awake()
    {
        selectedSprites = resumeTexts[2].Sprites;
        Instance = this;
    }

    public void Resume()
    {
        StartCoroutine(ResumeRoutine());
    }


    private IEnumerator ResumeRoutine()
    {
        holder.SetActive(true);
        for (int _i = 0; _i <= 3; _i++)
        {
            display.sprite = selectedSprites[_i];
            display.SetNativeSize();
            yield return new WaitForSeconds(1);
        }

        GamePlayManager.TimeScale = 1;
        yield return new WaitForSeconds(0.5f);
        holder.SetActive(false);
    }
}
