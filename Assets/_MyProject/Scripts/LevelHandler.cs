using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelHandler : MonoBehaviour
{
    public GameObject levelPrefab;
    public List<Vector2> levelsHolders;
    public int levelsUnlocked;

    void Start()
    {
        Initialize();
        StartCoroutine(Scroll());
    }
    void Initialize()
    {
        levelsUnlocked = PlayerPrefs.GetInt("currentLevel");
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
    }
    IEnumerator Scroll()
    {
        yield return new WaitForSeconds(1);
        Vector3 pos = transform.GetChild(levelsUnlocked).position;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (Camera.main.ScreenToViewportPoint(pos).y > .35f)
                transform.parent.position -= Vector3.up * 2;
            if (Camera.main.ScreenToViewportPoint(pos).y < .33f)
                transform.parent.position += Vector3.up * 2;
            if (Camera.main.ScreenToViewportPoint(pos).y < .35 && Camera.main.ScreenToViewportPoint(pos).y > .33f)
                break;

            pos = transform.GetChild(levelsUnlocked).position;
        }
    }
}
