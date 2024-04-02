using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PowerupBulletSpeed : SC_PowerupBase
{
    private SC_PlayerShooting playerShooter;

    [SerializeField] private float bulletSpeedIncreaseRate;     //een * boven de nul

    protected override void Start()
    {
        base.Start();
        playerShooter = FindObjectOfType<SC_PlayerShooting>();
    }


    private void OnTriggerEnter(Collider other)
    {
        SC_PlayerHealth playerHealth = other.gameObject.GetComponent<SC_PlayerHealth>();

        if (playerHealth != null)
        {
            IncreaseBulletSpeed();
            ServiceLocator.Main.PickupUIManager.ModifyPickupAmount("BulletSpeed");
            Destroy(gameObject);
        }
    }


    private void IncreaseBulletSpeed()
    {
        playerShooter.CurrentPlayerBaseBulletSpeed += (playerShooter.MaxPlayerBaseBulletSpeed - playerShooter.CurrentPlayerBaseBulletSpeed) / bulletSpeedIncreaseRate;
    }
}
