using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public GameObject levelPrefab;
    public List<Vector2> levelsHolders;
    public int levelsUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Initialize()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 0);
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject g = (GameObject)Instantiate(levelPrefab, transform.GetChild(i).transform);
            if(i==levelsUnlocked)
                g.transform.GetChild(2).gameObject.SetActive(true);
            if (i < levelsUnlocked)
                g.transform.GetChild(1).gameObject.SetActive(false);
            g.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;// transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
