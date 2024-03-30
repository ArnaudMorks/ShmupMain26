using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SlugEnemy : SC_EnemyBase
{
    private float slugFroggerMode = 0;
    public float SlugFroggerMode
    {
        set { slugFroggerMode = value; }
    }
    [SerializeField] private float froggerModeSpeed = 60;
/*    protected override void Start()
    {
        base.Start();

        _rigidBody.AddForce(transform.forward * _enemySpeed);
    }*/

    protected override void OnEnable()
    {
        base.OnEnable();
        _rigidBody.velocity = Vector3.zero;    //zorgt dat de "AddForce" niet kan stacken

        float minOffset = _enemySpeedBase - _enemySpeedOffset;
        float maxOffset = _enemySpeedBase + _enemySpeedOffset;

        if (_singleSetSpeed) { _enemySpeed = _enemySpeedBase; }
        else { _enemySpeed = Random.Range(minOffset, maxOffset); }

        _rigidBody.AddForce(transform.forward * _enemySpeed);

        if (slugFroggerMode == 1)       //Specifiek onderdeel in level 1
        {
            _rigidBody.AddForce(transform.right * froggerModeSpeed);
        }
        else if (slugFroggerMode == 2)
        {
            _rigidBody.AddForce(transform.right * -froggerModeSpeed);
        }
    }

}
