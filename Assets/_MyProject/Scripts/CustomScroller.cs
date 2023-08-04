using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomScroller : MonoBehaviour
{
    public Vector2 lastMousePos;
    public Vector2[] targetPosition;
    float startTime;
    [SerializeField] private float speed;
    public float swipeSpeed = 1;
    public RectTransform[] screens;
    public int[] screenNumbers;
    public bool[] screenChnaged;
    float change;
    // Start is called before the first frame update
    void Start()
    {
        screenChnaged = new bool[2];
        targetPosition = new Vector2[2];
        screenNumbers = new int[2];
        for(int i = 0; i < 2; i++)
        {
            targetPosition[i] = screens[i].anchoredPosition;
        }
        screenNumbers[0] = 0;
        screenNumbers[1] = 1;
        Debug.Log(targetPosition[0]);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            screens[i].anchoredPosition = Vector3.Lerp(screens[i].anchoredPosition, targetPosition[i], speed);
            if (change > 0)
            {
                if (!GetComponent<RectTransform>().rect.Contains(screens[i].anchoredPosition + Vector2.down * screens[i].sizeDelta.y/2.1f))
                {
                    if (!screenChnaged[i])
                    {
                        Vector2 newPosition = screens[(i + 1) % 2].anchoredPosition - new Vector2(0, 1920);
                        Vector2 offset = newPosition - screens[i].anchoredPosition;
                        screens[i].anchoredPosition += offset;
                        targetPosition[i] += offset;
                        screenNumbers[i] -= 2;
                        screenChnaged[i] = true;
                    }
                }
                else
                {
                    screenChnaged[i] = false;
                }
            }
            if (change < 0)
            {
                if (!GetComponent<RectTransform>().rect.Contains(screens[i].anchoredPosition + Vector2.up * screens[i].sizeDelta.y / 2.1f))
                {
                    if (!screenChnaged[i])
                    {
                        Vector2 newPosition = screens[(i + 1) % 2].anchoredPosition + new Vector2(0, 1920);
                        Vector2 offset = newPosition - screens[i].anchoredPosition;
                        screens[i].anchoredPosition += offset;
                        targetPosition[i] += offset;
                        screenNumbers[i] += 2;
                        screenChnaged[i] = true;
                    }
                }
                else
                {
                    screenChnaged[i] = false;
                }
            }
        }
    }
    public void Drag()
    {
        change = Input.mousePosition.y - lastMousePos.y;
        swipeSpeed = (Time.time - startTime) > 0 ? (Time.time - startTime) : 1;
        float distance = (change) / (swipeSpeed * 10);
        for (int i = 0; i < 2; i++)
        {
            if (screenNumbers[i] == 0)
            {
                if (targetPosition[i].y + distance > 0 && screens[i].anchoredPosition.y <= 300)
                {
                    distance = -targetPosition[i].y;
                    break;
                }
            }
            if (screenNumbers[i] == 2)
            {
                if (targetPosition[i].y + distance < 0 && screens[i].anchoredPosition.y >= -300)
                {
                    distance = -targetPosition[i].y;
                    break;
                }
            }
        }
        for(int i = 0; i < 2; i++)
        {
            targetPosition[i].y = targetPosition[i].y + distance;
        }
        lastMousePos = Input.mousePosition;
        startTime = Time.time;
    }
    public void DragStart()
    {
        lastMousePos = Input.mousePosition;
        startTime = Time.time;

    }
    public void DragEnd()
    {
        float change = Input.mousePosition.y - lastMousePos.y;
        float distance = change;
        for (int i = 0; i < 2; i++)
        {
            if (screenNumbers[i] == 0)
            {
                if (targetPosition[i].y + distance > 0 && screens[i].anchoredPosition.y <= 300)
                {
                    distance = -targetPosition[i].y;
                    break;
                }
            }
            if (screenNumbers[i] == 2)
            {
                if (targetPosition[i].y + distance < 0 && screens[i].anchoredPosition.y >= -300)
                {
                    distance = -targetPosition[i].y;
                    break;
                }
            }
        }
        for (int i = 0; i < 2; i++)
        {
            targetPosition[i].y = targetPosition[i].y + distance;
        }

    }
}
