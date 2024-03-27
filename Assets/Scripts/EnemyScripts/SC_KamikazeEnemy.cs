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
    [SerializeField] private Quaternion _rotationBase;

    [SerializeField] private float _enemyStopDrag;
    [SerializeField] private float _playerLookTime;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _attackMoveMultiplier;
    [SerializeField] private float _destroyTimeBase;
    [SerializeField] private float _destroyTime;

    private BoxCollider _thisCollider;


    protected override void Start()
    {
        base.Start();
        _destroyTime = _destroyTimeBase;
        _kamikazeState = KamikazeState.searching;

        //_rigidBody.AddForce(transform.forward * _enemySpeed);
        //_rotationBase = transform.rotation;

        //_thisCollider = GetComponentInChildren<BoxCollider>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _kamikazeState = KamikazeState.searching;
        _destroyTime = _destroyTimeBase;
        transform.rotation = _rotationBase;
        //_rigidBody.velocity = Vector3.zero;    //zorgt dat de "AddForce" niet kan stacken
        _rigidBody.drag = 0;
        _rigidBody.AddForce(transform.forward * _enemySpeed);
    }

    private void OnDisable()
    {
        StopCoroutine(BeginKamikaze());
        _kamikazeCoroutine = null;
        _rigidBody.velocity = Vector3.zero;
    }

    protected override void Update()
    {
        base.Update();

        if (_kamikazeState == KamikazeState.attacking)
        {
            _destroyTime -= Time.deltaTime;

            if (_destroyTime <= 0)
            {
                gameObject.SetActive(false);        //disabled na een tijdje
            }
        }


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
        //Destroy(gameObject, _destroyTime);    OUDE MANIER
    }


    public void PlayerDetection(Transform playerTransform)
    {
        // We need the transform of the player to look at it
        _playerTransform = playerTransform;

        // Start the kamikaze coroutine only once
        _kamikazeCoroutine ??= StartCoroutine(BeginKamikaze());
    }

}
