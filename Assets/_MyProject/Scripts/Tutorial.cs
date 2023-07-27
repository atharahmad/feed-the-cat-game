using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Tutorial : MonoBehaviour
{
    public List<RectTransform> hintsObjects;
    public RectTransform hintPanel;
    public static Tutorial instance;
    public TextMeshProUGUI hint;

    void Start()
    {
        instance = this;
        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, -1) == -1)
        {
            hintPanel.position = hintsObjects[0].position;
            hintPanel.gameObject.SetActive(true);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
            Time.timeScale = 0;
        }
    }
    public IEnumerator ShowInstruction(GameObject obj,string msg)
    {
        yield return new WaitUntil(() => obj.transform.position.y < Screen.height / 1.5f);
        CharacterMover.Instance.OnPointerUp(null);
        PlayerPrefs.SetInt(msg, 1);
        hintPanel.position = obj.transform.position;
        hint.text = msg;
        hintPanel.gameObject.SetActive(true);
        Time.timeScale = 0;

    }
    public void Toggle(bool _val)
    {
        hintPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
