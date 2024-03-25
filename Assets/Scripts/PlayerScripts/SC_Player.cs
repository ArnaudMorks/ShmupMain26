using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Player : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigidbody;

    [SerializeField] private float superModeTime = 6;
    public float SuperModeTime
    {
        get { return superModeTime; }
        set { superModeTime = value; }
    }

    [SerializeField] private float superModeMoveSpeed;

    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float moveSpeed;
    private Vector3 movement;

    private SC_PlayerShooting playerShooting;
    private SC_PlayerHealth playerHealth;
    private SC_PowerupUI powerUpUI;     //MISSCHIEN LATER ANDERS
    [SerializeField] private GameObject invincibilityBubble;


    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        moveSpeed = baseMoveSpeed;      //de logica; "moveSpeed" kan veranderen en de "baseMovespeed" is de standaard snelheid waar de player weer terug naar kan veranderen

        playerShooting = gameObject.GetComponent<SC_PlayerShooting>();
        playerHealth = gameObject.GetComponent<SC_PlayerHealth>();
        powerUpUI = FindObjectOfType<SC_PowerupUI>();
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        //De movement gekoppeld aan de input manager van Horizontal en Vertical
        movement.z = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        //moveSpeed = Mathf.Lerp(Camera.main.fieldOfView, targetFov, fovLerpTimer * Time.deltaTime);


        //MovmentBounds
        if (transform.position.x >= 22)      //max width x position
        {
            if (movement.x > 0) { movement.x = 0; }
        }

        if (transform.position.x <= -22)      //min width x position
        {
            if (movement.x < 0) { movement.x = 0; }
        }

        if (transform.position.z >= 15.8)      //max height y position
        {
            if (movement.z > 0) { movement.z = 0; }
        }

        if (transform.position.z <= -15.8)      //min height y position
        {
            if (movement.z < 0) { movement.z = 0; }
        }

    }

    void FixedUpdate()
    {
        //myRigidbody.MovePosition(myRigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
        myRigidbody.AddForce(movement * moveSpeed);

        Vector3 flatVelocity = new Vector3(myRigidbody.velocity.x, 0, myRigidbody.velocity.z);

        /*        if (flatVelocity.magnitude > baseMoveSpeed + 0.4f)
                {

                }*/

        //limit velocity if needed
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            myRigidbody.velocity = new Vector3(limitedVelocity.x, myRigidbody.velocity.y, limitedVelocity.z);
            //print(limitedVelocity.magnitude);
        }
    }


    public void SuperShooterModeSpeed()
    {
        powerUpUI.PowerupModeUI();
        playerShooting.SuperShooterModeShooting();
        playerHealth.PlayerInvincibleMode();
        invincibilityBubble.SetActive(true);
        moveSpeed = superModeMoveSpeed;
        Invoke("EndSuperShooterMode", superModeTime);       //moet net zo lang zijn als bij de "SC_PlayerShooting" SuperShooterMode
    }

    private void EndSuperShooterMode()
    {
        powerUpUI.NormalModeUI();
        playerShooting.EndSuperShooterModeShooting();
        playerHealth.EndPlayerInvincibleMode();
        invincibilityBubble.SetActive(false);
        moveSpeed = baseMoveSpeed;
    }

}
