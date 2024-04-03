using UnityEngine;

public class SC_MapSpeedSetter : MonoBehaviour
{
    [SerializeField] private float activateLocation = 18.16f;     //start zodra het boven aan het speelscherm is
    [SerializeField] private float newMapSpeed;
    [SerializeField] bool mapSpeedSet = false;

    //private bool startBeforeDisable = false;

    private SC_GameWorldMove gameWorldMove;

    [SerializeField] bool setSlugSpawner;      //later nog types van maken
    [SerializeField] bool setShooterSpawner;
    [SerializeField] bool setAllBasicEnemiesSpawner;
    [SerializeField] bool setKamikazeSpawner;
    [SerializeField] bool setSwarmSpawner;

    private SC_SpawnManager spawnManager;
    //private SC_EnemySpawner enemySpawner;

    private void Start()
    {
        print("Start");
        gameWorldMove = FindObjectOfType<SC_GameWorldMove>();

        spawnManager = FindObjectOfType<SC_SpawnManager>();
        //enemySpawner = FindObjectOfType<SC_EnemySpawner>();
        //startBeforeDisable = true;
    }

/*    private void OnDisable()
    {
        if (startBeforeDisable)
        {
            //print("MapSpeed");
            //Destroy(this, 0.2f);    //vernietigt zichzelf bijna gelijk nadat een enemy is gespawned
            if (setSpawnManager)
            {
                spawnManager.SlugEnemySpawnMode();
            }
        }

    }*/


    private void FixedUpdate()
    {
        if (transform.position.z <= activateLocation && transform.position.z >= -30f && mapSpeedSet == false && isActiveAndEnabled)
        {
            gameWorldMove.GameWolrdMoveSpeed = newMapSpeed;
            mapSpeedSet = true;
            if (setSlugSpawner)
            {
                spawnManager.SlugEnemySpawnMode();
            }
            else if (setShooterSpawner)
            {
                spawnManager.ShooterEnemySpawnMode();
            }
            else if (setAllBasicEnemiesSpawner)
            {
                spawnManager.BasicEnemiesSpawnMode();
            }
            else if (setKamikazeSpawner)
            {
                spawnManager.KamikazeEnemySpawnMode();
            }
            else if (setSwarmSpawner)
            {
                spawnManager.SwarmEnemiesSpawnMode();
            }

            gameObject.SetActive(false);
        }

    }


}
