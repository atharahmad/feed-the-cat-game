using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PowerWordsDisplay : MonoBehaviour
{
    //[SerializeField] private List<SpriteArray> sprites;
    [SerializeField] private TextMeshProUGUI imageDisplay;
    [SerializeField] private int showSweetAt;
    [SerializeField] private int showDeliciousAt;
    [SerializeField] private int showYummyAt;
    [SerializeField] private int showPerfectAt;
    [SerializeField] private int showAmazingAt;

    //private Sprite[] selectedSprites;
    private int strike;

    public Color[] gradiantsColors;
    public Color[] gradiantsColors2;
    private void Awake()
    {
        //selectedSprites = sprites[Random.Range(0, sprites.Count)].sprites;
        //selectedSprites = sprites[2].Sprites;
    }

    private void OnEnable()
    {
        FoodController.OnMelted += ResetStrike;
        CharacterController.OnEatenIceCream += IncreaseStrike;
    }

    private void OnDisable()
    {
        FoodController.OnMelted -= ResetStrike;
        CharacterController.OnEatenIceCream -= IncreaseStrike;
    }

    private void ResetStrike()
    {
        strike = 0;
    }

    private void IncreaseStrike()
    {
        strike++;
        ShowWord();
    }

    private void ShowWord()
    {
        string word = null;

        if (strike==showSweetAt)
        {
            word = "Sweet";
        }
        else if(strike==showDeliciousAt)
        {
            word = "Delicious";
        }
        else if (strike==showYummyAt)
        {
            word = "Yummy";
        }
        else if (strike==showPerfectAt)
        {
            word = "Perfect";
        }
        else if (strike == showAmazingAt)
        {
            word = "Amazing";
        }

        if (word != null)
        {
            StartCoroutine(ShowWord(strike, word));
        }
    }


    private IEnumerator ShowWord(int strike, string _word)
    {
        VertexGradient gradiant = new VertexGradient(gradiantsColors[strike], gradiantsColors[strike], gradiantsColors2[strike], gradiantsColors2[strike]);
        imageDisplay.colorGradient = gradiant;
        imageDisplay.text = _word;
        imageDisplay.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageDisplay.gameObject.SetActive(false);

    }
}
