using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelHandler : MonoBehaviour
{
    public GameObject levelPrefab;
    public List<Vector2> levelsHolders;
    public int levelsUnlocked;
    public static LevelHandler instance;
    private void Awake()
    {
        instance = this;
        Initialize();
    }
    void Start()
    {
    }
    void Initialize()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelscompleted");
        levelsUnlocked -= 1;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject g = (GameObject)Instantiate(levelPrefab, transform.GetChild(i).transform);
            int levelNo = i;
            g.GetComponent<Button>().onClick.AddListener(() => MainMenuUI.instance.PlayLevel(levelNo));
            if(i==levelsUnlocked)
                g.transform.GetChild(2).gameObject.SetActive(true);
            if (i <= levelsUnlocked)
                g.transform.GetChild(1).gameObject.SetActive(false);
            g.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;// transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
        }
        StartCoroutine(Scroll());
    }
    IEnumerator Scroll()
    {
        yield return new WaitForSeconds(.5f);
        Vector3 pos = transform.GetChild(levelsUnlocked).position;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (Camera.main.ScreenToViewportPoint(pos).y > .35f)
                transform.parent.position -= Vector3.up * Screen.height / 100f;
            if (Camera.main.ScreenToViewportPoint(pos).y < .33f)
                transform.parent.position += Vector3.up * Screen.height / 100f;
            if (Camera.main.ScreenToViewportPoint(pos).y < .35 && Camera.main.ScreenToViewportPoint(pos).y > .33f)
                break;

            pos = transform.GetChild(levelsUnlocked).position;
        }
    }
}
