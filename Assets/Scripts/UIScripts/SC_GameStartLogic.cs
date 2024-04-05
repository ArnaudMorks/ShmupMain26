using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GameStartLogic : MonoBehaviour
{
    [SerializeField] private SC_Player player;
    [SerializeField] private SC_PlayerShooting playerShooting;
    [SerializeField] private SC_PlayerHealth playerHealth;

    [SerializeField] private GameObject worldOne;
    [SerializeField] private GameObject worldTwo;

    [SerializeField] private GameObject miniBossLocation;

    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject shieldPowerup;


    private float movementX;
    private float movementZ;

    void Start()
    {
        player = FindObjectOfType<SC_Player>();
        playerShooting = FindObjectOfType<SC_PlayerShooting>();
        playerHealth = FindObjectOfType<SC_PlayerHealth>();
        //animationObject = FindObjectOfType<SC_StartAnimationObjectRef>();
        Time.timeScale = 0;
    }


    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        movementZ = Input.GetAxisRaw("Vertical");

        if (movementX != 0|| movementZ != 0)
        {
            StartLevel();
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            StartLevel();
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            StartLevelTwo();
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            StartLevelThree();
        }

    }



    public void StartLevelTwo()
    {
        playerShooting.CurrentPlayerBaseBulletSpeed *= 2;
        playerShooting.RateOfFire *= 0.78f;
        player.SpeedUpFromPowerup();
        worldOne.SetActive(false);
        worldTwo.SetActive(true);
        //worldTwo.transform.position = new Vector3(0, 0, 442);
        StartLevel();
    }


    public void StartLevelThree()
    {
        playerShooting.CurrentPlayerBaseBulletSpeed *= 2.4f;
        playerShooting.RateOfFire *= 0.4f;
        player.SpeedStartLevelThree();
        worldOne.SetActive(false);
        boss.SetActive(true);
        shieldPowerup.SetActive(true);
        StartLevel();
    }



    public void StartLevel()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

}
