using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public int money {  get; private set; }
    public float[] lanePosition { get; private set; }

    [Header("Level Data")]
    [SerializeField] private float botLanePos;
    [SerializeField] private float midLanePos;
    [SerializeField] private float topLanePos;

    [SerializeField] private float unitSpawnPos;
    [SerializeField] private float enemySpawnPos;

    [Header("Unit Data")]
    [SerializeField] private UnitData[] unitData;

    [Header("Player")]
    [SerializeField] private Player player;

    [Header("UI")]
    [SerializeField] private ScreenToggle overlayScreens;
    
    private EnemySpawnAlgorithm spawnAlgorithm;

    // Events
    public delegate void MoneyEvent(int amount);
    public static event MoneyEvent OnMoneyChanged;

    private void Awake()
    {
        lanePosition = new float[] { botLanePos, midLanePos, topLanePos };
    }

    private void Start()
    {
        LevelUI.Instance.GenerateUnitButtons(unitData);

        spawnAlgorithm = GetComponent<EnemySpawnAlgorithm>();
        spawnAlgorithm.InitializeAndStart(GameManager.currentLevel);
    }
    private void OnEnable()
    {
        Enemy.OnEnemyKilled += OnEnemyKilled;
        Player.OnPlayerKilled += OnPlayerKilled;
    }
    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= OnEnemyKilled;
        Player.OnPlayerKilled -= OnPlayerKilled;
    }
    private void OnEnemyKilled(Enemy enemy)
    {
        AddMoney(enemy.moneyOnKill);
    }
    private void OnPlayerKilled(Player player)
    {
        FailLevel();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        OnMoneyChanged?.Invoke(money);
    }

    public void SpawnUnit(UnitData unitData)
    {
        AddMoney(-unitData.cost);
        GameObject g = Instantiate(unitData.spawnEntity);
        Unit u = g.GetComponent<Unit>();
        u.transform.position = new(unitSpawnPos, 0);
        u.SetLane(player.lane);
    }
    public void SpawnEnemy(EnemyData enemyData)
    {
        GameObject g = Instantiate(enemyData.spawnEntity);
        Enemy e = g.GetComponent<Enemy>();
        e.transform.position = new(enemySpawnPos, 0);
        e.SetLane((Lane)Random.Range(0, 3));
    }

    public void CompleteLevel()
    {
        GameManager.Pause();
        spawnAlgorithm.Stop();
        LaneEntity[] entities = FindObjectsOfType<LaneEntity>();
        foreach (LaneEntity entity in entities)
        {
            entity.Stop();
        }
        overlayScreens.SetScreen(0);
        // UPDATE GAMEMANAGER
        if(GameManager.saveInfo.level == GameManager.currentLevel.levelNumber)
        {
            // If we just beat the current level
            GameManager.saveInfo.level += 1;    // Then increment the current level
        }
        GameManager.Save(); // Save the file
    }

    public void FailLevel()
    {
        GameManager.Pause();
        spawnAlgorithm.Stop();
        LaneEntity[] entities = FindObjectsOfType<LaneEntity>();
        foreach (LaneEntity entity in entities)
        {
            entity.Stop();
        }
        overlayScreens.SetScreen(1);
    }
}
