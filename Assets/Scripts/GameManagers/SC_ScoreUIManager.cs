using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SC_ScoreUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        UpdateScoreUI(0);
    }

    public void UpdateScoreUI(int score)
    {
        _scoreText.text = score.ToString("0000");
    }
}
