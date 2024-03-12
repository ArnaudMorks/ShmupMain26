using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }
}
