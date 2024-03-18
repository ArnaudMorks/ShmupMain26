using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ScoreManager : MonoBehaviour
{
    private int _score;

    public void ModifyScore(int amount)
    {
        _score += amount;
        ServiceLocator.Main.ScoreUIManager.UpdateScoreUI(_score);
    }

    public int GetScore()
    {
        return _score;
    }
}
