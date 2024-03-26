using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ObjectShaker : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _shakeIntensity;
    private bool _isShaking;
    private Vector3 _position;
    private float _shakeSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_isShaking) { return; }
        // Only runs if isShaking is true

        _shakeSpeed -= Time.deltaTime;

        if(_shakeSpeed < 0)
        {
            Vector2 randomPosition = Random.insideUnitCircle * _shakeIntensity;
            Vector3 displacement = new(randomPosition.x, 0f, randomPosition.y);

            _rigidbody.position = _position + displacement;
        }
    }

    public void ShakeObject(float intensity, float shakeSpeed)
    {
        _position = transform.position;
        _shakeIntensity = intensity;
        _shakeSpeed = shakeSpeed;
        _isShaking = true;
    }
}
