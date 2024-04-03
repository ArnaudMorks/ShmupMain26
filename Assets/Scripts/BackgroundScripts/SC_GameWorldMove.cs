using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GameWorldMove : MonoBehaviour           //zit op de "World" GameObject en zorgt ervoor hoe snel de wereld beweegt
{
    [SerializeField] private float gameWorldMoveSpeed;
    public float GameWolrdMoveSpeed
    {
        get { return gameWorldMoveSpeed; }
        set { gameWorldMoveSpeed = value; }
    }


    private void FixedUpdate()
    {
        transform.position -= new Vector3(0, 0, gameWorldMoveSpeed * Time.fixedDeltaTime);
    }

}
