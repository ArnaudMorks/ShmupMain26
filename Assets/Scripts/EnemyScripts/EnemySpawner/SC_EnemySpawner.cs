using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemySpawner : MonoBehaviour
{
    [SerializeField] private bool randomSpawnerOn;
    public bool RandomSpawnerOn
    {
        get { return randomSpawnerOn; }
        set { randomSpawnerOn = value; }
    }

    //private GameObject currentEnemy;      wordt nu niet gebruikt
    //private float spawnRateUp = 8;    wordt nu niet gebruikt
    [Header("SpawnRates")]
    [SerializeField] private float beginSlugSpawnRate;

    [SerializeField] private float highSpawnRate = 0.5f;

    [SerializeField] private float currentSpawnRate;
    public float CurrentSpawnRate
    {
        get { return currentSpawnRate; }
        set { currentSpawnRate = value; }
    }

    [SerializeField] private float spawnTimer = 0;      //zodra deze "0" is dan wordt er iets gespawned

    //private float spawnTimerUp = 0;
    [Header("MapOffsets")]
    [SerializeField] private float firstLevelLeftOrRightSpawnOffset = 16.8f;
    [SerializeField] private float secondLevelLeftOrRightSpawnOffset = 21;
    [SerializeField] private float currentLeftOrRightSpawnOffset;

    //[SerializeField] private GameObject[] enemyTypeArray;     OUDE MANIER

    [SerializeField] private SC_EnemyShooterPool enemyShooterPool = null;
    [SerializeField] private SC_EnemyKamikazePool enemyKamikazePool = null;
    [SerializeField] private SC_EnemySlugPool enemySlugPool = null;
    [SerializeField] private int currentEnemy;
    [SerializeField] private int enemySpecificRandomizer;

    [SerializeField] private int spawnerLevel;

    [Header("Powerups")]
    [SerializeField] private GameObject currentPowerup = null;
    public GameObject CurrentPowerup        //wordt in spawnManager gezet
    {
        get { return currentPowerup; }
        set { currentPowerup = value; }
    }

    public int SpawnerLevel
    {
        get { return spawnerLevel; }
        set { spawnerLevel = value; }
    }


    void Start()
    {
       // SpawnEnemy();
        enemyShooterPool = FindObjectOfType<SC_EnemyShooterPool>();
        enemyKamikazePool = FindObjectOfType<SC_EnemyKamikazePool>();
        enemySlugPool = FindObjectOfType<SC_EnemySlugPool>();

        currentSpawnRate = beginSlugSpawnRate;
        currentLeftOrRightSpawnOffset = firstLevelLeftOrRightSpawnOffset;       //later een "if" erbij als je in een ander level spawned
    }

    void Update()
    {
        if (spawnTimer < currentSpawnRate)
        {
            spawnTimer += Time.deltaTime;        // zodra de "spawnTimer" een hoger getal is dan de "spawnRate" dan wordt er iets gespawned
        }
        else if (randomSpawnerOn)
        {
            CurrentSpawnLevel();
            SpawnEnemy();
            spawnTimer = 0;

            //maak spawn rate random
        }

/*        if (spawnTimerUp < spawnRateUp)
        {
            spawnTimerUp += Time.deltaTime;
        }
        else
        {
            spawnRate *= 0.8f;
            spawnTimerUp = 0;
        }

        if (spawnRate <= 0.33) { spawnRate = 2; }   //reset de spawn rate, en zorgt ervoor dat enemy's sneller spawnen*/

    }


    private void CurrentSpawnLevel()
    {

        switch (spawnerLevel)
        {
            case 0:
                currentEnemy = 2;
                break;
            case 1:
                currentEnemy = Random.Range(1, 3);
                break;
            case 2:
                enemySpecificRandomizer = Random.Range(0, 2);

                if (enemySpecificRandomizer == 0) { currentEnemy = 0; }
                else { currentEnemy = 2; }

                break;
            case 3:
                currentEnemy = Random.Range(0, 2);
                break;
            case 4:
                currentEnemy = Random.Range(0, 3);      //alle enemies
                break;
            default:
                Debug.Log("Enemy spawner werkt niet");
                break;
        }


    }


    private void SpawnEnemy()
    {
        float maxLeftPoint = transform.position.x - currentLeftOrRightSpawnOffset;
        float maxRightPoint = transform.position.x + currentLeftOrRightSpawnOffset;

        switch (currentEnemy)
        {
            case 0:
                enemyShooterPool.ActivateShooterEnemy(new Vector3(Random.Range(maxLeftPoint, maxRightPoint), 0, transform.position.z));
                break;
            case 1:
                enemyKamikazePool.ActivateKamikazeEnemy(new Vector3(Random.Range(maxLeftPoint, maxRightPoint), 0, transform.position.z));
                break;
            case 2:
                enemySlugPool.ActivateSlugEnemy(new Vector3(Random.Range(maxLeftPoint, maxRightPoint), 0, transform.position.z), transform.rotation, false, currentPowerup, 0, 0);
                break;
            default:
                Debug.Log("Enemy spawner werkt niet");
                break;
        }

        //enemyShooterPool.ActivateShooterEnemy(new Vector3(Random.Range(maxLeftPoint, maxRightPoint), 0, transform.position.z));
        //enemyKamikazePool.ActivateKamikazeEnemy(new Vector3(Random.Range(maxLeftPoint, maxRightPoint), 0, transform.position.z));           //Voor testen individueel
        //enemySlugPool.ActivateSlugEnemy(new Vector3(Random.Range(maxLeftPoint, maxRightPoint), 0, transform.position.z));

        //Instantiate(enemyTypeArray[Random.Range(0, enemyTypeArray.Length)], new Vector3(Random.Range(maxLeftPoint, maxRightPoint), 0, transform.position.z), transform.rotation);
    }


    public void SpawnSpecificLocation(int currentSpecificEnemy, Vector3 thisPosition, Quaternion thisRotation, bool singleSetSpeed, float customSpeed, float slugFroggerMode)       //wordt gebruikt door de "SC_SingleTimeSpawner"
    {
        switch (currentSpecificEnemy)
        {
            case 0:
                enemyShooterPool.ActivateShooterEnemy(thisPosition);
                break;
            case 1:
                enemyKamikazePool.ActivateKamikazeEnemy(thisPosition);
                break;
            case 2:
                enemySlugPool.ActivateSlugEnemy(thisPosition, thisRotation, true, currentPowerup, customSpeed, slugFroggerMode);   //als "customSpeed" 0 is dan heeft die de orginele snelheid
                break;
            default:
                Debug.Log("Enemy spawner werkt niet");
                break;
        }
    }

}