using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SlugEnemy : SC_EnemyBase
{
    private void Start()
    {
        _rigidBody.AddForce(transform.forward * _enemySpeed);
    }
}
