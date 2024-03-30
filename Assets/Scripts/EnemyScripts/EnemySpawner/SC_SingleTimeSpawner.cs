using UnityEngine;

public class SC_SingleTimeSpawner : MonoBehaviour           //EMPTY GAME OBJECT WAAR SPAWNERS IN ZITTEN MOETEN OP 0 STAAN IN DE WORLD LOCATION (dus omgereede van de min value van het top world parent object)
{
    //[SerializeField] float mapMovementSpeed;    //beweegt net zo snel als de top map  BEWEEGT NU IN DE MAP ZELF
    [SerializeField] float enemySpawnLocation = 18.16f;     //zodat de enemy boven aan het scherm spawned
    //private bool startBeforeDisable = false;

    [Header("SpawnSettings")]
    [SerializeField] private int currentEnemy;  // 0 = shooter; 1 = kamikaze; 2 = slug
    [SerializeField] private float spawnDelay = 0;
    [SerializeField] private float customSpeed;     // als dit 0 is is het de orginele snelheid
    [SerializeField] private bool singleSetSpeed;

    [SerializeField] private float slugFroggerMode = 0;     //0 is uit, 1 is van links en 2 is van rechts

    private SC_EnemySpawner enemySpawner = null;


    private void Start()
    {
        enemySpawner = FindObjectOfType<SC_EnemySpawner>();
    }


    private void FixedUpdate()
    {
        if (transform.position.z <= enemySpawnLocation && isActiveAndEnabled)
        {
            if (spawnDelay <= 0)
            {
                //Debug.Log("SetActive(false) om enemy te spawnen");
                enemySpawner.SpawnSpecificLocation(currentEnemy, transform.position, transform.rotation, singleSetSpeed, customSpeed, slugFroggerMode);
                gameObject.SetActive(false);
            }
            else
            {
                spawnDelay -= Time.fixedDeltaTime;
            }
        }

    }


}
