using UnityEngine;

public class SC_SingleTimeSpawner : MonoBehaviour           //EMPTY GAME OBJECT WAAR SPAWNERS IN ZITTEN MOETEN OP 0 STAAN IN DE WORLD LOCATION (dus omgereede van de min value van het top world parent object)
{
    //[SerializeField] float mapMovementSpeed;    //beweegt net zo snel als de top map  BEWEEGT NU IN DE MAP ZELF
    [SerializeField] float enemySpawnLocation = 18.16f;     //zodat de enemy boven aan het scherm spawned
    private bool startBeforeDisable = false;

    [Header("SpawnSettings")]
    [SerializeField] private int currentEnemy;  // 0 = shooter; 1 = kamikaze; 2 = slug
    [SerializeField] private float spawnDelay = 0;

    private SC_EnemySpawner enemySpawner;


    private void Start()
    {
        enemySpawner = FindObjectOfType<SC_EnemySpawner>();
        startBeforeDisable = true;
    }

    private void OnDisable()
    {
        if (startBeforeDisable)
        {
            enemySpawner.SpawnSpecificLocation(currentEnemy, transform.position);
            //Destroy(this, 0.2f);    //vernietigt zichzelf bijna gelijk nadat een enemy is gespawned
        }

    }

    private void FixedUpdate()
    {
        if (transform.position.z <= enemySpawnLocation && isActiveAndEnabled)
        {
            if (spawnDelay <= 0)
            {
                //Debug.Log("SetActive(false) om enemy te spawnen");
                gameObject.SetActive(false);
            }
            else
            {
                spawnDelay -= Time.fixedDeltaTime;
            }
        }

    }


}
