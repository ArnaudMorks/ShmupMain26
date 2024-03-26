using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletStandard : MonoBehaviour
{

    [SerializeField] protected float _despawnPoint = -20;

    [SerializeField] private float projectileSpeed;


    private void FixedUpdate()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;

        if (transform.position.z <= -20)     //net uit het scherm despawnen de bullets
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }


    //public void SetEnemyBulletSpeed(float newSpeed) { projectileSpeed = newSpeed; }      //wordt bepaald in de "SC_PlayerShooting" en de "SC_PoolPlayerBullets"

}
