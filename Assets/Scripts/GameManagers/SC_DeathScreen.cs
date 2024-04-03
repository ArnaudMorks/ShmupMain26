using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject _deathScreen;

    public void ShowDeathScreen()
    {
        if (_deathScreen == null) { return; }

        Time.timeScale = 0;
        _deathScreen.SetActive(true);
    }
}
