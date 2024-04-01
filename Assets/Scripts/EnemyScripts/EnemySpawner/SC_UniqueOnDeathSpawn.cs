using UnityEngine;

public class SC_UniqueOnDeathSpawn : MonoBehaviour
{
    [SerializeField] private GameObject toSpawn;
    [SerializeField] private SC_SlugEnemy thisEnemy;
    private bool spawnedEnemies = false;
    private bool thisEnabled = false;
    [SerializeField] private Rigidbody thisRigidBody;
    [SerializeField] private float movementSpeed;        //moet gelijk zijn als die van de slug enemy

    private void OnEnable()
    {
        Debug.Log("This Enabled");
        thisEnabled = true;
        thisRigidBody.AddForce(transform.forward * movementSpeed);
    }

    void Update()
    {
        if ((thisEnemy.HealthEnemy <= 0 || thisEnemy.isActiveAndEnabled == false)  && spawnedEnemies == false && thisEnabled)
        {
            //Instantiate(toSpawn, transform.position, transform.rotation);       //Later misschien uit een object pool halen
            print("SPAWN");
            toSpawn.SetActive(true);
            spawnedEnemies = true;
        }
    }
}
