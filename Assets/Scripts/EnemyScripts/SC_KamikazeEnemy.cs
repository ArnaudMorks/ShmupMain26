using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KamikazeState
{
    searching,
    lookingAtPlayer,
    attacking
}

public class SC_KamikazeEnemy : SC_EnemyBase
{
    private KamikazeState _kamikazeState;
    private Transform _playerTransform;
    private Coroutine _kamikazeCoroutine = null;

    [SerializeField] private float _enemyStopDrag;
    [SerializeField] private float _playerLookTime;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _destroyTime;

    protected override void Start()
    {
        base.Start();

        _kamikazeState = KamikazeState.searching;

        _rigidBody.AddForce(transform.forward * _enemySpeed);
    }

    protected override void Update()
    {
        base.Update();

        if(_kamikazeState == KamikazeState.lookingAtPlayer && _playerTransform != null)
        {
            Quaternion targetDirection = Quaternion.LookRotation(_playerTransform.transform.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, Time.deltaTime * _rotationSpeed);
        }
    }

    private IEnumerator BeginKamikaze()
    {
        _kamikazeState = KamikazeState.lookingAtPlayer;

        // Stop the player (not instantly) when we're looking at the player
        _rigidBody.drag = _enemyStopDrag;

        // Wait for a little bit of time
        yield return new WaitForSeconds(_playerLookTime);
        _rigidBody.drag = 0;

        // Stop looking at the player and start attacking
        _kamikazeState = KamikazeState.attacking;
        _rigidBody.AddForce(transform.forward * _enemySpeed);

        // Destroy the enemy after a certain amount of time because the enemy has the possibility to never reach the destroy threshold
        Destroy(gameObject, _destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider we hit has the player component
        if (other.TryGetComponent<SC_Player>(out _))
        {
            // We need the transform of the player to look at it
            _playerTransform = other.transform;

            // Start the kamikaze coroutine only once
            _kamikazeCoroutine ??= StartCoroutine(BeginKamikaze());
        }
    }
}
