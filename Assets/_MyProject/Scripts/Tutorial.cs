using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Tutorial : MonoBehaviour
{
    public List<RectTransform> hintsObjects;
    public RectTransform hintPanel;
    public TextMeshProUGUI hint;
    public Transform handGesture;
    public static Tutorial Instance;
    public float swipeSpeed;
    private float scalingDir = .1f;
    private void Awake() => Instance = this;

    void Start()
    {
        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, -1) == -1)
        {
            hintPanel.position = hintsObjects[0].position;
            hintPanel.gameObject.SetActive(true);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
            if (handGesture != null)
                StartCoroutine(SwipeAnimation());
        }
        if (FoodSpawner.Instance && PlayerPrefs.GetInt("icecreamtutorial", -1) == -1)
            Toggle(true);
    }
    private void Update()
    {
        if (hintPanel.transform.localScale.x > 1.02f)
            scalingDir = -.1f;
        if (hintPanel.transform.localScale.x < 0.98f)
            scalingDir = .1f;
        hintPanel.transform.localScale += Vector3.one * Time.deltaTime * scalingDir;
    }
    IEnumerator SwipeAnimation()
    {
        handGesture.gameObject.SetActive(true);
        Vector2 left = (Vector2)handGesture.position + Vector2.left * 100;
        Vector2 right = (Vector2)handGesture.position + Vector2.right * 100;
        bool direction = false;
        while (hintPanel.gameObject.activeInHierarchy)
        {
            if (direction)
            {
                handGesture.position += (Vector3)Vector2.right * Time.deltaTime * swipeSpeed;
                if (handGesture.position.x > right.x)
                {
                    direction = false;
                }
            }
            else
            {
                handGesture.position += (Vector3)Vector2.left * Time.deltaTime * swipeSpeed;
                if (handGesture.position.x < left.x)
                {
                    direction = true;
                }
            }
            yield return new WaitForEndOfFrame();
        }
        handGesture.gameObject.SetActive(false);
    }
    public IEnumerator ShowInstruction(GameObject obj, string msg)
    {
        yield return new WaitUntil(() => obj.transform.position.y < Screen.height / 1.5f);
        CharacterMover.Instance.OnPointerUp(null);
        PlayerPrefs.SetInt(msg, 1);
        hintPanel.position = obj.transform.position;
        GamePlayManager.Instance.Pause();
        Toggle(true);
        hint.text = msg;
    }
    public void Toggle(bool _val)
    {
        hintPanel.gameObject.SetActive(_val);
        hint.gameObject.SetActive(_val);
        for (int i = 0; i < GamePlayManager.Instance.foodHolder.childCount; i++)
            if (GamePlayManager.Instance.foodHolder.GetChild(i).GetComponent<FoodController>())
                GamePlayManager.Instance.foodHolder.GetChild(i).GetComponent<FoodController>().enabled = !_val;
        if (_val)
            GamePlayManager.Instance.Pause();
        else
            GamePlayManager.Instance.Play();
    }
}
