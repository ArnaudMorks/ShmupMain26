using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All enemies must require the RigidBody component
[RequireComponent(typeof(Rigidbody))]
public abstract class SC_EnemyBase : MonoBehaviour
{
    protected Rigidbody _rigidBody;
    [SerializeField] protected float _despawnPoint;
    [SerializeField] protected float _enemySpeed;

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
       // Destroy the gameObject if it goes past a certain point defined in the inspector window
        if(transform.position.z <= _despawnPoint)
            Destroy(gameObject);
    }
}
