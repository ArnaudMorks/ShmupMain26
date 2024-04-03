using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public enum DashState
{
    aiming,
    dashing,
    retreating,
    finished
}

public enum LaserState
{
    warning,
    beaming,
    finished
}

public enum BossState
{
    arriving,
    waitingforattack,
    attacking,
    dying
}

[RequireComponent(typeof(Rigidbody))]
public class SC_Boss : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _restingPosition = new(0f, 0f, 27.4f);
    private Transform _playerTransform;
    private SC_PoolBossBullets bulletPool = null;

    private DashState _dashState;

    [Header("Dashing")]
    [SerializeField] private float _aimTime;
    [SerializeField] private float _aimSpeed;
    [SerializeField] private float _minimumDistance;
    [SerializeField] private float _timeBeforeAttack;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _retreatSpeed;

    [Header("Missiles")]
    [SerializeField] private GameObject _missilePrefab;
    [SerializeField] private Transform[] _missileSpawnLocations;
    [SerializeField] private int _burstAmount;
    [SerializeField] private float _burstTime;


    private LaserState _laserState;

    [Header("Laser")]
    [SerializeField] private GameObject _lasers;
    [SerializeField] private GameObject _lasersWarning;
    [SerializeField] private float _laserSpeed;
    [SerializeField] private float _laserWarningTime;
    [SerializeField] private float _laserTime;


    [Header("Basic boss behavior")]
    [SerializeField] private BossState _state;
    [SerializeField] private AnimationCurve _arrivalCurve;
    [SerializeField] private float _arrivalSpeed;
    [SerializeField] private float _attackInterval; // In seconds
    [SerializeField] private int _health;
    [SerializeField] private float _timeUntilDeath;
    [SerializeField] private float _enragedAttackInterval;
    [SerializeField] private GameObject _deathParticle;
    private float _totalHealth;
    private bool _isEnraged = false;
    private SC_ObjectShaker _objectShaker;
    private float _storedAttackInterval;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _objectShaker = GetComponentInChildren<SC_ObjectShaker>();
        _storedAttackInterval = _attackInterval;
        _state = BossState.arriving;
        _totalHealth = _health;
    }

    private void Start()
    {
        bulletPool = FindObjectOfType<SC_PoolBossBullets>();
        _playerTransform = FindObjectOfType<SC_Player>().transform;
        StartCoroutine(ArriveAtScene());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_state == BossState.arriving) { return; }

        _health--;
        if (_health <= _totalHealth * 0.333 && !_isEnraged)
        {
            _objectShaker.ShakeObject(0.2f, 0.05f);
            _storedAttackInterval = _enragedAttackInterval;
            _isEnraged = true;
        }

        if (_health <= 0 && _state != BossState.dying) 
        {
            _state = BossState.dying;
            _objectShaker.ShakeObject(0.5f, 0.001f);
            StartCoroutine(Die());
        }
    }

    private void Update()
    {
        if(_state == BossState.waitingforattack)
        {
            _attackInterval -= Time.deltaTime;

            if (_attackInterval > 0) { return; }

            int randomIndex = Random.Range(0, 3);
            _state = BossState.attacking;
            _attackInterval = _storedAttackInterval;

            switch(randomIndex)
            {
                case 0:
                    print("charge attack");
                    StartCoroutine(ChargeAttack());
                    break;
                case 1:
                    print("shoot attack");
                    StartCoroutine(ShootMissiles());
                    break;
                case 2:
                    print("laser attack");
                    StartCoroutine(LaserAttack());
                    break;
            }
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(_timeUntilDeath);

        Instantiate(_deathParticle, transform.position - new Vector3(0, 0, 17), Quaternion.Euler(-90, 0, 0), null);
        ServiceLocator.Main.VictoryScreen.ShowVictoryScreen();
        Destroy(gameObject);
    }

    private IEnumerator ArriveAtScene()
    {
        while(_state == BossState.arriving)
        {
            Vector3 travelDirection = _restingPosition - _rigidbody.position;
            travelDirection.Normalize();

            float distance = Vector3.Distance(_restingPosition, _rigidbody.position);
            _rigidbody.MovePosition(_rigidbody.position + (travelDirection * _arrivalCurve.Evaluate(distance) * _arrivalSpeed));

            if(distance <= 0.2)
            {
                _rigidbody.position = _restingPosition;
                _state = BossState.waitingforattack;
            }

            yield return null;
        }
    }

    private IEnumerator ChargeAttack()
    {
        float aimTime = _aimTime;
        float dashTime = _dashTime;
        _dashState = DashState.aiming;

        while(_dashState == DashState.aiming && _playerTransform != null)
        {
            Vector3 targetPosition = new(_playerTransform.position.x, _rigidbody.position.y, _rigidbody.position.z);
            MoveTowardsTarget(targetPosition, _aimSpeed);

            aimTime -= Time.deltaTime;

            if (aimTime <= 0)
            {
                _dashState = DashState.dashing;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return new WaitForSeconds(_timeBeforeAttack);

        while(_dashState == DashState.dashing)
        {
            Vector3 targetPosition = new(transform.position.x, transform.position.y, 0);
            MoveTowardsTarget(targetPosition, _dashSpeed);

            dashTime -= Time.deltaTime;

            if (dashTime <= 0)
            {
                _dashState = DashState.retreating;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }

        while(_dashState == DashState.retreating)
        {
            MoveTowardsTarget(_restingPosition, _retreatSpeed);

            if(Vector3.Distance(transform.position, _restingPosition) <= 0.5)
            {
                _dashState = DashState.finished;
                transform.position = _restingPosition;
                break;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }

        _state = BossState.waitingforattack;
    }

    private IEnumerator ShootMissiles()
    {
        for(int i = 0; i < _burstAmount; i++)
        {
            for(int j = 0; j < _missileSpawnLocations.Length; j++)
            {
                //GameObject missile = Instantiate(_missilePrefab, _missileSpawnLocations[j].position, _missileSpawnLocations[j].rotation, null);
                SC_HomingMissile bossBullet;
                bossBullet = bulletPool.ActivateBossBullet(_missileSpawnLocations[j].position, _missileSpawnLocations[j].rotation);

                bossBullet.SetTarget(_playerTransform);
                //missile.GetComponent<SC_HomingMissile>().SetTarget(_playerTransform);
            }

            yield return new WaitForSeconds(_burstTime / _burstAmount);
        }

        _state = BossState.waitingforattack;
    }

    private IEnumerator LaserAttack()
    {
        _laserState = LaserState.warning;
        float laserWarningTime = _laserWarningTime;
        float laserTime = _laserTime;
        Quaternion startLookDirection;

        if(_playerTransform != null)
        {
            startLookDirection = Quaternion.LookRotation(_playerTransform.transform.position - transform.position);
            _lasersWarning.transform.rotation = startLookDirection;
        }

        _lasersWarning.SetActive(true);

        while (_laserState == LaserState.warning && _playerTransform != null)
        {
            // Get the target direction and slowly rotate towards it
            Quaternion targetDirection = Quaternion.LookRotation(_playerTransform.transform.position - _lasersWarning.transform.position);
            _lasersWarning.transform.rotation = Quaternion.Slerp(_lasersWarning.transform.rotation, targetDirection, Time.deltaTime * _laserSpeed);

            // Count down
            laserWarningTime -= Time.deltaTime;

            if(laserWarningTime <= 0)
            {
                _laserState = LaserState.beaming;
                _lasersWarning.SetActive(false);
                break;
            }

            yield return null;
        }

        _lasers.transform.rotation = _lasersWarning.transform.rotation;
        _lasers.SetActive(true);

        while (_laserState == LaserState.beaming && _playerTransform != null)
        {
            // Get the target direction and slowly rotate towards it
            Quaternion targetDirection = Quaternion.LookRotation(_playerTransform.transform.position - _lasers.transform.position);
            _lasers.transform.rotation = Quaternion.Slerp(_lasers.transform.rotation, targetDirection, Time.deltaTime * _laserSpeed);

            // Count down
            laserTime -= Time.deltaTime;

            if (laserTime <= 0)
            {
                _laserState = LaserState.finished;
                _lasers.SetActive(false);
                break;
            }

            yield return null;
        }

        _state = BossState.waitingforattack;
    }

    private void MoveTowardsTarget(Vector3 targetPosition, float speed)
    {
        Vector3 travelDirection = targetPosition - _rigidbody.position;
        travelDirection.Normalize();

        float distance = Vector3.Distance(targetPosition, _rigidbody.position);
        if (distance > _minimumDistance)
        {
            _rigidbody.MovePosition(_rigidbody.position + (speed * distance * travelDirection));
        }
    }
}
