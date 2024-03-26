using UnityEngine;

public class SC_EnemyPool : MonoBehaviour
{
    [SerializeField] private int poolSize = 120;

    [SerializeField] private SC_ShooterEnemy shooterPrefab = null;
    private SC_ShooterEnemy[] poolEnemies = null;


    private void Awake()
    {
        MakeEnemies();
    }


    public SC_ShooterEnemy ActivateShooterEnemy(Vector3 position)
    {
        SC_ShooterEnemy availableShooterEnemies = null;

        for (int i = 0; i < poolEnemies.Length; i++)
        {
            if (poolEnemies[i].isActiveAndEnabled == false)
            {
                availableShooterEnemies = poolEnemies[i];
                break;
            }
        }

        availableShooterEnemies.transform.position = position;      //zit in de bullet maar wordt uit de shooter gehaald

        availableShooterEnemies.gameObject.SetActive(true);
        return availableShooterEnemies;
    }



    private void MakeEnemies()
    {
        Transform poolTransform = transform;
        poolEnemies = new SC_ShooterEnemy[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            //Create new bullet
            SC_ShooterEnemy newBullet = Instantiate<SC_ShooterEnemy>(shooterPrefab, poolTransform);

            //Deactivate it
            newBullet.gameObject.SetActive(false);

            poolEnemies[i] = newBullet;
        }
    }

}
