using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerHealth : MonoBehaviour
{
    private Coroutine _damageCoroutine;
    private BoxCollider _collider;

    [SerializeField] private int _health;
    [SerializeField] private int _starterHealth;
    [SerializeField] private float _damageDelay;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        // Set the health to the starter health
        _health = _starterHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _damageCoroutine ??= StartCoroutine(TakeDamage());
    }

    private IEnumerator TakeDamage()
    {
        // Disable collision so you cant just run into enemies during invincible time
        _collider.enabled = false;

        // Remove one point of health and check if the health is 0 (or somehow less than 0)
        _health--;
        ServiceLocator.Main.HealthUIManager.UpdateHealthUI(_health);

        if(_health <= 0)
        {
            Destroy(gameObject);
        }

        // Wait for the delay
        yield return new WaitForSeconds(_damageDelay);

        // Enable collision and let the coroutine be able to run again
        _collider.enabled = true;

        _damageCoroutine = null;
    }
}
