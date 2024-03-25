using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All enemies must require the RigidBody component
[RequireComponent(typeof(Rigidbody))]
public abstract class SC_EnemyBase : MonoBehaviour
{
    private SC_ScoreManager _scoreManager;

    protected Coroutine _damageCoroutine = null;
    protected Rigidbody _rigidBody;
    [SerializeField] protected float _despawnPoint;
    [SerializeField] protected float _enemySpeed;
    [SerializeField] protected int _health;
    [SerializeField] protected float _damageDelay;
    [SerializeField] protected int _scoreOnDeath;

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        _scoreManager = ServiceLocator.Main.ScoreManager;
    }

    protected virtual void Update()
    {
       // Destroy the gameObject if it goes past a certain point defined in the inspector window
        if(transform.position.z <= _despawnPoint)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _damageCoroutine ??= StartCoroutine(TakeDamage());
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Powerup"))
        {
            print(other);
            _damageCoroutine ??= StartCoroutine(TakeDamage()); 
        }
    }

    protected virtual IEnumerator TakeDamage()
    {
        // Actually take damage
        _health--;

        // Check if the enemy is dead
        if (_health <= 0)
        {
            _scoreManager.ModifyScore(_scoreOnDeath);

            Destroy(gameObject);
        }

        yield return null;

        _damageCoroutine = null;
    }
}
