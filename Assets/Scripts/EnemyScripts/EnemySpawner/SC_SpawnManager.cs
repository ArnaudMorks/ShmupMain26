using UnityEngine;

public class SC_SpawnManager : MonoBehaviour
{
    private SC_EnemySpawner enemySpawner;

    [SerializeField] private GameObject fireRatePowerup = null;
    [SerializeField] private GameObject bulletSpeedPowerup = null;
    [SerializeField] private GameObject currentPowerup = null;
    [SerializeField] private GameObject setCurrentPowerup = null;

    private int randomSetterBasePowerup;

    private bool powerUpRepeatOn = false;
    private bool randomBasePowerup = true;


    private void Start()
    {
        enemySpawner = FindObjectOfType<SC_EnemySpawner>();
    }


/*    private void CurrentRandomBasePowerup()
    {
        //currentPowerup = fireRatePowerup;
    }*/
    public void SpawnModeOn() { enemySpawner.RandomSpawnerOn = true; }
    public void SpawnModeOff() { enemySpawner.RandomSpawnerOn = false; }

    //Powerup Repeats
    private void PowerupRepeatCurrent()
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
        currentPowerup = null;
        enemySpawner.CurrentPowerup = currentPowerup;
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
        Invoke("StopRepeat", 50);
    }


}
