using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemySpawnAlgorithm : Singleton<EnemySpawnAlgorithm>
{
    private LevelData data;
    private Random random;

    int _waveIndex = 0;
    bool _passedIntro = false;
    bool _doneSpawningWave = false;

    public void Start()
    {
        data = GameManager.CurrentLevel;
        random = new Random(data.levelNumber);
        StartCoroutine(C_WaveLoop());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator C_WaveLoop()
    {
        int[] waveSeries = data.wavesIntro;
        while(true)
        {
            float timeElapsed = 0f;
            while (timeElapsed < data.timeBetweenWaves)
            {
                if (!GameManager.gamePaused)
                {
                    timeElapsed += Time.deltaTime;
                }
                yield return null;
            }
            // Enough time has passed for a new wave!
            if(!_passedIntro && _waveIndex >= data.wavesIntro.Length)
            {
                _passedIntro = true;
                _waveIndex = 0;
                waveSeries = data.wavesLoop;
            }
            _doneSpawningWave = false;
            StartCoroutine(C_SpawnWave(waveSeries[_waveIndex % waveSeries.Length]));
            yield return new WaitUntil(() => _doneSpawningWave);
            _waveIndex++;
        }
    }
    private IEnumerator C_SpawnWave(int difficulty)
    {
        int remainingPoints = difficulty;
        while(remainingPoints > 0)
        {
            List<EnemyWeight> enemySpawnList = new();
            int weightSum = 0;
            foreach(EnemyWeight ew in data.enemyWeights)
            {
                if (ew.enemy.cost > remainingPoints) continue;  // Do not add the enemy if it's too expensive
                if (ew.spawnType == EnemySpawnType.ONLY_DURING_INTRO && _passedIntro) continue;
                if (ew.spawnType == EnemySpawnType.ONLY_AFTER_INTRO && !_passedIntro) continue;
                if (ew.spawnType == EnemySpawnType.ONLY_AFTER_FIRST_LOOP && !_passedIntro && _waveIndex >= data.wavesLoop.Length) continue;
                
                enemySpawnList.Add(ew);
                weightSum += ew.weight;
            }

            if(weightSum == 0)
                break;      // Prematurely break, because this means there aren't enemies cheap enough anymore to include

            int index = random.Next(weightSum);
            int i = 0;
            EnemyData selectedEnemy = null;
            while(selectedEnemy == null)
            {
                EnemyWeight ew = enemySpawnList[i];
                if(index < ew.weight)
                {
                    selectedEnemy = ew.enemy;
                }
                else
                {
                    index -= ew.weight;
                }
                i++;
            }
            LevelManager.Instance.SpawnEnemy(selectedEnemy);
            remainingPoints -= selectedEnemy.cost;
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.20f, 0.50f));
        }
        _doneSpawningWave = true;
    }
}
