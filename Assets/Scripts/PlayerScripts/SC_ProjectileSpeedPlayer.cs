using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ProjectileSpeedPlayer : MonoBehaviour               //zit op een (verdere) empty "Game Object" en NIET OP DE SPELER
{
    [SerializeField] private float minPlayerBaseBulletSpeed;
    public float MinPlayerBaseBulletSpeed
    {
        get { return minPlayerBaseBulletSpeed; }
    }

    [SerializeField] private float currentPlayerBaseBulletSpeed;
    public float CurrentPlayerBaseBulletSpeed
    {
        get { return currentPlayerBaseBulletSpeed; }
        set { currentPlayerBaseBulletSpeed = value; }       //wordt aangepast door de "SC_PowerupBulletSpeed"
    }

    [SerializeField] private float maxPlayerBaseBulletSpeed;
    public float MaxPlayerBaseBulletSpeed
    {
        get { return maxPlayerBaseBulletSpeed; }
    }


    private void Start()
    {
        currentPlayerBaseBulletSpeed = minPlayerBaseBulletSpeed;        //bij het begin is dit hetzelfde
    }


}
