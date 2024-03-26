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

    private bool superShooterMode = false;
    [SerializeField] private float superRateOfFire;
    [SerializeField] private float superBulletSpeed;


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
        currentPlayerBaseBulletSpeed = minPlayerBaseBulletSpeed;        //in het begin gaat de bullet de minimale snelheid MISSCHIEN LATER VERANDEREN
        bulletPool = FindObjectOfType<SC_PoolPlayerBullets>();
        rateOfFire = baseRateOfFire;        //hoe de logica werkt; misschien anders wegens respawns of iets dergelijks
    }

    void Update()
    {
        holdingFireButton = Input.GetKey(KeyCode.Space);

        FireProjectile();

        //print(currentPlayerBaseBulletSpeed);
    }


    private void FireProjectile()
    {
        if (superShooterMode == false)
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
        }
        else if (superShooterMode == true)          //snellere fire rate
        {
            if (fireRateTimer <= 0 && holdingFireButton)                //zodra "fireRateTimer" "0" of minder is kan je schieten
            {
                ProjectileBaseSpawnLocation();
                fireRateTimer = superRateOfFire;
            }
            else if (fireRateTimer > 0)
            {
                fireRateTimer -= Time.deltaTime;
            }
        }

    }

    private void ProjectileBaseSpawnLocation()
    {
        SC_MainBulletPlayer playerBullet;
        Vector3 turretOne = new Vector3(transform.position.x - 0.46f, transform.position.y, transform.position.z);
        Vector3 turretTwo = new Vector3(transform.position.x + 0.46f, transform.position.y, transform.position.z);      //getal is hoeveel van het midden af de kogel wordt afgeschoten

        if (superShooterMode)
        {
            playerBullet = bulletPool.ActivateBullet(turretOne, superBulletSpeed);
            playerBullet = bulletPool.ActivateBullet(turretTwo, superBulletSpeed);
        }
        else if (twoTurretModeBasePlayer == false) { playerBullet = bulletPool.ActivateBullet(transform.position, currentPlayerBaseBulletSpeed); }
        else if (twoTurretModeBasePlayer == true)
        {
            switch (currentTurret)
            {
                case 0:
                    //Instantiate(projectile, turretOne, transform.rotation);   OUDE MANIER
                    playerBullet = bulletPool.ActivateBullet(turretOne, currentPlayerBaseBulletSpeed);
                    currentTurret = 1;
                    break;
                case 1:
                    //Instantiate(projectile, turretTwo, transform.rotation);   OUDE MANIER
                    playerBullet = bulletPool.ActivateBullet(turretTwo, currentPlayerBaseBulletSpeed);
                    currentTurret = 0;
                    break;
                default:
                    Debug.Log("TURRET WERKT NIET");
                    break;
            }
        }

    }


    public void SuperShooterModeShooting()
    {
        superShooterMode = true;
        fireRateTimer = 0;      //zorgt dat je gelijk schiet als de powerup activeert zodat je niet eerst op je normale fire rate moet wachten
    }

    public void EndSuperShooterModeShooting()       //wordt aangeroepen vanuit "SC_Player"
    {
        superShooterMode = false;
    }

}
