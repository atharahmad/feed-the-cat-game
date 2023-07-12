using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeltedIceBorder : MonoBehaviour
{
    [SerializeField] private RectTransform meltedIce;
    private readonly Vector3 offset = new Vector3(0, -100, 0);
    private readonly float movingSpeed = -18;
    private readonly List<Transform> iceCreams = new List<Transform>();
    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;

    }

    private void OnEnable()
    {
        FoodController.OnReachedBorder += AddIceCream;
    }

    private void OnDisable()
    {
        FoodController.OnReachedBorder -= AddIceCream;
    }

    private void AddIceCream(FoodController _food)
    {
        if (_food.Type == FoodType.IceCream || _food.Type == FoodType.RewardingIceCream)
        {
            iceCreams.Add(_food.transform);
        }
    }

    public void Update()
    {
        var _iceCreamRect = meltedIce.rect;
        Vector3 _topLeftCorner = meltedIce.TransformPoint(new Vector3(_iceCreamRect.xMin, _iceCreamRect.yMax, 0f));
        myTransform.position = _topLeftCorner + (offset * MeltedIceCreamHandler.Instance.Size);
        myTransform.localRotation = Quaternion.identity;

        if (MeltedIceCreamHandler.Instance.Size==0)
        {
            return;
        }
        
        foreach (var _iceCream in iceCreams.ToList())
        {
            if (_iceCream == null)
            {
                iceCreams.Remove(_iceCream);
                continue;
            }

            var _iceCreamTransform = _iceCream.transform;
            var _iceCreamPosition = _iceCreamTransform.position;
            Vector3 _newPos = new Vector3(
                _iceCreamPosition.x,
                myTransform.position.y,
                _iceCreamPosition.z);
            _newPos.x += movingSpeed * Time.deltaTime;
            _iceCreamPosition = _newPos;
            _iceCreamTransform.position = _iceCreamPosition;
        }
    }
}
