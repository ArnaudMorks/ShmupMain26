using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SlugEnemy : SC_EnemyBase
{
    protected override void Start()
    {
        base.Start();

        _rigidBody.AddForce(transform.forward * _enemySpeed);
    }
}
