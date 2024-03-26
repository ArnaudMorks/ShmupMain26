using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainBulletPlayer : MonoBehaviour
{

    //private float despawnTimer = 3;

    //private SC_PlayerShooting projectilePlayerManager;
    [SerializeField] private float mainPlayerBulletSpeed;

    void Start()
    {
        //projectilePlayerManager = FindObjectOfType<SC_PlayerShooting>();

        //mainPlayerBulletSpeed = projectilePlayerManager.CurrentPlayerBaseBulletSpeed;

        //Destroy(gameObject, despawnTimer);    OUDE MANIER
    }


    private void FixedUpdate()
    {
        transform.position += transform.forward * mainPlayerBulletSpeed * Time.deltaTime;

        if (transform.position.z >= 20)     //net uit het scherm despawnen de bullets
        {
            gameObject.SetActive(false);
        }

        //print(mainPlayerBulletSpeed);
    }


/*    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        SC_EnemyBase enemyBase = other.gameObject.GetComponent<SC_EnemyBase>();
        SC_Boss boss = other.gameObject.GetComponent<SC_Boss>();
        //hier later terrain reference
        

        if (other.isTrigger == false && (enemyBase != null || boss != null))
        { 
            gameObject.SetActive(false);
        }

    }

    public void SetSpeed(float newSpeed) { mainPlayerBulletSpeed = newSpeed; }      //wordt bepaald in de "SC_PlayerShooting" en de "SC_PoolPlayerBullets"
}
