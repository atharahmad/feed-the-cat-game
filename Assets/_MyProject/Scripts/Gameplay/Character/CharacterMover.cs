using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMover : MonoBehaviour, IPointerMoveHandler, IPointerUpHandler, IPointerDownHandler
{
    //[SerializeField] float speed;
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;

    private delegate Vector3 GetPositionHandler();

    [SerializeField] private Transform playerHolder;
    private Vector2 clickedAt;


    private void Awake()
    {
    }

    private void Update()
    {
        if (GamePlayManager.TimeScale == 0)
        {
            return;
        }
        if (GamePlayManager.input.Inputting()) 
        {
            playerHolder.position += new Vector3(GamePlayManager.input.Drag().x, 0, 0);
        }
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        GamePlayManager.input.OnInputDown();
        clickedAt = _eventData.position;
    }

    public void OnPointerMove(PointerEventData _eventData)
    {
        if (clickedAt == default)
        {
            return;
        }
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        GamePlayManager.input.OnInputUp();
        clickedAt = default;
    }
}
