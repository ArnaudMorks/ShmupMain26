using UnityEngine;

public class SC_UniqueOnDeathSpawn : MonoBehaviour
{
    [SerializeField] private GameObject toSpawn;
    private SC_SlugEnemy thisEnemy;
    private bool spawnedEnemies = false;

    void Start()
    {
        thisEnemy = GetComponentInParent<SC_SlugEnemy>();
    }


    void Update()
    {
        if (thisEnemy.HealthEnemy <= 1 && spawnedEnemies == false)
        {
            Instantiate(toSpawn, transform.position, transform.rotation);       //Later misschien uit een object pool halen
            spawnedEnemies = true;
        }
    }
}
