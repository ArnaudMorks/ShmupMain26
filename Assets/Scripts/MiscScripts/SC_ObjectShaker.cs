using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ObjectShaker : MonoBehaviour
{
    private float _shakeIntensity;
    private bool _isShaking;
    [SerializeField] private Transform _positionTransform;
    private float _shakeSpeed;
    private float _shakeInterval;

    private void Update()
    {
        if (!_isShaking) { return; }
        // Only runs if isShaking is true

        _shakeSpeed -= Time.deltaTime;

        if(_shakeSpeed < 0)
        {
            Vector2 randomPosition = Random.insideUnitCircle * _shakeIntensity;
            Vector3 displacement = new(randomPosition.x, 0f, randomPosition.y);

            transform.position = _positionTransform.position + displacement;
            _shakeSpeed = _shakeInterval;
        }
    }

    public void ShakeObject(float intensity, float shakeSpeed)
    {
        _shakeIntensity = intensity;
        _shakeInterval = shakeSpeed;
        _isShaking = true;
    }
}
