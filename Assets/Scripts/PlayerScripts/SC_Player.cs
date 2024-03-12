using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Player : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigidbody;


    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float moveSpeed;
    Vector3 movement;


    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        moveSpeed = baseMoveSpeed;      //de logica; "moveSpeed" kan veranderen en de "baseMovespeed" is de standaard snelheid waar de player weer terug naar kan veranderen
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        //De movement gekoppeld aan de input manager van Horizontal en Vertical
        movement.z = Input.GetAxisRaw("Vertical");

        movement.Normalize();


    }

    void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
