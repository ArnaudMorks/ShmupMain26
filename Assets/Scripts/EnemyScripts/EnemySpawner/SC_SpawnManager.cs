using UnityEngine;

public class SC_SpawnManager : MonoBehaviour
{
    private SC_PlayerHealth playerHealth;
    private SC_EnemySpawner enemySpawner;

    [SerializeField] private float slowSpawnRate;
    [SerializeField] private float mediumSpawnRate;

    [SerializeField] private GameObject fireRatePowerup = null;
    [SerializeField] private GameObject bulletSpeedPowerup = null;
    [SerializeField] private GameObject currentPowerup = null;
    [SerializeField] private GameObject setCurrentPowerup = null;       //wordt nu niet gebruikt; misschien handig als je specifiek één powerup wilt

    [SerializeField] private GameObject restoreHealthPowerup = null;

    private int randomSetterBasePowerup;

    private bool powerUpRepeatOn = false;
    private bool randomBasePowerup = true;

    private int restoreHealthChance;

    private void Start()
    {
        playerHealth = FindObjectOfType<SC_PlayerHealth>();
        enemySpawner = FindObjectOfType<SC_EnemySpawner>();
    }


/*    private void CurrentRandomBasePowerup()
    {
        //currentPowerup = fireRatePowerup;
    }*/
    public void SpawnModeOn() { enemySpawner.RandomSpawnerOn = true; }
    public void SpawnModeOff() { enemySpawner.RandomSpawnerOn = false; }

    //Powerup Repeats
    private void PowerupRepeatCurrent()         //nu is het altijd twee keer (snel achter elkaar) dezelfde powerup op enemies; later anders doen
    {
        if (powerUpRepeatOn) 
        {
            if (setCurrentPowerup != null && randomBasePowerup == false)
            {
                currentPowerup = setCurrentPowerup;
            }

            enemySpawner.CurrentPowerup = currentPowerup;
            Invoke("PowerupRepeatNull", 2);
        }
    }

    private void PowerupRepeatNull()
    {
        if (powerUpRepeatOn)
        {
            currentPowerup = null;
            enemySpawner.CurrentPowerup = currentPowerup;
            Invoke("PowerupRepeatCurrent", 8);
        }
    }


    private void RestoreHpRepeatCurrent()
    {
        if (powerUpRepeatOn)
        {
            if (setCurrentPowerup == null && playerHealth.CurrentHealthPlayer < playerHealth.StarterHealthPlayer)
            {
                restoreHealthChance = Random.Range(0, 4);       //kans om een heal te spawnen op een vijand ( 1/3e van de tijd wordt dit op een vijand gedaan wegens de repeat timer)
                //print("RandomHealChance");

                if (restoreHealthChance == 0)
                {
                    print("HealInEnemy");
                    currentPowerup = restoreHealthPowerup;
                    enemySpawner.CurrentPowerup = currentPowerup;
                }
                else
                {
                    currentPowerup = null;
                    enemySpawner.CurrentPowerup = currentPowerup;
                }

            }

            Invoke("RestoreHpRepeat", 1);
        }
    }

    private void RestoreHpRepeat()
    {
        if (powerUpRepeatOn)
        {
            if (currentPowerup == restoreHealthPowerup)
            {
                currentPowerup = null;
            }
            //print("RepeatHeal");
            //enemySpawner.CurrentPowerup = currentPowerup;
            Invoke("RestoreHpRepeatCurrent", 2);
        }
    }


    public void StopRepeat()
    {
        powerUpRepeatOn = false;
        PowerupIsNull();
    }


    private void RandomBasePowerup()
    {
        if (powerUpRepeatOn && randomBasePowerup)
        {
            randomSetterBasePowerup = Random.Range(0, 2);

            if (randomSetterBasePowerup == 0) { currentPowerup = fireRatePowerup; }
            else { currentPowerup = bulletSpeedPowerup; }

            Invoke("RandomBasePowerup", 0.2f);
        }
    }


    private void PowerupIsNull()
    {
        CancelInvoke();     //stop alle Invokes
        currentPowerup = null;
        enemySpawner.CurrentPowerup = currentPowerup;
        Debug.Log("Stop Random spawner en stop Generating Powerups");
    }



    //LEVEL 1
    public void SlugEnemySpawnMode()        //begin eerste level        wordt vanuit "SC_MapSpeedSetter" opgeroepen
    {
        //basis setting horen bij deze spawn mode
        enemySpawner.CurrentSpawnRate = mediumSpawnRate;
        Invoke("SpawnModeOn", 2);
        Invoke("SlugSpawnRateUp", 8);
    }

    private void SlugSpawnRateUp()
    {
        //enemySpawner.CurrentPowerup = currentPowerup;
        enemySpawner.CurrentSpawnRate = enemySpawner.CurrentSpawnRate * 0.72f;
        randomBasePowerup = true;
        powerUpRepeatOn = true;         //deze wordt gebruikt in de repeat
        Invoke("RandomBasePowerup", 2);
        Invoke("PowerupRepeatCurrent", 2);
        Invoke("RestoreHpRepeatCurrent", 2);
        Invoke("StopRepeat", 45);       //stopt ook alle Invokes
    }




    //LEVEL 2
    public void ShooterEnemySpawnMode()        //begin eerste level        wordt vanuit "SC_MapSpeedSetter" opgeroepen
    {
        enemySpawner.CurrentSpawnRate = slowSpawnRate;
        enemySpawner.SpawnerLevel = 5;      //alleen de shooter enemy spawned nu
        //basis setting horen bij deze spawn mode
        Invoke("SpawnModeOn", 2);
        Invoke("ShooterSpawnerRateUp", 10);
    }

    private void ShooterSpawnerRateUp()
    {
        enemySpawner.CurrentSpawnRate = enemySpawner.CurrentSpawnRate * 0.64f;
        randomBasePowerup = true;
        powerUpRepeatOn = false;
        //Invoke("RandomBasePowerup", 2);
        //Invoke("PowerupRepeatCurrent", 2);
        //Invoke("RestoreHpRepeatCurrent", 2);
        //Invoke("StopRepeat", 28);
    }



    public void BasicEnemiesSpawnMode()        //begin eerste level        wordt vanuit "SC_MapSpeedSetter" opgeroepen
    {
        enemySpawner.CurrentSpawnRate = mediumSpawnRate;
        enemySpawner.SetSpawnWidthAreaSecondLevel();
        enemySpawner.SpawnerLevel = 4;      //alle basic enemies
        //basis setting horen bij deze spawn mode
        Invoke("SpawnModeOn", 2);
        Invoke("BasicEnemiesSpawnerRateUpOne", 10);
    }

    private void BasicEnemiesSpawnerRateUpOne()
    {
        enemySpawner.CurrentSpawnRate = enemySpawner.CurrentSpawnRate * 0.76f;
        randomBasePowerup = true;
        //powerUpRepeatOn = false;
        randomBasePowerup = true;
        powerUpRepeatOn = true;         //deze wordt gebruikt in de repeat
        Invoke("RandomBasePowerup", 1.8f);
        Invoke("PowerupRepeatCurrent", 1.8f);
        Invoke("RestoreHpRepeatCurrent", 1.8f);
        Invoke("BasicEnemiesSpawnerRateUpTwo", 60);
    }

    private void BasicEnemiesSpawnerRateUpTwo()
    {
        enemySpawner.CurrentSpawnRate = enemySpawner.CurrentSpawnRate * 0.76f;
        enemySpawner.CurrentSpawnRate = enemySpawner.NormalCurrentSpawnRate * 0.76f;
        //Invoke("StopRepeat", 20);
    }



    public void KamikazeEnemySpawnMode()        //begin eerste level        wordt vanuit "SC_MapSpeedSetter" opgeroepen
    {
        enemySpawner.CurrentSpawnRate = slowSpawnRate;
        enemySpawner.SetSpawnWidthAreaSecondLevel();
        enemySpawner.SpawnerLevel = 6;      //alleen de kamikaze enemy spawned nu
        //basis setting horen bij deze spawn mode
        Invoke("SpawnModeOn", 1);
        Invoke("KamikazeSpawnerRateUp", 8);
    }

    private void KamikazeSpawnerRateUp()
    {
        enemySpawner.CurrentSpawnRate = enemySpawner.CurrentSpawnRate * 0.62f;
        //randomBasePowerup = true;
        //powerUpRepeatOn = false;
        //Invoke("RandomBasePowerup", 2);
        //Invoke("PowerupRepeatCurrent", 2);
        //Invoke("RestoreHpRepeatCurrent", 2);
        //Invoke("StopRepeat", 28);
    }



    public void SwarmEnemiesSpawnMode()        //begin eerste level        wordt vanuit "SC_MapSpeedSetter" opgeroepen
    {
        enemySpawner.CurrentSpawnRate = mediumSpawnRate;
        enemySpawner.SetSpawnWidthAreaSecondLevel();
        enemySpawner.SpawnerLevel = 4;      //alle basic enemies
        enemySpawner.SwarmSpawnChance();
        //basis setting horen bij deze spawn mode
        Invoke("SpawnModeOn", 1);
        Invoke("SwarmEnemiesSpawnerRateUpOne", 10);
    }

    private void SwarmEnemiesSpawnerRateUpOne()
    {
        enemySpawner.CurrentSpawnRate = enemySpawner.CurrentSpawnRate * 0.5f;
        enemySpawner.CurrentSpawnRate = enemySpawner.NormalCurrentSpawnRate * 0.5f;
        randomBasePowerup = true;
        randomBasePowerup = true;
        powerUpRepeatOn = true;         //deze wordt gebruikt in de repeat
        Invoke("RandomBasePowerup", 1.8f);
        Invoke("PowerupRepeatCurrent", 1.8f);
        Invoke("RestoreHpRepeatCurrent", 1.8f);
        Invoke("SwarmEnemiesSpawnerRateUpTwo", 60);
    }

    private void SwarmEnemiesSpawnerRateUpTwo()
    {
        enemySpawner.CurrentSpawnRate = enemySpawner.CurrentSpawnRate * 0.76f;
        enemySpawner.CurrentSpawnRate = enemySpawner.NormalCurrentSpawnRate * 0.76f;
        Invoke("StopRepeat", 20);
    }

}
