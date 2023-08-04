using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public Transform reference;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Setup(Transform _obj,Sprite image)
    {
        reference = _obj;
        GetComponent<SpriteRenderer>().sprite = image;
        gameObject.AddComponent<PolygonCollider2D>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (reference != null)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(reference.position);
            pos.z = 0;
            transform.position = pos;
            transform.rotation = reference.rotation;
            Debug.Log(GetComponent<PolygonCollider2D>().bounds.size.x);
            GetComponent<TrailRenderer>().startWidth = GetComponent<PolygonCollider2D>().bounds.size.x/1.4f;
            GetComponent<TrailRenderer>().endWidth = GetComponent<PolygonCollider2D>().bounds.size.x/1.4f;
        }
    }
}
