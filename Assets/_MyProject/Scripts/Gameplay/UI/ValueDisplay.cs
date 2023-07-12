using UnityEngine;
using TMPro;
using System.Collections;

public class ValueDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountDisplay;

    public void Setup(int _amount)
    {
        amountDisplay.text = "+" + _amount;
        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
