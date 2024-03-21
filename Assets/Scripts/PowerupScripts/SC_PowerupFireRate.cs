using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PowerupFireRate : SC_PowerupBase
{
    private SC_PlayerShooting playerShooting;

    [SerializeField] private float fireRateDecrease;     //een * onder de nul


    protected override void Start()
    {
        base.Start();
        playerShooting = FindObjectOfType<SC_PlayerShooting>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SC_PlayerHealth playerHealth = other.gameObject.GetComponent<SC_PlayerHealth>();

        if (playerHealth != null)
        {
            playerShooting.RateOfFire *= fireRateDecrease;
            Destroy(gameObject);
        }
    }
}
