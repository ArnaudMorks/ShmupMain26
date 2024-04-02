using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_ButtonManager : MonoBehaviour
{
    public void SwitchScene(int sceneIndex)
    {
        // Prevent the game from staying at timescale 0 from being paused
        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
