using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All enemies must require the RigidBody component
[RequireComponent(typeof(Rigidbody))]
public abstract class SC_EnemyBase : MonoBehaviour
{
    protected Rigidbody _rigidBody;
    [SerializeField] protected float _enemySpeed;

    protected virtual void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
}
