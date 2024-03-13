using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemySpawner : MonoBehaviour
{
    //private GameObject currentEnemy;      wordt nu niet gebruikt
    //private float spawnRateUp = 8;    wordt nu niet gebruikt
    [SerializeField] private float spawnRate = 2;
    [SerializeField] private float spawnTimer = 0;      //zodra deze "0" is dan wordt er iets gespawned
    //private float spawnTimerUp = 0;
    [SerializeField] private float leftOrRightSpawnOffset = 4.5f;
    [SerializeField] private GameObject[] enemyTypeArray;


    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < spawnRate)
        {
            spawnTimer += Time.deltaTime;        // zodra de "spawnTimer" een hoger getal is dan de "spawnRate" dan wordt er iets gespawned
        }
        else
        {
            SpawnEnemy();
            spawnTimer = 0;
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

    void SpawnEnemy()
    {
        float maxLeftPoint = transform.position.x - leftOrRightSpawnOffset;
        float maxRightPoint = transform.position.x + leftOrRightSpawnOffset;

        Instantiate(enemyTypeArray[Random.Range(0, enemyTypeArray.Length)], new Vector3(Random.Range(maxLeftPoint, maxRightPoint), 0, transform.position.z), transform.rotation);
    }
}