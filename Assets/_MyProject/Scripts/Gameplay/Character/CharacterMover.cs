using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMover : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    //[SerializeField] float speed;
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;

    private delegate Vector3 GetPositionHandler();
    private bool inputting;

    [SerializeField] private Transform playerHolder;


    private void Update()
    {
        if (GamePlayManager.TimeScale == 0)
        {
            return;
        }
        if (inputting) 
        {
            playerHolder.position += new Vector3(GamePlayManager.input.Drag().x, 0, 0);
        }
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        GamePlayManager.input.OnInputDown();
        inputting = true;
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        GamePlayManager.input.OnInputUp();
        inputting = false;
    }
}
