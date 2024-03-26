using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_HomingMissile : MonoBehaviour
{
    private Transform _target;
    private Rigidbody _rigidbody;
    private bool _lockedOn;

    [SerializeField] private float _missileSpeed;
    [SerializeField] private float _rotationSpeed;
    private float _lockOnTimeBase;
    [SerializeField] private float _lockOnTime;
    [SerializeField] private float _lockOnWaitTime;
    [SerializeField] private float _destroyTimeBase;
    [SerializeField] private float _destroyTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _lockOnTimeBase = _lockOnTime;
    }

    private void Start()
    {
        //StartCoroutine(LockOnAndLook());
        //Destroy(gameObject, _destroyTime);
    }

    private void OnEnable()
    {
        _lockedOn = false;
        StartCoroutine(LockOnAndLook());
        _destroyTime = _destroyTimeBase;
        _lockOnTime = _lockOnTimeBase;
    }

    private void Update()
    {
        _destroyTime -= Time.deltaTime;

        if (_destroyTime <= 0)
        {

            gameObject.SetActive(false);
        }

        if (_lockedOn && _target != null && _lockOnTime > 0)
        {
            Quaternion targetDirection = Quaternion.LookRotation(_target.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, Time.deltaTime * _rotationSpeed);

            _lockOnTime -= Time.deltaTime;

        }
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(transform.forward * _missileSpeed);
    }

    private IEnumerator LockOnAndLook()
    {
        yield return new WaitForSeconds(_lockOnWaitTime);

        _lockedOn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<SC_EmptyDetectionReference>(out _))
        {
            gameObject.SetActive(false);
        }
    }

    public void SetTarget(Transform target) { _target = target; }
}
