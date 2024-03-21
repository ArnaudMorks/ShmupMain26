using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainBulletPlayer : MonoBehaviour
{

    private float despawnTimer = 3;

    private SC_ProjectileSpeedPlayer projectilePlayerManager;
    [SerializeField] private float mainPlayerBulletSpeed;


    void Start()
    {
        projectilePlayerManager = FindObjectOfType<SC_ProjectileSpeedPlayer>();

        mainPlayerBulletSpeed = projectilePlayerManager.CurrentPlayerBaseBulletSpeed;

        Destroy(gameObject, despawnTimer);
    }


    private void FixedUpdate()
    {
        transform.position += transform.forward * mainPlayerBulletSpeed * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
