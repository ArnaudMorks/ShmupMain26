using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SC_HealthUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _healthIcons;

    public void UpdateHealthUI(int health)
    {
        for(int i = 0; i < _healthIcons.Length; i++)
        {
            _healthIcons[i].SetActive(i < health);
        }
    }
}
