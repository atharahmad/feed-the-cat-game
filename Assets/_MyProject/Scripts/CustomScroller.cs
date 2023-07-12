using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomScroller : MonoBehaviour
{
    public Vector2 lastMousePos;
    public float movementStartRange;
    public float movementEndRange;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Drag()
    {
        float change = Input.mousePosition.y - lastMousePos.y;
        GetComponent<RectTransform>().anchoredPosition += Vector2.up * change;
        if (GetComponent<RectTransform>().anchoredPosition.y < movementStartRange)
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, movementStartRange);
        if (GetComponent<RectTransform>().anchoredPosition.y > movementEndRange)
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, movementEndRange);
        lastMousePos = Input.mousePosition;
    }
    public void DragStart()
    {
        lastMousePos = Input.mousePosition;
    }
}
