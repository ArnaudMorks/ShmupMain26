using UnityEngine;

public class SC_PoolBossBullets : MonoBehaviour
{
    [SerializeField] private int poolSize = 120;

    [SerializeField] private SC_HomingMissile bulletPrefab = null;
    private SC_HomingMissile[] poolBullets = null;

    private void Awake()
    {
        Transform poolTransform = transform;
        poolBullets = new SC_HomingMissile[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            //Create new bullet
            SC_HomingMissile newEnemyBullet = Instantiate<SC_HomingMissile>(bulletPrefab, poolTransform);

            //Deactivate it
            newEnemyBullet.gameObject.SetActive(false);

            poolBullets[i] = newEnemyBullet;
        }
    }


    public SC_HomingMissile ActivateBossBullet(Vector3 position, Quaternion rotation)
    {
        SC_HomingMissile availableEnemyBullet = null;

        for (int i = 0; i < poolBullets.Length; i++)
        {
            if (poolBullets[i].isActiveAndEnabled == false)
            {
                availableEnemyBullet = poolBullets[i];
                break;
            }
        }

        //if (position == null) { position = transform.position; }

        availableEnemyBullet.transform.position = position;      //zit in de bullet maar wordt uit de shooter gehaald

        availableEnemyBullet.transform.rotation = rotation;      //zit in de bullet

        availableEnemyBullet.gameObject.SetActive(true);
        return availableEnemyBullet;
    }

}
