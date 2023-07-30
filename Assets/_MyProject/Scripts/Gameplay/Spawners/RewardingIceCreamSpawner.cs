using UnityEngine;

public class RewardingIceCreamSpawner : MonoBehaviour
{
    public static RewardingIceCreamSpawner Instance;

    [SerializeField] private FoodController prefab;
    [SerializeField] private Transform foodHolder;

    [SerializeField] private float initialCooldown;
    [SerializeField] private float minCooldown;
    [SerializeField] private float maxCooldown;

    private float counter;

    private void Awake() => Instance = this;

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
        GamePlayUI.Instance.SetTimer((int)counter);
        if (counter <= 0)
        {
            counter = Random.Range(minCooldown, maxCooldown);
            GamePlayUI.Instance.SetTimer((int)counter);
            FoodController _rewardingIceCream = Instantiate(prefab, foodHolder);

            Vector3 _spawnPosition = new Vector3();
            _spawnPosition.x = Random.Range(Screen.width / 12f, Screen.width - Screen.width/ 12f);
            _spawnPosition.y = Screen.height - Screen.height / 12f;
            _rewardingIceCream.transform.position = _spawnPosition;

            _rewardingIceCream.Setup(false);
        }
    }
}
