using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FoodController : MonoBehaviour
{
    public static Action<FoodController> OnReachedBorder;
    public static Action OnMelted;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private int score;

    [SerializeField] private FoodType type;

    private bool fall;
    private float speed;
    private float torque;

    public bool isInMouth;
    public FoodType Type => type;
    public int Score => score;
    public virtual void Setup(bool _randomRotation = true)
    {
        if (_randomRotation)
            transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        torque = UnityEngine.Random.Range(-100, 101);
        fall = true;
        
        if (type == FoodType.IceCream && PlayerPrefs.GetInt("icecreamtutorial", -1) == -1)
        {
            transform.position = new Vector3(Screen.width / 2f, transform.position.y, transform.position.z);
            StartCoroutine(Tutorial.instance.ShowInstruction(gameObject, "Eat IceCream To Get Points"));
            PlayerPrefs.SetInt("icecreamtutorial", 1);
        }
        else if (type == FoodType.Chilli && PlayerPrefs.GetInt("chillitutorial", -1) == -1)
        {
            transform.position = new Vector3(Screen.width / 2f, transform.position.y, transform.position.z);
            StartCoroutine(Tutorial.instance.ShowInstruction(gameObject, "Avoid Chillies"));
            PlayerPrefs.SetInt("chillitutorial", 1);
        }
    }

    protected virtual void Update()
    {
        if (!fall)
        {
            return;
        }

        if (GamePlayManager.TimeScale == 0)
        {
            return;
        }

        var _position = transform.position;
        _position = Vector3.MoveTowards(_position, isInMouth ? CharacterVisual.instance.mouthMask.position : _position + Vector3.down * 10, speed * Time.deltaTime * Screen.height / 2000f);
        transform.position = _position;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + torque * Time.deltaTime);
    }

    protected virtual bool AllowAutomaticEating()
    {
        return true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Border"))
        {
            HandleCollisionWithBorder();
        }
    }

    protected virtual void HandleCollisionWithBorder()
    {
        OnReachedBorder?.Invoke(this);
        Destroy(gameObject);
    }

    protected IEnumerator Melt(IceCreamSO _iceCream)
    {
        if (Type==FoodType.IceCream)
        {
            OnMelted?.Invoke();
        }
        fall = false;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        Image _image = GetComponent<Image>();
        _image.sprite = _iceCream.SemiMelted;
        yield return new WaitForSeconds(1);
        _image.sprite = _iceCream.Melted;
        yield return new WaitForSeconds(1);

        Color _color = _image.color;
        float _duration = 5;
        Sequence _sequence = DOTween.Sequence();
        _sequence.Append(DOTween.To(() => _color.a, _x => _color.a = _x, 0, _duration).OnUpdate(() =>
        {
            if (_image != null)
            {
                _image.color = _color;
            }
        }));
        _sequence.OnComplete(() =>
        {
            if (_image!=null)
            {
                Destroy(gameObject);
            }
        });
        _sequence.Play();
    }
}
