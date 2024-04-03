using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_VictoryScreen : MonoBehaviour
{
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private float _timeUntilShowScreen;

    public void ShowVictoryScreen()
    {
        if (_victoryScreen == null) { return; }

        StartCoroutine(ShowScreenCoroutine());
    }

    private IEnumerator ShowScreenCoroutine()
    {
        yield return new WaitForSeconds(_timeUntilShowScreen);
        
        Time.timeScale = 0;
        _victoryScreen.SetActive(true);
    }
}
