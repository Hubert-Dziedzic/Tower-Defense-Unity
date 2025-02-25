using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{

    [Header("Events")]
    public static UnityEvent onEnemyKilled = new UnityEvent();
    public static UnityEvent onGameWon = new UnityEvent();

    [Header("Spawner References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] enemySecondPathPrefabs;
    [SerializeField] private Canvas victoryCanvas;

    [Header("Spawners Attribute")]
    [SerializeField] private int countBaseEnemies = 8;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float enemiesPerSec = 0.5f;
    [SerializeField] private float difficultySpawnerFactor = 0.8f;
    [SerializeField] private float epsCap = 11.3f;
    [SerializeField] private int wavesToWin = 3;

    private bool isActuallSpawning = false;
    private int actualWave = 1;
    private float timeSinceLastWave;
    private int enemiesCureentlyAlive;
    private int enemiesLeftToSpawn;
    private float eps;


    private void Awake()
    {
        onEnemyKilled.AddListener(EnemyKilled);
    }

    private void Start()
    {
        StartCoroutine(StartSpawningWave()); 
    }


    private void Update()
    {
        if (isActuallSpawning == false) return;

        timeSinceLastWave += Time.deltaTime;

        if (timeSinceLastWave >= (1f / eps) && enemiesLeftToSpawn >= 1)
        {
            SpawnEnemy();
            enemiesCureentlyAlive++;
            enemiesLeftToSpawn--;
            timeSinceLastWave = 0f;
        }

        if (enemiesCureentlyAlive == 0 && enemiesLeftToSpawn == 0)
        {
            StopSpawningWave();
        }
    }
    private void StopSpawningWave()
    {
        isActuallSpawning = false ;
        timeSinceLastWave = 0f;

        if (actualWave >= wavesToWin)
        {
            WinGame();
        }
        else
        {
            actualWave++;
            StartCoroutine(StartSpawningWave());
        }

    }

    private void EnemyKilled()
    {
        enemiesCureentlyAlive--;
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefabToSpawn = enemyPrefabs[index];

        // Random spawn point selection
        Transform spawnPoint = LevelManager.main.startingPoint;
        if (LevelManager.main.startingPoint2 != null && Random.value > 0.5f)
        {
            spawnPoint = LevelManager.main.startingPoint2;
            int secondPathIndex = Random.Range(0, enemySecondPathPrefabs.Length);
            enemyPrefabToSpawn = enemySecondPathPrefabs[secondPathIndex];
        }

        // Spawn or retrieve enemy from pool
        GameObject enemy = ObjectPoolManager.SpawnObject(enemyPrefabToSpawn, spawnPoint.position, Quaternion.identity);
    }

    private IEnumerator StartSpawningWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isActuallSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSec();
    }

    private int EnemiesPerWave() 
    {
        return Mathf.RoundToInt(countBaseEnemies * Mathf.Pow(actualWave, difficultySpawnerFactor));
    }

    private float EnemiesPerSec()
    {
        return Mathf.Clamp(enemiesPerSec * Mathf.Pow(actualWave, difficultySpawnerFactor), 0f, epsCap);
    }
    private void WinGame()
    {
        onGameWon.Invoke();
        victoryCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
