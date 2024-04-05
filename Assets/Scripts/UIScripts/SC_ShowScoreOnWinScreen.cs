using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_ShowScoreOnWinScreen : MonoBehaviour
{

    private int score;

    [SerializeField] private TextMeshProUGUI scoreText;

    private SC_ScoreAcrossScenes scoreAcrossScenes;


    void Start()
    {
        scoreAcrossScenes = FindObjectOfType<SC_ScoreAcrossScenes>();

        score = scoreAcrossScenes.CurrentScore;

        scoreText.text = "Score: " + score;
    }


    void Update()
    {
        
    }


}
