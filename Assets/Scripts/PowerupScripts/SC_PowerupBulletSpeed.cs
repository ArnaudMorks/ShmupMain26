using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PowerupBulletSpeed : SC_PowerupBase
{
    private SC_ProjectileSpeedPlayer projectilePlayerManager;

    [SerializeField] private float bulletSpeedIncreaseRate;     //een * boven de nul

    protected override void Start()
    {
        base.Start();
        projectilePlayerManager = FindObjectOfType<SC_ProjectileSpeedPlayer>();
    }


    private void OnTriggerEnter(Collider other)
    {
        SC_PlayerHealth playerHealth = other.gameObject.GetComponent<SC_PlayerHealth>();

        if (playerHealth != null)
        {
            IncreaseBulletSpeed();
            Destroy(gameObject);
        }
    }


    private void IncreaseBulletSpeed()
    {
        projectilePlayerManager.CurrentPlayerBaseBulletSpeed += (projectilePlayerManager.MaxPlayerBaseBulletSpeed - projectilePlayerManager.CurrentPlayerBaseBulletSpeed) / bulletSpeedIncreaseRate;
    }
}
