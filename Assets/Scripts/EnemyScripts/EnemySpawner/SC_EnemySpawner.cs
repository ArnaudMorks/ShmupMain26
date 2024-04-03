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

    //[SerializeField] private float highSpawnRate = 0.5f;        //zit in de Spawn Manager

    [SerializeField] private float currentSpawnRateBase;
    public float CurrentSpawnRate       //set de Base versie
    {
        get { return currentSpawnRateBase; }
        set { currentSpawnRateBase = value; }
    }

    private bool swarmRateSet = false;
    [SerializeField] private float normalCurrentSpawnRate;      //alleen gebuirken bij swarm stuk als NIET swarm enemies worden gespawned
    [SerializeField] private float currentSpawnRate;        //hier wordt offset bij gebruikt
    [SerializeField] private float spawnRateOffset;

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

    [SerializeField] private bool swarmSpawnMode;
    [SerializeField] private int swarmSpawnChance = 1;

    [SerializeField] private GameObject kamikazeCircle;
    [SerializeField] private GameObject kamikazeSwarm;
    [SerializeField] private GameObject shooterSwarm;


    void Start()
    {
       // SpawnEnemy();
        enemyShooterPool = FindObjectOfType<SC_EnemyShooterPool>();
        enemyKamikazePool = FindObjectOfType<SC_EnemyKamikazePool>();
        enemySlugPool = FindObjectOfType<SC_EnemySlugPool>();

        currentSpawnRateBase = beginSlugSpawnRate;
        currentSpawnRate = currentSpawnRateBase;
        currentLeftOrRightSpawnOffset = firstLevelLeftOrRightSpawnOffset;       //later een "if" erbij als je in een ander level spawned
    }

    void Update()
    {

        if (swarmSpawnChance == 0 && swarmRateSet == false)
        {
            normalCurrentSpawnRate = currentSpawnRate;
            currentSpawnRate *= 3;
            swarmRateSet = true;
        }


        if (spawnTimer < currentSpawnRate)
        {
            spawnTimer += Time.deltaTime;        // zodra de "spawnTimer" een hoger getal is dan de "spawnRate" dan wordt er iets gespawned
        }
        else if (randomSpawnerOn)
        {
            CurrentSpawnLevel();

            if (swarmSpawnChance != 0)
            {
                SpawnEnemy();
            }
            else
            {
                SpawnSwarm();
                currentSpawnRate = normalCurrentSpawnRate;  //zet de normale spawn rate weer terug
            }

            if (swarmSpawnMode) 
            {
                swarmSpawnChance = Random.Range(0, 8);
                swarmRateSet = false;
            }

            SetSpawnRate();
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
            case 5:
                currentEnemy = 0;
                break;
            case 6:
                currentEnemy = 1;
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
                enemyShooterPool.ActivateShooterEnemy(new Vector3(Random.Range(maxLeftPoint, maxRightPoint), 0, transform.position.z), false, currentPowerup, 0);
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


    private void SpawnSwarm()
    {
        float maxLeftPoint = transform.position.x - firstLevelLeftOrRightSpawnOffset;       //gebruikt van eerste level want goed voor de swarm
        float maxRightPoint = transform.position.x + firstLevelLeftOrRightSpawnOffset;

        switch (currentEnemy)
        {
            case 0:
                Instantiate(kamikazeCircle, new Vector3(Random.Range(maxLeftPoint, maxRightPoint), 0, transform.position.z), transform.rotation);
                break;
            case 1:
                Instantiate(kamikazeSwarm, new Vector3(Random.Range(maxLeftPoint, maxRightPoint), 0, transform.position.z), transform.rotation);
                break;
            case 2:
                Instantiate(shooterSwarm, new Vector3(Random.Range(maxLeftPoint, maxRightPoint), 0, transform.position.z), transform.rotation);
                break;
            default:
                Debug.Log("Enemy spawner werkt niet");
                break;
        }
    }


    private void SetSpawnRate()
    {
        spawnRateOffset = currentSpawnRateBase * 0.20f;  //de offset is nu 20% van de normale spawn rate

        float minOffset = currentSpawnRateBase - spawnRateOffset;
        float maxOffset = currentSpawnRateBase + spawnRateOffset;

        currentSpawnRate = Random.Range(minOffset, maxOffset);

    }



    public void SpawnSpecificLocation(int currentSpecificEnemy, Vector3 thisPosition, Quaternion thisRotation, bool singleSetSpeed, float customSpeed, float slugFroggerMode)       //wordt gebruikt door de "SC_SingleTimeSpawner"
    {
        switch (currentSpecificEnemy)
        {
            case 0:
                enemyShooterPool.ActivateShooterEnemy(thisPosition, singleSetSpeed, currentPowerup, customSpeed);
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


    public void SetSpawnWidthAreaSecondLevel()
    {
        currentLeftOrRightSpawnOffset = secondLevelLeftOrRightSpawnOffset;
    }

    public void SwarmSpawnChance()
    {
        swarmSpawnMode = true;
    }

}