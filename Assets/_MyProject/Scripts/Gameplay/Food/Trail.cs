using UnityEngine;

public class Trail : MonoBehaviour
{
    public Transform reference;
    public void Setup(Transform _obj, Sprite image)
    {
        reference = _obj;
        GetComponent<SpriteRenderer>().sprite = image;
        GetComponent<TrailRenderer>().startWidth = GetComponent<TrailRenderer>().endWidth = GetComponent<SpriteRenderer>().bounds.size.x / 1.4f;
    }

    void LateUpdate()
    {
        if (reference != null)
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(reference.position);
    }
}
