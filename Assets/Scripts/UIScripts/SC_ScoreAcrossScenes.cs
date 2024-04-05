using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_ScoreAcrossScenes : MonoBehaviour
{
    public static SC_ScoreAcrossScenes thisScript;
    private int currentScore;
    public int CurrentScore
    {
        get { return currentScore; }
        set { currentScore = value; }
    }

    private void Awake()
    {
        if (thisScript != null)
        {
            Destroy(gameObject);
        }
        else
        {
            thisScript = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

}
