using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject _deathScreen;
    private bool playerIsDead = false;
    public bool PlayerIsDead
    {
        get { return playerIsDead; }
    }

    public void ShowDeathScreen()
    {
        if (_deathScreen == null) { return; }

        Time.timeScale = 0;
        playerIsDead = true;
        _deathScreen.SetActive(true);
    }
}
