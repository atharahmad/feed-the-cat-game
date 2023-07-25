using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterVisual characterVisual;
    public static Action OnEatenIceCream;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        FoodController _foodController = _collision.gameObject.GetComponent<FoodController>();
        if (_foodController == null)
        {
            return;
        }
        _foodController.isInMouth = true;
        switch (_foodController.Type)
        {
            case FoodType.IceCream:
                var _amountToAdd = _foodController.Score * GamePlayManager.Instance.Multiplier;
                GamePlayManager.Instance.Score += _amountToAdd;
                (_foodController as FoodIceCream)?.SpawnCoinText(_amountToAdd);
                Routine.WaitAndCall(0.35f, () => { 
                    characterVisual.EatIceCream();
                    AudioManager.Instance.Play(AudioManager.ICE_CREAM_COLLECT);
                });
                Routine.WaitAndCall(0.55f, () => characterVisual.ThrowStick());
                break;
            case FoodType.Chilli:
                Routine.WaitAndCall(0.35f, () => {
                    characterVisual.EatChilly();
                    AudioManager.Instance.Play(AudioManager.ANGRY_CAT);
                });
                if (!ElixirHandler.IsActive)
                    GamePlayManager.Instance.TakeDamage(1);
                break;
            case FoodType.RewardingIceCream:
                Routine.WaitAndCall(0.35f, () => {
                    characterVisual.EatIceCream();
                    AudioManager.Instance.Play(AudioManager.ICE_CREAM_COLLECT);
                });
                Routine.WaitAndCall(0.55f, () => { characterVisual.ThrowStick(); AdHandler.Instance.Setup(); });
                break;
            case FoodType.Coin:
                int _amountOfCoins = _foodController.Score * GamePlayManager.Instance.Multiplier;
                DataManager.Instance.PlayerData.Coins += _amountOfCoins;
                Routine.WaitAndCall(0.35f, () => {
                    characterVisual.EatIceCream();
                    AudioManager.Instance.Play(AudioManager.ICE_CREAM_COLLECT);
                });
                break;
            default:
                throw new Exception("Don`t know how to eat: " + _foodController.Type);
        }

        if (_foodController.Type != FoodType.Chilli)
        {
            OnEatenIceCream?.Invoke();
        }
        _collision.transform.SetParent(characterVisual.mouthMask);
        _foodController.GetComponent<Collider2D>().enabled = false;
        Routine.Scale(_collision.gameObject.transform, Vector3.one, Vector3.one * .5f, 0.2f, () => Destroy(_collision.gameObject));
    }
}
