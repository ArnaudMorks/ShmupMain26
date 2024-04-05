using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_VictoryScreen : MonoBehaviour
{
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private float _timeUntilShowScreen;
    private int thisRunScore;
    [SerializeField] private int currentScore;
    private SC_ScoreAcrossScenes scoreAcrossScenes;

    private void Start()
    {
        scoreAcrossScenes = FindObjectOfType<SC_ScoreAcrossScenes>();
    }

    public void ShowVictoryScreen()
    {
        if (_victoryScreen == null) { return; }

        //SetScoreInt(thisRunScore, currentScore);
        thisRunScore = ServiceLocator.Main.ScoreManager.GetScore();
        scoreAcrossScenes.CurrentScore = thisRunScore;
        StartCoroutine(ShowScreenCoroutine());
    }

    private IEnumerator ShowScreenCoroutine()
    {
        yield return new WaitForSeconds(_timeUntilShowScreen);
        
        Time.timeScale = 0;
        SceneManager.LoadScene("Winscene");
    }

/*    public void SetScoreInt(string thisRunScore, int currentScore)
    {
        currentScore = ServiceLocator.Main.ScoreManager.GetScore();
        PlayerPrefs.SetInt(thisRunScore, currentScore);
    }*/

}
