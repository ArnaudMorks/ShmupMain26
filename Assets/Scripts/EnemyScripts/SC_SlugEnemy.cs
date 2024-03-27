using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SlugEnemy : SC_EnemyBase
{
/*    protected override void Start()
    {
        base.Start();

        _rigidBody.AddForce(transform.forward * _enemySpeed);
    }*/

    protected override void OnEnable()
    {
        _rigidBody.velocity = Vector3.zero;    //zorgt dat de "AddForce" niet kan stacken
        _rigidBody.AddForce(transform.forward * _enemySpeed);
    }
}
