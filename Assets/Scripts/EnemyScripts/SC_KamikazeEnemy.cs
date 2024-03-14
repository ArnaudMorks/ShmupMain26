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

    [SerializeField]private float _searchDelay;
    [SerializeField] private float _enemyStopDrag;
    [SerializeField] private float _searchRadius;
    [SerializeField] private float _playerLookTime;
    [SerializeField] private float _destroyTime;

    private void Start()
    {
        _kamikazeState = KamikazeState.searching;

        _rigidBody.AddForce(transform.forward * _enemySpeed);
        StartCoroutine(BeginKamikaze());
    }

    protected override void Update()
    {
        base.Update();

        if(_kamikazeState == KamikazeState.lookingAtPlayer)
        {
            transform.LookAt(_playerTransform);
        }
    }

    private IEnumerator BeginKamikaze()
    {
        while(_kamikazeState == KamikazeState.searching)
        {
            // We dont really need to search in very quick succession, so we wait
            yield return new WaitForSeconds(_searchDelay);

            CheckForPlayer();
        }

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

    private void CheckForPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _searchRadius);
        foreach(var hitCollider in hitColliders)
        {
            // Check if the collider we hit has the player component
            if(hitCollider.TryGetComponent<SC_Player>(out _))
            {
                // We need the transform of the player
                _playerTransform =  hitCollider.transform;

                // Change the state so we can look at the player
                _kamikazeState = KamikazeState.lookingAtPlayer;
            }
        }
    }
}
