using UnityEngine;

public class SC_EnemySlugPool : MonoBehaviour
{
    [SerializeField] private int poolSize = 120;

    [SerializeField] private SC_SlugEnemy shooterPrefab = null;
    private SC_SlugEnemy[] poolEnemies = null;

    [SerializeField] private float normalSlugSpeed = 300;     //moet zelfde zijn als in de GamePrefab

    private void Awake()
    {
        MakeEnemies();
    }


    public SC_SlugEnemy ActivateSlugEnemy(Vector3 position, Quaternion rotation, bool singleSetSpeed, GameObject normalPowerUp, float customSpeed, float froggerMode)      //als "customSpeed" 0 is dan heeft die de orginele snelheid
    {
        SC_SlugEnemy availableSlugEnemy = null;

        for (int i = 0; i < poolEnemies.Length; i++)
        {
            if (poolEnemies[i].isActiveAndEnabled == false)
            {
                availableSlugEnemy = poolEnemies[i];
                break;
            }
        }

        availableSlugEnemy.transform.position = position;      //zit in de bullet maar wordt uit de shooter gehaald

        availableSlugEnemy.transform.localRotation = rotation;

        availableSlugEnemy.SingleSetSpeed = singleSetSpeed;

        availableSlugEnemy.PowerupOnDeath(normalPowerUp);

        if (customSpeed != 0) { availableSlugEnemy.EnemySpeedBase = customSpeed;}
        else { availableSlugEnemy.EnemySpeedBase = normalSlugSpeed; }

        availableSlugEnemy.SlugFroggerMode = froggerMode;

        availableSlugEnemy.gameObject.SetActive(true);
        return availableSlugEnemy;
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
