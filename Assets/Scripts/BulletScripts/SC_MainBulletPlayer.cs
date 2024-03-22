using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainBulletPlayer : MonoBehaviour
{

    //private float despawnTimer = 3;

    private SC_ProjectileSpeedPlayer projectilePlayerManager;
    [SerializeField] private float mainPlayerBulletSpeed;


    void Start()
    {
        projectilePlayerManager = FindObjectOfType<SC_ProjectileSpeedPlayer>();

        //mainPlayerBulletSpeed = projectilePlayerManager.CurrentPlayerBaseBulletSpeed;

        //Destroy(gameObject, despawnTimer);    OUDE MANIER
    }


    private void FixedUpdate()
    {
        transform.position += transform.forward * mainPlayerBulletSpeed * Time.deltaTime;

        if (transform.position.z >= 33)     //net uit het scherm despawnen de bullets
        {
            gameObject.SetActive(false);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

    public void SetSpeed(float newSpeed) { mainPlayerBulletSpeed = newSpeed; }
}
