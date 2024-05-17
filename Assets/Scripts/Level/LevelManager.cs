using System.Numerics;
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
    private LevelData data;

    // Events
    public delegate void MoneyEvent(int amount);
    public static event MoneyEvent OnMoneyChanged;
    public delegate void PointsEvent(int amount);
    public static event PointsEvent OnPointsCalculated;

    private void Awake()
    {
        lanePosition = new float[] { botLanePos, midLanePos, topLanePos };
    }

    private void Start()
    {
        data = GameManager.currentLevel;
        spawnAlgorithm = GetComponent<EnemySpawnAlgorithm>();

        LevelUI.Instance.GenerateUnitButtons(unitData);
    }
    private void OnEnable()
    {
        Enemy.OnEnemyKilled += OnEnemyKilled;
        Player.OnPlayerKilled += OnPlayerKilled;
        Obstacle.OnEnemyBaseKilled += OnEnemyBaseKilled;
        Obstacle.OnPlayerBaseKilled += OnPlayerBaseKilled;
    }
    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= OnEnemyKilled;
        Player.OnPlayerKilled -= OnPlayerKilled;
        Obstacle.OnEnemyBaseKilled -= OnEnemyBaseKilled;
        Obstacle.OnPlayerBaseKilled -= OnPlayerBaseKilled;
    }
    private void OnEnemyKilled(Enemy enemy)
    {
        AddMoney(enemy.moneyOnKill);
    }
    private void OnPlayerKilled(Player _)
    {
        FailLevel();
    }
    private void OnPlayerBaseKilled(Obstacle _)
    {
        FailLevel();
    }
    private void OnEnemyBaseKilled(Obstacle _)
    {
        CompleteLevel();
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
            GameManager.IncrementLevel();
        }
        int points = CalculateLevelPoints(true, true);
        OnPointsCalculated?.Invoke(points);     // Anything that relies wants to know the amount of points calculated can use this event
        GameManager.AddPoints(points);
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
        int points = CalculateLevelPoints(true, true);
        OnPointsCalculated?.Invoke(points);     // Anything that relies wants to know the amount of points calculated can use this event
        GameManager.AddPoints(points);
        GameManager.Save();
    }

    private int CalculateLevelPoints(bool levelCompleted, bool doRandom = true)
    {

        // CALCULATE WAVE POINTS //
        float introDifficulty = 0f;
        float loopDifficulty = 0f;
        foreach (int wave in data.wavesIntro)
        {
            introDifficulty += wave;    // Get total amount of points in the intro
        }
        introDifficulty /= data.wavesIntro.Length; // Divide this by the number of waves
        foreach (int wave in data.wavesLoop)
        {
            loopDifficulty += wave;     // Get total amount of points in the loop
        }
        loopDifficulty /= data.wavesLoop.Length;   // Divide this by the number of waves
                                                                   // Multiply all wave points, then divide by the time between each wave
        float wavePoints = introDifficulty * loopDifficulty / data.timeBetweenWaves * 100f;

        // CALCULATE ENEMY POINTS //
        float enemyPoints = 0f;
        float totalWeight = 0;
        foreach(EnemyWeight e in data.enemyWeights)
        {
            totalWeight += e.weight;    // Get the total amount of weight in the summation
        }
        foreach(EnemyWeight e in data.enemyWeights)
        {
            float f = Mathf.Pow(e.enemy.cost, 2) * e.weight / totalWeight;  // Add cost^2 * weight / total weight

            enemyPoints += f * GetSpawnModifier(e.spawnType); // Add this to the sum, multiplying by spawnMod
        }
        enemyPoints *= 50f;

        // CALCULATE LEVEL MODIFIER //
        float levelMod = 1 + data.levelNumber / 10f;

        // CALCULATE COMPLETION MODIFIER //
        float completedMod = levelCompleted ? 1f : 0.01f;

        // CALCULATE RANDOM MODIFIER //
        float randomMod = doRandom ? Random.Range(-0.10f, 0.10f) : 0f;

        // RESULT //
        float result = (wavePoints + enemyPoints) * levelMod * completedMod;
        result += result * randomMod;           // Apply += 10%
        return Mathf.RoundToInt(result);
    }
    private float GetSpawnModifier(EnemySpawnType est)
    {
        return est switch
        {
            EnemySpawnType.ALWAYS => 2.0f,
            EnemySpawnType.ONLY_AFTER_INTRO => 1.5f,
            EnemySpawnType.ONLY_DURING_INTRO => 1.0f,
            EnemySpawnType.ONLY_AFTER_FIRST_LOOP => 0.75f,
            _ => throw new System.NotImplementedException(),
        };
    }
}
