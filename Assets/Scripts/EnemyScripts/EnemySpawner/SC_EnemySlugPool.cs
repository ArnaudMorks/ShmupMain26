using UnityEngine;

public class SC_EnemySlugPool : MonoBehaviour
{
    [SerializeField] private int poolSize = 120;

    [SerializeField] private SC_SlugEnemy shooterPrefab = null;
    private SC_SlugEnemy[] poolEnemies = null;


    private void Awake()
    {
        MakeEnemies();
    }


    public SC_SlugEnemy ActivateSlugEnemy(Vector3 position)
    {
        SC_SlugEnemy availableShooterEnemies = null;

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
        poolEnemies = new SC_SlugEnemy[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            //Create new bullet
            SC_SlugEnemy newEnemy = Instantiate<SC_SlugEnemy>(shooterPrefab, poolTransform);

            //Deactivate it
            newEnemy.gameObject.SetActive(false);

            poolEnemies[i] = newEnemy;
        }
    }

}
