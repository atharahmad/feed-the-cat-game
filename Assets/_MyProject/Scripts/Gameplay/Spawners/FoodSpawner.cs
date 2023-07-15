using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public static FoodSpawner Instance;

    [SerializeField] private FoodChillies chilliPrefab;
    [SerializeField] private FoodIceCream iceCreamPrefab;
    [SerializeField] private Transform foodHolder;
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
    private Vector2 foodSize;

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
        foodSize = chilliPrefab.GetComponent<Collider2D>().bounds.size * 2f;
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
            _spawnPosition.x = Random.Range(Screen.width / 12f, Screen.width - Screen.width / 12f);
            _spawnPosition.y = Screen.height - Screen.height / 12f;
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
