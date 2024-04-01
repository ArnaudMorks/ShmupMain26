using UnityEngine;

public class SC_MapSpeedSetter : MonoBehaviour
{
    [SerializeField] private float activateLocation = 18.16f;     //start zodra het boven aan het speelscherm is
    [SerializeField] private float newMapSpeed;
    [SerializeField] bool mapSpeedSet = false;

    private bool startBeforeDisable = false;

    private SC_GameWorldMove gameWorldMove;

    [SerializeField] bool setSpawnManager;      //later nog types van maken

    private SC_SpawnManager spawnManager;
    //private SC_EnemySpawner enemySpawner;

    private void Start()
    {
        print("Start");
        gameWorldMove = FindObjectOfType<SC_GameWorldMove>();

        spawnManager = FindObjectOfType<SC_SpawnManager>();
        //enemySpawner = FindObjectOfType<SC_EnemySpawner>();
        startBeforeDisable = true;
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
        if (transform.position.z <= activateLocation && mapSpeedSet == false && isActiveAndEnabled)
        {
            gameWorldMove.GameWolrdMoveSpeed = newMapSpeed;
            mapSpeedSet = true;
            if (setSpawnManager)
            {
                spawnManager.SlugEnemySpawnMode();
            }
            gameObject.SetActive(false);
        }

    }


}
