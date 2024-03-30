using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PowerupBase : MonoBehaviour
{
    [SerializeField] protected float _despawnPoint = -21;
    protected Rigidbody rigidBody;
    [SerializeField] protected float powerupPickupSpeedBase;
    [SerializeField] protected float powerupPickupSpeed = 10;
    [SerializeField] protected float pickupSpeedOffset;

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        float minOffset = powerupPickupSpeedBase - pickupSpeedOffset;
        float maxOffset = powerupPickupSpeedBase + pickupSpeedOffset;

        powerupPickupSpeed = Random.Range(minOffset, maxOffset);

        rigidBody.AddForce(transform.forward * powerupPickupSpeed);
    }

    protected virtual void FixedUpdate()
    {
        if (transform.position.z <= _despawnPoint)      //werkt net als bij de "SC_EnemyBase"
            Destroy(gameObject);
    }

}