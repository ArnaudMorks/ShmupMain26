using UnityEngine;

public class SC_EnemyKamikazePool : MonoBehaviour
{
    [SerializeField] private int poolSize = 120;

    [SerializeField] private SC_KamikazeEnemy shooterPrefab = null;
    private SC_KamikazeEnemy[] poolEnemies = null;


    private void Awake()
    {
        MakeEnemies();
    }


    public SC_KamikazeEnemy ActivateKamikazeEnemy(Vector3 position)
    {
        SC_KamikazeEnemy availableShooterEnemies = null;

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
        poolEnemies = new SC_KamikazeEnemy[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            //Create new bullet
            SC_KamikazeEnemy newEnemy = Instantiate<SC_KamikazeEnemy>(shooterPrefab, poolTransform);

            //Deactivate it
            newEnemy.gameObject.SetActive(false);

            poolEnemies[i] = newEnemy;
        }
    }

}
