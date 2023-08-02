using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Target : MonoBehaviour
{
    private Sprite skin;
    [SerializeField] TextMeshProUGUI targetDisplay;
    int targetVal;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Setup(int index,int _val)
    {
        GetComponent<Image>().sprite = GamePlayUI.Instance.skins[index];
        skin = GamePlayUI.Instance.skins[index];
        targetVal = _val;
        targetDisplay.text = targetVal.ToString();
        gameObject.SetActive(true);
    }
    public void Decrease(Sprite _skin)
    {
        if (skin == _skin)
        {
            if (targetVal > 0)
            {
                targetVal--;
                targetDisplay.text = targetVal.ToString();
                if (targetVal == 0)
                    GamePlayUI.Instance.CheckLevelComplete();
            }
        }
    }
    public bool IsComplete()
    {
        if (targetVal > 0)
            return false;
        else
            return true;
    }
}
