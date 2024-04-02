using UnityEngine;

public class SC_PoolEnemyBullets : MonoBehaviour
{
    [SerializeField] private int poolSize = 120;

    [SerializeField] private SC_BulletStandard bulletPrefab = null;
    private SC_BulletStandard[] poolBullets = null;

    private void Awake()
    {
        Transform poolTransform = transform;
        poolBullets = new SC_BulletStandard[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            //Create new bullet
            SC_BulletStandard newEnemyBullet = Instantiate<SC_BulletStandard>(bulletPrefab, poolTransform);

            //Deactivate it
            newEnemyBullet.gameObject.SetActive(false);

            poolBullets[i] = newEnemyBullet;
        }
    }


    public SC_BulletStandard ActivateEnemyBullet(Vector3 position, Quaternion rotation)
    {
        SC_BulletStandard availableEnemyBullet = null;

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

        availableEnemyBullet.transform.localRotation = rotation;

        // availableBullet.SetEnemyBulletSpeed(bulletSpeed);      //zit in de bullet

        availableEnemyBullet.gameObject.SetActive(true);
        return availableEnemyBullet;
    }

}
