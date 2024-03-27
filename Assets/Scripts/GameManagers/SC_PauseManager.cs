using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _pauseUIObjects;
    private bool _isPaused = false;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOrUnpause();
        }
    }

    public void PauseOrUnpause()
    {
        _isPaused = !_isPaused;

        if(_isPaused)
        {
            Pause();
        }
        else
        {
            UnPause();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;

        foreach(GameObject pauseObject in _pauseUIObjects)
        {
            pauseObject.SetActive(true);
        }
    }

    private void UnPause()
    {
        Time.timeScale = 1f;

        foreach (GameObject pauseObject in _pauseUIObjects)
        {
            pauseObject.SetActive(false);
        }
    }
}
