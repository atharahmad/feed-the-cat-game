using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private FoodController prefab;
    [SerializeField] private Transform foodHolder;

    [SerializeField] private float initialCooldown;
    [SerializeField] private float minCooldown;
    [SerializeField] private float maxCooldown;

    private float counter;
    private Vector2 coinSize;

    private void Start()
    {
        counter = initialCooldown;
        coinSize = prefab.GetComponent<Collider2D>().bounds.size * 2f;
    }

    private void Update()
    {
        if (GamePlayManager.TimeScale == 0)
            return;

        counter -= Time.deltaTime;

        if (counter <= 0)
        {
            counter = Random.Range(minCooldown, maxCooldown);
            FoodController _rewardingIceCream = Instantiate(prefab, foodHolder);

            Vector3 _spawnPosition = new Vector3();
            _spawnPosition.x = Random.Range(Screen.width / 12f, Screen.width - Screen.width / 12f);
            _spawnPosition.y = Screen.height - Screen.height / 12f;
            _rewardingIceCream.transform.position = _spawnPosition;

            _rewardingIceCream.Setup();
        }
    }

}
