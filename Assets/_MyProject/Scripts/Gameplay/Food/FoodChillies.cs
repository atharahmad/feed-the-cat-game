using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FoodChillies : FoodController
{
    [HideInInspector] public bool CanSplit = true;
    [SerializeField] private float splitOffset;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image image;

    public override void Setup(bool _randomRotation = true)
    {
        base.Setup(_randomRotation);
        image.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    protected override void OnTriggerEnter2D(Collider2D _collision)
    {
        base.OnTriggerEnter2D(_collision);

        if (_collision.CompareTag("Split"))
        {

            if (!CanSplit)
            {
                return;
            }

            CanSplit = false;

            SpawnNewChili(Vector3.left);
            SpawnNewChili(Vector3.right);
        }

        void SpawnNewChili(Vector3 _direction)
        {
            FoodChillies _newChilli = Instantiate(this, FoodSpawner.Instance.FoodHolder);
            _newChilli.transform.position = transform.position;
            _newChilli.CanSplit = false;
            _newChilli.Setup();
            _newChilli.image.sprite = image.sprite;//ensure new chili is same as original chili 
            _newChilli.Move(_direction);
        }
    }

    protected override bool AllowAutomaticEating()
    {
        return false;
    }

    private void Move(Vector3 _direction)
    {
        var _position = transform.position;
        float _endPositionX = _position.x + (_direction.x * splitOffset);
        float _currentX = _position.x;
        float _animationTime = 0.3f;
        Sequence _sequence = DOTween.Sequence();
        _sequence.Append(DOTween.To(() => _currentX, _x => _currentX = _x, _endPositionX, _animationTime).OnUpdate(() =>
        {
            transform.position = new Vector3(_currentX, _position.y, _position.z);
            if (transform.position.x < Screen.width / 12f || transform.position.x > Screen.width - Screen.width / 12f)
            {
                _sequence.Kill();
            }
        }));
        _sequence.Play();
    }
}
 