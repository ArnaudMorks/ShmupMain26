using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private float rateOfFire;
    [SerializeField] private float fireRateTimer;

    private bool holdingFireButton = false;


    void Update()
    {
        holdingFireButton = Input.GetKey(KeyCode.Space);

/*        if (Input.GetKey(KeyCode.Space))
        {
            holdingFireButton = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            holdingFireButton = false;
        }*/

            FireProjectile();
    }


    private void FireProjectile()
    {
        if (fireRateTimer <= 0 && holdingFireButton)                //zodra "fireRateTimer" "0" of minder is kan je schieten
        {
            Instantiate(projectile, transform.position, transform.rotation);
            fireRateTimer = rateOfFire;
        }
        else if (fireRateTimer > 0)
        {
            fireRateTimer -= Time.deltaTime;
        }
    }

}
