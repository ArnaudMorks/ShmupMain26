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
    [SerializeField] private float _attackMoveMultiplier;
    [SerializeField] private float _destroyTime;

    private BoxCollider _thisCollider;


    protected override void Start()
    {
        base.Start();

        _kamikazeState = KamikazeState.searching;

        _rigidBody.AddForce(transform.forward * _enemySpeed);

        //_thisCollider = GetComponentInChildren<BoxCollider>();
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
        _rigidBody.AddForce(_attackMoveMultiplier * _enemySpeed * transform.forward);

        // Destroy the enemy after a certain amount of time because the enemy has the possibility to never reach the destroy threshold
        Destroy(gameObject, _destroyTime);
    }


    public void PlayerDetection(Transform playerTransform)
    {
        // We need the transform of the player to look at it
        _playerTransform = playerTransform;

        // Start the kamikaze coroutine only once
        _kamikazeCoroutine ??= StartCoroutine(BeginKamikaze());
    }

}
