using UnityEngine;

public class SC_PoolPlayerBullets : MonoBehaviour
{
    [SerializeField] private int poolSize = 120;

    [SerializeField] private SC_MainBulletPlayer bulletPrefab = null;
    private SC_MainBulletPlayer[] poolBullets = null;

    private void Awake()
    {
        Transform poolTransform = transform;
        poolBullets = new SC_MainBulletPlayer[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            //Create new bullet
            SC_MainBulletPlayer newBullet = Instantiate<SC_MainBulletPlayer>(bulletPrefab, poolTransform);

            //Deactivate ite
            newBullet.gameObject.SetActive(false);

            poolBullets[i] = newBullet;
        }
    }


    public SC_MainBulletPlayer ActivateBullet(Vector3 position, float bulletSpeed)
    {
        SC_MainBulletPlayer availableBullet = null;

        for (int i = 0; i < poolBullets.Length; i++)
        {
            if (poolBullets[i].isActiveAndEnabled == false)
            {
                availableBullet = poolBullets[i];
                break;
            }
        }

        availableBullet.transform.position = position;      //zit in de bullet maar wordt uit de shooter gehaald

        availableBullet.SetSpeed(bulletSpeed);      //zit in de bullet

        availableBullet.gameObject.SetActive(true);
        return availableBullet;
    }

}
