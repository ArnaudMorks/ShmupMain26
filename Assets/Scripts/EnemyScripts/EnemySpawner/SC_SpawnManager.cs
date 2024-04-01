using UnityEngine;

public class SC_SpawnManager : MonoBehaviour
{
    private SC_PlayerHealth playerHealth;
    private SC_EnemySpawner enemySpawner;

    [SerializeField] private GameObject fireRatePowerup = null;
    [SerializeField] private GameObject bulletSpeedPowerup = null;
    [SerializeField] private GameObject currentPowerup = null;
    [SerializeField] private GameObject setCurrentPowerup = null;       //wordt nu niet gebruikt; misschien handig als je specifiek ��n powerup wilt

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


    private void StopRepeat()
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


    public void SlugEnemySpawnMode()        //begin eerste level        wordt vanuit "SC_MapSpeedSetter" opgeroepen
    {
        //basis setting horen bij deze spawn mode
        Invoke("SpawnModeOn", 2);
        Invoke("SlugSpawnRateUp", 10);
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


}