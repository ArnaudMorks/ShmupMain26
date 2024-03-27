using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WorldTopMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(transform.position.x, transform.position.y, moveSpeed * Time.fixedDeltaTime);
    }
}
