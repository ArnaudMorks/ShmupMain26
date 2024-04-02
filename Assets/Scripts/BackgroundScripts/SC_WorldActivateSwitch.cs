using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WorldActivateSwitch : MonoBehaviour
{
    [SerializeField] private float secondWorldActiavtion = 18.16f;
    [SerializeField] private float firstWorldDeactivation = -26;

    [SerializeField] private GameObject secondWorld;
    [SerializeField] private GameObject firstWorld;

    private bool secondWorldSetOn = false;

    private void FixedUpdate()
    {
        if (transform.position.z <= secondWorldActiavtion && secondWorldSetOn == false)
        {
            secondWorld.SetActive(true);
            secondWorldSetOn = true;
        }

        if (transform.position.z <= firstWorldDeactivation && isActiveAndEnabled)
        {
            firstWorld.SetActive(false);
        }

    }
}
