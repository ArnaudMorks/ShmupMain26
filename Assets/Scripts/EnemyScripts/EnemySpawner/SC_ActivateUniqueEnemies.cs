using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ActivateUniqueEnemies : MonoBehaviour
{
    [SerializeField] private float enemySpawnLocation = 18.16f;     //zodat de enemy boven aan het scherm spawned

    [SerializeField] private GameObject enemies;   //alle enemies die een in één child object zitten van dit gameobject
    private bool enemiesEnabled = false;
    [SerializeField] private bool hasDisableTimer;
    [SerializeField] private float timeUntilDisable = 40f;


    private void FixedUpdate()
    {
        if (transform.position.z <= enemySpawnLocation && enemiesEnabled == false)
        {
            enemies.SetActive(true);
            if (hasDisableTimer)
            {
                Invoke("DisableThis", timeUntilDisable);
            }
            else if (transform.position.z <= -24) { gameObject.SetActive(false); }

            enemiesEnabled = true;
        }

    }

    private void DisableThis()
    {
        gameObject.SetActive(false);
    }

}
