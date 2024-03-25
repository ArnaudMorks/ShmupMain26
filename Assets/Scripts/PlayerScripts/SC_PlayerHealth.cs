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

    [SerializeField] private bool _invincibleMode;

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
        if (_invincibleMode == false)       //voor als je een powerup pakt; je kan alleen maar damage krijgen als je niet invincible bent
        {
            _damageCoroutine ??= StartCoroutine(TakeDamage());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_invincibleMode == false)       //voor als je een powerup pakt; je kan alleen maar damage krijgen als je niet invincible bent
        {
                if (other.gameObject.CompareTag("Projectile") && other.gameObject.layer != 9)       //layer 9 is de layer van playerprojectiles
            {
                _damageCoroutine ??= StartCoroutine(TakeDamage());
            }
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

        if (_health <= 0)
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


    public void PlayerInvincibleMode()              //wordt vanuit de "SC_Player" aangeroepen
    {
        Physics.IgnoreLayerCollision(6, 7, true);
        _invincibleMode = true;
    }

    public void EndPlayerInvincibleMode()
    {
        Physics.IgnoreLayerCollision(6, 7, false);
        _invincibleMode = false;
    }

}
