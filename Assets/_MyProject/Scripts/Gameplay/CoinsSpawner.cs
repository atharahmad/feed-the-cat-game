using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private FoodController prefab;
    [SerializeField] private Transform foodHolder;
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;

    [SerializeField] private float initialCooldown;
    [SerializeField] private float minCooldown;
    [SerializeField] private float maxCooldown;

    private float counter;

    private void Start()
    {
        counter = initialCooldown;
    }

    private void Update()
    {
        if (GamePlayManager.TimeScale == 0)
        {
            return;
        }

        counter -= Time.deltaTime;

        if (counter <= 0)
        {
            counter = Random.Range(minCooldown, maxCooldown);
            FoodController _rewardingIceCream = Instantiate(prefab, foodHolder);

            Vector3 _spawnPosition = new Vector3();
            var _leftBoundary = leftBoundary.position;
            var _rightBoundary = rightBoundary.position;
            _spawnPosition.x = Random.Range(_leftBoundary.x, _rightBoundary.x);
            _spawnPosition.y = Random.Range(_leftBoundary.y, _rightBoundary.y);
            _spawnPosition.z = Random.Range(_leftBoundary.z, _rightBoundary.z);
            _rewardingIceCream.transform.position = _spawnPosition;

            _rewardingIceCream.Setup();
        }
    }

}
