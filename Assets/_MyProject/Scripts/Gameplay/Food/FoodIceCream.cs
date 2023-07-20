using UnityEngine;
using UnityEngine.UI;

public class FoodIceCream : FoodController
{
    [SerializeField] private ValueDisplay valueDisplayPrefab;
    [SerializeField] private IceCreamSO[] iceCreams;
    [SerializeField] private Image imageDisplay;
    private IceCreamSO iceCream;


    public void SpawnCoinText(int _amount)
    {
        ValueDisplay _textObj = Instantiate(valueDisplayPrefab, GameObject.FindGameObjectWithTag("FoodHolder").transform);
        _textObj.Setup(_amount);
        _textObj.transform.localPosition = transform.localPosition;
    }

    public override void Setup(bool _randomRotation = true)
    {
        base.Setup(_randomRotation);
        iceCream = iceCreams[Random.Range(0, iceCreams.Length)];
        imageDisplay.sprite = iceCream.Whole;
        imageDisplay.SetNativeSize();
        GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta * 1.1f;
    }

    protected override void HandleCollisionWithBorder()
    {
        OnReachedBorder?.Invoke(this);
        StartCoroutine(Melt(iceCream));
    }
}
