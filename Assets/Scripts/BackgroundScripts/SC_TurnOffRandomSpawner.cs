using UnityEngine;

public class SC_TurnOffRandomSpawner : MonoBehaviour
{
    [SerializeField] private float activateLocation = 18.16f;
    private SC_SpawnManager spawnManager;


    void Start()
    {
        spawnManager = FindObjectOfType<SC_SpawnManager>();
    }


    private void FixedUpdate()
    {
        if (transform.position.z <= activateLocation && isActiveAndEnabled)
        {
            Debug.Log("Spawn Mode Off");
            spawnManager.SpawnModeOff();    //zet de random spawner uit
            spawnManager.StopRepeat();      //stopt powerups laten spawnen en cancelled alle invokes
            gameObject.SetActive(false);
        }

    }
}
