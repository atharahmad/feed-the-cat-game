using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public static FoodSpawner Instance;

    [SerializeField] private FoodChillies chilliPrefab;
    [SerializeField] private FoodIceCream iceCreamPrefab;
    [SerializeField] private Transform foodHolder;
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;
    [SerializeField] private int spawnChilliAfterMin;
    [SerializeField] private int spawnChilliAfterMax;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float spawnCooldownReducer;
    [SerializeField] private int reduceSpawnCooldownAfter;
    [SerializeField] private float minSpawnCooldown;

    public Transform FoodHolder => foodHolder;

    private float spawnTimerCounter;
    private int spawnedCounter;
    private int spawnChilliAt;
    private int totalSpawnCounter;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //starting values
        spawnTimerCounter = 3;
        spawnedCounter = 0;
        spawnChilliAt = 3;
    }
    private void Update()
    {
        if (GamePlayManager.TimeScale == 0)
        {
            return;
        }

        spawnTimerCounter -= Time.deltaTime;
        if (spawnTimerCounter <= 0)
        {
            spawnTimerCounter = spawnCooldown;
            FoodController _foodPrefab;
            if (spawnedCounter == spawnChilliAt)
            {
                _foodPrefab = chilliPrefab;
                spawnChilliAt = Random.Range(spawnChilliAfterMin, spawnChilliAfterMax);
                spawnedCounter = 0;
            }
            else
            {
                _foodPrefab = iceCreamPrefab;
            }

            FoodController _foodController = Instantiate(_foodPrefab, foodHolder);

            Vector3 _spawnPosition = new Vector3();
            var _leftBoundaryPosition = leftBoundary.position;
            var _rightBoundaryPosition = rightBoundary.position;
            _spawnPosition.x = Random.Range(_leftBoundaryPosition.x, _rightBoundaryPosition.x);
            _spawnPosition.y = Random.Range(_leftBoundaryPosition.y, _rightBoundaryPosition.y);
            _spawnPosition.z = Random.Range(_leftBoundaryPosition.z, _rightBoundaryPosition.z);
            _foodController.transform.position = _spawnPosition;

            _foodController.Setup();
            spawnedCounter++;
            totalSpawnCounter++;
            if (totalSpawnCounter % reduceSpawnCooldownAfter == 0)
            {
                spawnCooldown -= spawnCooldownReducer;
                if (spawnCooldown < minSpawnCooldown)
                {
                    spawnCooldown = minSpawnCooldown;
                }
            }
        }
    }

}
