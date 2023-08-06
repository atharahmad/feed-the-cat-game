using UnityEngine;

public class FoodRewardingIceCream : FoodController
{
    [SerializeField] private IceCreamSO iceCream;
    
    protected override bool AllowAutomaticEating()
    {
        return false;
    }

    protected override void HandleCollisionWithBorder()
    {
        OnReachedBorder?.Invoke(this);
        StartCoroutine(Melt(iceCream));
        Destroy(trail.gameObject);
    }
}
