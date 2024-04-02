using UnityEngine;

public class SC_EnemyShooterPool : MonoBehaviour
{
    [SerializeField] private int poolSize = 120;

    [SerializeField] private SC_ShooterEnemy shooterPrefab = null;
    private SC_ShooterEnemy[] poolEnemies = null;

    [SerializeField] private float normalShooterSpeed = 4;     //moet zelfde zijn als in de GamePrefab

    private void Awake()
    {
        MakeEnemies();
    }


    public SC_ShooterEnemy ActivateShooterEnemy(Vector3 position, bool singleSetSpeed, GameObject normalPowerUp, float customSpeed)
    {
        SC_ShooterEnemy availableShooterEnemy = null;

        for (int i = 0; i < poolEnemies.Length; i++)
        {
            if (poolEnemies[i].isActiveAndEnabled == false)
            {
                availableShooterEnemy = poolEnemies[i];
                break;
            }
        }

        availableShooterEnemy.transform.position = position;      //zit in de bullet maar wordt uit de shooter gehaald

        availableShooterEnemy.SingleSetSpeed = singleSetSpeed;

        availableShooterEnemy.PowerupOnDeath(normalPowerUp);

        if (customSpeed != 0) { availableShooterEnemy.EnemySpeedBase = customSpeed; }
        else { availableShooterEnemy.EnemySpeedBase = normalShooterSpeed; }

        availableShooterEnemy.gameObject.SetActive(true);
        return availableShooterEnemy;
    }



    private void MakeEnemies()
    {
        Transform poolTransform = transform;
        poolEnemies = new SC_ShooterEnemy[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            //Create new bullet
            SC_ShooterEnemy newEnemy = Instantiate<SC_ShooterEnemy>(shooterPrefab, poolTransform);

            //Deactivate it
            newEnemy.gameObject.SetActive(false);

            poolEnemies[i] = newEnemy;
        }
    }

}
