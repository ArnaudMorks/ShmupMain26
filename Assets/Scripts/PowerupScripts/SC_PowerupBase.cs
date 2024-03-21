using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PowerupBase : MonoBehaviour
{
    [SerializeField] protected float _despawnPoint = -21;
    protected Rigidbody rigidBody;
    [SerializeField] protected float powerupPickupSpeed = 10;

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        rigidBody.AddForce(transform.forward * powerupPickupSpeed);
    }

    protected virtual void Update()
    {
        if (transform.position.z <= _despawnPoint)      //werkt net als bij de "SC_EnemyBase"
            Destroy(gameObject);
    }

    protected virtual void FixedUpdate()
    {
        
    }

}
