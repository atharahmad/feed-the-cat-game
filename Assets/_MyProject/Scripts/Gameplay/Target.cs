using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI targetDisplay;
    private Sprite skin;
    private int targetVal;
    private GameObject iceCreamAnim;

    void Start()
    {
        iceCreamAnim = new GameObject();
        iceCreamAnim.transform.parent = FindAnyObjectByType<Canvas>().transform;
        iceCreamAnim.AddComponent<RectTransform>();
        iceCreamAnim.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;
        iceCreamAnim.AddComponent<Image>();
        iceCreamAnim.transform.localScale = Vector3.one;
        iceCreamAnim.SetActive(false);
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
                iceCreamAnim.SetActive(true);
                iceCreamAnim.GetComponent<Image>().sprite = skin;
                Routine.MoveConstant(iceCreamAnim.transform, CharacterVisual.Instance.mouthMask.position, transform.position, 0.04f, () => iceCreamAnim.SetActive(false));
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
