using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMover : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    //[SerializeField] float speed;
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;

    private delegate Vector3 GetPositionHandler();
    private bool inputting;
    private float minLimit;
    private float maxLimit;

    [SerializeField] private Transform playerHolder;

    public static CharacterMover Instance;
    private void Awake() => Instance = this;
    private void Start()
    {
        minLimit = Screen.width / 12f;
        maxLimit = Screen.width - Screen.width / 12f;
    }

    private void Update()
    {
        if (GamePlayManager.TimeScale == 0)
        {
            return;
        }
        if (inputting) 
        {
            playerHolder.position += new Vector3(GamePlayManager.input.Drag().x, 0, 0);
            if (playerHolder.position.x < minLimit)
                playerHolder.position = new Vector3 (minLimit, playerHolder.position.y, playerHolder.position.z);
            if (playerHolder.position.x > maxLimit)
                playerHolder.position = new Vector3 (maxLimit, playerHolder.position.y, playerHolder.position.z);
        }
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        if (Tutorial.instance.hintPanel.gameObject.activeInHierarchy) { 
            Tutorial.instance.Toggle(false);
            FoodSpawner.Instance.enabled = true;
        }
        GamePlayManager.input.OnInputDown();
        inputting = true;
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        GamePlayManager.input.OnInputUp();
        inputting = false;
    }
}
