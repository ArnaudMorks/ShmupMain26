using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum DashState
{
    aiming,
    dashing,
    retreating
}

[RequireComponent(typeof(Rigidbody))]
public class SC_Boss : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _restingPosition;
    private Transform _playerTransform;

    // Dash attack
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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _restingPosition = transform.position;
    }

    private void Start()
    {
        _playerTransform = FindObjectOfType<SC_Player>().transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ShootMissiles());
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

            if(Vector3.Distance(transform.position, _restingPosition) <= 0.2)
            {
                transform.position = _restingPosition;
                break;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator ShootMissiles()
    {
        for(int i = 0; i < _burstAmount; i++)
        {
            for(int j = 0; j < _missileSpawnLocations.Length; j++)
            {
                GameObject missile = Instantiate(_missilePrefab, _missileSpawnLocations[j].position, _missileSpawnLocations[j].rotation, null);
                missile.GetComponent<SC_HomingMissile>().SetTarget(_playerTransform);
            }

            yield return new WaitForSeconds(_burstTime / _burstAmount);
        }
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
