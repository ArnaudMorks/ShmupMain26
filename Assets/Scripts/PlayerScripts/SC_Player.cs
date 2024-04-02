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

    [SerializeField] private float baseMoveSpeed = 14;
    [SerializeField] private float setMoveSpeed;
    [SerializeField] private float slowZoneMoveSpeed;   //als je over bepaalde planten gaat ga je langzamer
    [SerializeField] private float currentMoveSpeed;
    private Vector3 movement;
    [SerializeField] private float knockBackForce;

    private SC_PlayerShooting playerShooting;
    private SC_PlayerHealth playerHealth;
    private SC_PowerupUI powerUpUI;     //MISSCHIEN LATER ANDERS
    [SerializeField] private GameObject invincibilityBubble;

    //[SerializeField] private bool canAddForce;
    //[SerializeField] private float canCollideKnockbackRate = 0.1f;
    //[SerializeField] private float collideKnockbackTimer;

    [Header("MaxMapWidth")]
    [SerializeField] private bool customWidthMode;
    [SerializeField] private float lightMapWidth = 17;
    [SerializeField] private float mediumMapWidth = 22;      //basis
    [SerializeField] private float maxMapWidth = 30;

    [SerializeField] private float currentMapWidth;



    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        setMoveSpeed = baseMoveSpeed;      //de logica; "setMoveSpeed" kan veranderen en de "baseMovespeed" is de standaard snelheid waar de player weer terug naar kan veranderen
        currentMoveSpeed = setMoveSpeed;

        playerShooting = gameObject.GetComponent<SC_PlayerShooting>();
        playerHealth = gameObject.GetComponent<SC_PlayerHealth>();
        //powerUpUI = FindObjectOfType<SC_PowerupUI>();

        //collideKnockbackTimer = canCollideKnockbackRate;

        currentMapWidth = maxMapWidth;        //misschien later veranderen wegens save en load (respawn)
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

        //De movement gekoppeld aan de input manager van Horizontal en Vertical
        movement.z = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        //moveSpeed = Mathf.Lerp(Camera.main.fieldOfView, targetFov, fovLerpTimer * Time.deltaTime);


        //MovmentBounds

        //if (canAddForce == false && collideKnockbackTimer >= canCollideKnockbackRate)
        //{
        //    collideKnockbackTimer -= Time.deltaTime;        // zodra de "spawnTimer" een hoger getal is dan de "spawnRate" dan wordt er iets gespawned
        //}
        //else if (canAddForce == false && collideKnockbackTimer <= 0)
        //{
        //    canAddForce = true;
        //}


        if (customWidthMode)
        {
            if (transform.position.x >= currentMapWidth)      //max width x position
            {
                if (movement.x > 0) { movement.x = 0; }
            }

            if (transform.position.x <= -currentMapWidth)      //min width x position
            {
                if (movement.x < 0) { movement.x = 0; }
            }
        }


        if (transform.position.z >= 15.8)      //max height y position
        {
            if (movement.z > 0) { movement.z = 0; }
        }

        if (transform.position.z <= -17.4)      //min height y position
        {
            movement.z = 1;
        }
        else if (transform.position.z <= -15.8)      //min height y position
        {
            if (movement.z < 0) { movement.z = 0; }
        }

    }

    void FixedUpdate()
    {
        //myRigidbody.MovePosition(myRigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
        myRigidbody.AddForce(movement * currentMoveSpeed);

        Vector3 flatVelocity = new Vector3(myRigidbody.velocity.x, 0, myRigidbody.velocity.z);

        /*        if (flatVelocity.magnitude > baseMoveSpeed + 0.4f)
                {

                }*/

        //limit velocity if needed

        if (flatVelocity.magnitude > currentMoveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * currentMoveSpeed;
            myRigidbody.velocity = new Vector3(limitedVelocity.x, myRigidbody.velocity.y, limitedVelocity.z);
            //print(limitedVelocity.magnitude);
        }

        /*        if (flatVelocity.magnitude > currentMoveSpeed)            nieuwe versie die niet werkt
                {
                    Vector3 limitedVelocity = flatVelocity.normalized * currentMoveSpeed;
                    Vector3 currentVelocity = myRigidbody.velocity;
                    currentVelocity += new Vector3(limitedVelocity.x, myRigidbody.velocity.y, limitedVelocity.z);
                    myRigidbody.velocity = currentVelocity;
                }*/
    }


    private void OnCollisionEnter(Collision collision)
    {
        myRigidbody.position += collision.contacts[0].normal * knockBackForce; //pakt eerste contact met de "[0]"; dus registreerd eerste botsting in het geval je meerdere contact punten hebt

        //if (canAddForce)
        //{
            //canAddForce = false;
            //myRigidbody.AddForce(collision.contacts[0].normal * knockBackForce, ForceMode.Force);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        SC_EmptyBushHitRef bush = other.gameObject.GetComponent<SC_EmptyBushHitRef>();
        if (bush != null)
        {
            currentMoveSpeed = slowZoneMoveSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SC_EmptyBushHitRef bush = other.gameObject.GetComponent<SC_EmptyBushHitRef>();
        if (bush != null)
        {
            currentMoveSpeed = setMoveSpeed;
        }
    }

    public void SpeedUpFromPowerup()        //max maar 2 keer   (voor nu idee)
    {
        setMoveSpeed += 2;
        currentMoveSpeed = setMoveSpeed;
    }


    public void SuperShooterModeSpeed()
    {
        //powerUpUI.PowerupModeUI();
        playerShooting.SuperShooterModeShooting();
        playerHealth.PlayerInvincibleMode();
        invincibilityBubble.SetActive(true);
        currentMoveSpeed = superModeMoveSpeed;
        Invoke("EndSuperShooterMode", superModeTime);       //moet net zo lang zijn als bij de "SC_PlayerShooting" SuperShooterMode
    }

    private void EndSuperShooterMode()
    {
        //powerUpUI.NormalModeUI();
        playerShooting.EndSuperShooterModeShooting();
        playerHealth.EndPlayerInvincibleMode();
        invincibilityBubble.SetActive(false);
        currentMoveSpeed = baseMoveSpeed;
    }


    public void SetMapWidthLight()
    {
        currentMapWidth = lightMapWidth;        //wordt vanuit de SC_MapEdgeOnSwitch aangeroepen
    }


    public void SetMapWidthMedium()
    {
        currentMapWidth = mediumMapWidth;        //wordt vanuit de SC_MapEdgeOnSwitch aangeroepen
    }

}
