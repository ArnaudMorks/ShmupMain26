using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerHealth : MonoBehaviour
{
    private Coroutine _damageCoroutine;
    private BoxCollider _collider;      //player collider

    [SerializeField] private int _health;
    [SerializeField] private int _starterHealth;
    [SerializeField] private float _damageDelay;
    [SerializeField] private bool _canTakeDamage;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        // Set the health to the starter health
        _health = _starterHealth;
        Physics.IgnoreLayerCollision(6, 7, false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_canTakeDamage == false)
        {
            
        }

            _damageCoroutine ??= StartCoroutine(TakeDamage());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            _damageCoroutine ??= StartCoroutine(TakeDamage());
        }
    }

    private IEnumerator TakeDamage()
    {
        // Disable collision so you cant just run into enemies during invincible time
        // _collider.enabled = false;
        Physics.IgnoreLayerCollision(6, 7, true);
        

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
        // _collider.enabled = true;
        Physics.IgnoreLayerCollision(6, 7, false);

        _damageCoroutine = null;
    }
}
