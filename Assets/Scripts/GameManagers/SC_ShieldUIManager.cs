using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SC_ShieldUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _shieldIcons;

    public void UpdateShieldUI(int shield)
    {
        for (int i = 0; i < _shieldIcons.Length; i++)
        {
            _shieldIcons[i].SetActive(i < shield);
        }
    }
}
