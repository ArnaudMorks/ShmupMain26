using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_LeftToRightMover : MonoBehaviour            //voor de shooter miniboss
{
    [SerializeField] private SC_GameWorldMove gameWorldMove;
    [SerializeField] private Rigidbody thisRigidBody;

    [SerializeField] private float mapWidth = 18;
    private bool cannotMoveLeft = false;
    private bool cannotMoveRight = false;

    private bool wantingToMoveLeft = false;
    private bool wantingToMoveRight = false;

    [SerializeField] private float movementSpeed;
    private int moveDirection;      // 0 is links 1 is rechts

    private bool onDeathMapMoveIncrease = false;
    [SerializeField] private float mapSpeedIncrease;


    void Start()
    {
        Debug.Log("MiniBoss Starter");
        Invoke("Move", 8);
    }



    private void FixedUpdate()
    {
        //MovmentBounds
        if (transform.position.x >= mapWidth)      //max width x position
        {
            print("cannot move right");
            cannotMoveRight = true;
        }

        if (transform.position.x <= -mapWidth)      //min width x position
        {
            cannotMoveLeft = true;
        }


        if (wantingToMoveLeft && cannotMoveLeft == false)
        {
            thisRigidBody.MovePosition(transform.position + -movementSpeed * Time.deltaTime * Vector3.right);
        }
        else if (wantingToMoveRight && cannotMoveRight == false)
        {
            thisRigidBody.MovePosition(transform.position + movementSpeed * Time.deltaTime * Vector3.right);
        }

    }

    private void Move()
    {
        if (onDeathMapMoveIncrease == false)
        onDeathMapMoveIncrease = true;

        moveDirection = Random.Range(0, 2);
        print(moveDirection);

        if (moveDirection == 0 && cannotMoveLeft == false)          //links
        {
            wantingToMoveRight = false;
            cannotMoveRight = false;
            wantingToMoveLeft = true;
        }
        else if (moveDirection == 1 && cannotMoveRight == false)    //rechts
        {
            wantingToMoveLeft = false;
            cannotMoveLeft = false;
            wantingToMoveRight = true;
        }

        Invoke("Move", 3);

    }


    private void OnDisable()
    {
        if (onDeathMapMoveIncrease) { gameWorldMove.GameWolrdMoveSpeed = mapSpeedIncrease; }
    }


}
