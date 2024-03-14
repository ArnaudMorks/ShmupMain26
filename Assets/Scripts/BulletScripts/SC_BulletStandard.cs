using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletStandard : MonoBehaviour
{

    private float despawnTimer = 2;

    [SerializeField] private float projectileSpeed;

    void Start()
    {
        Destroy(gameObject, despawnTimer);
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
