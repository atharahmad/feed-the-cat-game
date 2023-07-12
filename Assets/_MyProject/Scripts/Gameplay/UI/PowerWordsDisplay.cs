using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerWordsDisplay : MonoBehaviour
{
    [SerializeField] private List<SpriteArray> sprites;
    [SerializeField] private Image imageDisplay;
    [SerializeField] private int showSweetAt;
    [SerializeField] private int showDeliciousAt;
    [SerializeField] private int showYummyAt;
    [SerializeField] private int showPerfectAt;
    [SerializeField] private int showAmazingAt;

    private Sprite[] selectedSprites;
    private int strike;

    private void Awake()
    {
        //selectedSprites = sprites[Random.Range(0, sprites.Count)].sprites;
        selectedSprites = sprites[2].Sprites;
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
        Sprite _sprite = null;

        if (strike==showSweetAt)
        {
            _sprite = selectedSprites[0];
        }
        else if(strike==showDeliciousAt)
        {
            _sprite = selectedSprites[1];
        }
        else if (strike==showYummyAt)
        {
            _sprite = selectedSprites[2];
        }
        else if (strike==showPerfectAt)
        {
            _sprite = selectedSprites[3];
        }
        else if (strike == showAmazingAt)
        {
            _sprite = selectedSprites[4];
        }

        if (_sprite != null)
        {
            StartCoroutine(ShowWord(_sprite));
        }
    }


    private IEnumerator ShowWord(Sprite _sprite)
    {
        imageDisplay.sprite = _sprite;
        imageDisplay.SetNativeSize();
        imageDisplay.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageDisplay.gameObject.SetActive(false);

    }
}
