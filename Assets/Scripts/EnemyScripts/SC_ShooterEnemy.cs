using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ShooterEnemy : SC_EnemyBase
{
    [SerializeField] private Transform _bulletSpawnPoint;
    //[SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _burstAmount;
    [SerializeField] private float _burstTime;
    [SerializeField] private float _burstInterval;

    [SerializeField] private SC_PoolEnemyBullets bulletPool = null;

    [SerializeField] private bool startBeforeActivate = false;      //check zodat de "OnEnable" niet eerst uitgevoert wordt


    protected override void Start()
    {
        base.Start();
        bulletPool = FindObjectOfType<SC_PoolEnemyBullets>();
        startBeforeActivate = true;
    }

    private void OnEnable()
    {
        if (startBeforeActivate == true)
        {
            // Start shooting projectiles in bursts
            bulletPool = FindObjectOfType<SC_PoolEnemyBullets>();
            StartCoroutine(ShootBullets());
        }
    }

    private void FixedUpdate()
    {
        // Move forward
        _rigidBody.MovePosition(transform.position + -_enemySpeed * Time.deltaTime * Vector3.forward);
    }

    private IEnumerator ShootBullets()
    {
        // We want this coroutine to run forever (its easier to work with them for something like this)
        // Because we we will run into a yield statement, this is perfectly safe
        while(true)
        {
            // Instantiate the prefab amount of times we want in a burst and wait the total burst time divided by the burst amount after every shot
            for(int i = 0; i < _burstAmount; i++)
            {
                //Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
                SC_BulletStandard enemyBullet;

                enemyBullet = bulletPool.ActivateEnemyBullet(transform.position);

                yield return new WaitForSeconds(_burstTime / _burstAmount);
            }
                //hier debuggen ZO
            // Wait the interval delay
            yield return new WaitForSeconds(_burstInterval);
        }

    }

    private void Shoot()
    {
        SC_BulletStandard enemyBullet;

        enemyBullet = bulletPool.ActivateEnemyBullet(transform.position);

    }

}
