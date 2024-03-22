using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerShooting : MonoBehaviour
{
    [Header("Bullet Speed")]            //was eerst een ander script
    [SerializeField] private float minPlayerBaseBulletSpeed;
    public float MinPlayerBaseBulletSpeed
    {
        get { return minPlayerBaseBulletSpeed; }
    }

    [SerializeField] private float currentPlayerBaseBulletSpeed;
    public float CurrentPlayerBaseBulletSpeed
    {
        get { return currentPlayerBaseBulletSpeed; }
        set { currentPlayerBaseBulletSpeed = value; }       //wordt aangepast door de "SC_PowerupBulletSpeed"
    }

    [SerializeField] private float maxPlayerBaseBulletSpeed;
    public float MaxPlayerBaseBulletSpeed
    {
        get { return maxPlayerBaseBulletSpeed; }
    }

    [Header("The rest")]
    [SerializeField] private SC_PoolPlayerBullets bulletPool = null;

    [SerializeField] private GameObject projectile;

    [SerializeField] private float baseRateOfFire;
    [SerializeField] private float rateOfFire;
    public float RateOfFire
    {
        get { return rateOfFire; }
        set { rateOfFire = value; }
    }

    [SerializeField] private float fireRateTimer;       //begint op 0; betekend dat je kan schieten

    private bool holdingFireButton = false;

    //private Vector3 currentTurretPosition;
    [SerializeField] private bool twoTurretModeBasePlayer = false;
    public bool TwoTurretModeBasePlayer
    {
        set { twoTurretModeBasePlayer = value; }
    }
    private int currentTurret = 0;      //switch tussen twee turrets als turret mode aan staat


    private void Start()
    {
        rateOfFire = baseRateOfFire;        //hoe de logica werkt; misschien anders wegens respawns of iets dergelijks
    }

    void Update()
    {
        holdingFireButton = Input.GetKey(KeyCode.Space);

        FireProjectile();


    }


    private void FireProjectile()
    {
        if (fireRateTimer <= 0 && holdingFireButton)                //zodra "fireRateTimer" "0" of minder is kan je schieten
        {
            ProjectileBaseSpawnLocation();
            fireRateTimer = rateOfFire;
        }
        else if (fireRateTimer > 0)
        {
            fireRateTimer -= Time.deltaTime;
        }

        Vector3 turretOne = new Vector3(transform.position.x, transform.position.y - 0.46f, transform.position.z);
        Vector3 turretTwo = new Vector3(transform.position.x, transform.position.y + 0.46f, transform.position.z);
    }

    private void ProjectileBaseSpawnLocation()
    {
        if (twoTurretModeBasePlayer == false) { Instantiate(projectile, transform.position, transform.rotation); }
        else
        {
            Vector3 turretOne = new Vector3(transform.position.x - 0.46f, transform.position.y, transform.position.z);
            Vector3 turretTwo = new Vector3(transform.position.x + 0.46f, transform.position.y, transform.position.z);      //getal is hoeveel van het midden af de kogel wordt afgeschoten

            switch (currentTurret)
            {
                case 0:
                    Instantiate(projectile, turretOne, transform.rotation);
                    currentTurret = 1;
                    break;
                case 1:
                    Instantiate(projectile, turretTwo, transform.rotation);
                    currentTurret = 0;
                    break;
                default:
                    Debug.Log("TURRET WERKT NIET");
                    break;
            }
        }
    }

}
