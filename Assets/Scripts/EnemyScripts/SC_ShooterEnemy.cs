using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ShooterEnemy : SC_EnemyBase
{
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _burstAmount;
    [SerializeField] private float _burstTime;
    [SerializeField] private float _burstInterval;

    private void Start()
    {
        // Move forward
        _rigidBody.AddForce(transform.forward * _enemySpeed);

        // Start shooting projectiles in bursts
        StartCoroutine(ShootBullets());
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
                Instantiate(_bulletPrefab, _bulletSpawnPoint);

                yield return new WaitForSeconds(_burstTime / _burstAmount);
            }

            // Wait the interval delay
            yield return new WaitForSeconds(_burstInterval);
        }
    }
}
