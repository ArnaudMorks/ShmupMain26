using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WorldMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void FixedUpdate()
    {
        transform.position -= new Vector3(transform.position.x, transform.position.y, moveSpeed * Time.fixedDeltaTime);
    }
}
