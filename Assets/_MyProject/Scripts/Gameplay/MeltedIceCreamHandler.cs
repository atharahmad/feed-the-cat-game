using UnityEngine;
using UnityEngine.UI;


public class MeltedIceCreamHandler : MonoBehaviour
{
    public static MeltedIceCreamHandler Instance;
    [SerializeField] private float meltingSpeed;
    [SerializeField] private float movingSpeed;
    [SerializeField] private RawImage rawImage;
    private Transform myTransform;
    private Rect uvRect;
    private float size;

    public float Size => size;

    private void Awake()
    {
        myTransform = transform;
        uvRect = rawImage.uvRect;
        Instance = this;
    }

    private void OnEnable()
    {
        FoodController.OnMelted += IncreaseMeltedIce;
    }

    private void OnDisable()
    {
        FoodController.OnMelted -= IncreaseMeltedIce;
    }

    private void Start()
    {
        size = myTransform.localScale.y;
    }

    private void IncreaseMeltedIce()
    {
        size += meltingSpeed;
        if (myTransform.localScale.y >= .7f)
        {
            Routine.WaitAndCall (0.5f, () => GamePlayManager.Instance.Drown());
        }
    }

    public void DecreaseMeltedIce(float _amount)
    {
        size -= _amount;
        if (size < 0)
        {
            size = 0;
        }

        UpdateVisual();
    }

    public void SetToZero()
    {
        size = 0;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        var _localScale = myTransform.localScale;
        _localScale = new Vector3(_localScale.x, size, _localScale.z);
        myTransform.localScale = _localScale;
    }

    private void Update()
    {
        uvRect.x += movingSpeed * Time.deltaTime;
        rawImage.uvRect = uvRect;
        myTransform.localScale = new Vector3(myTransform.localScale.x, Mathf.MoveTowards(myTransform.localScale.y, size, Time.deltaTime * meltingSpeed), myTransform.localScale.z);
    }
}
