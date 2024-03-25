using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PowerupUI : MonoBehaviour
{
    [SerializeField] private GameObject normalModeText;
    [SerializeField] private GameObject powerupModeText;


    public void NormalModeUI()
    {
        normalModeText.SetActive(true);
        powerupModeText.SetActive(false);
    }

    public void PowerupModeUI()
    {
        normalModeText.SetActive(false);
        powerupModeText.SetActive(true);
    }

}
