using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Main { get; private set; }

    public SC_ScoreManager ScoreManager { get; private set; }
    public SC_ScoreUIManager ScoreUIManager { get; private set; }
    public SC_HealthUIManager HealthUIManager { get; private set; }

    private void Awake()
    {
        if(Main != null && Main != this)
        {
            // Destroy this instance of ServiceLocator if another instance exists as there can be only one instance
            Destroy(this);
            return;
        }

        Main = this;

        ScoreManager = GetComponentInChildren<SC_ScoreManager>();
        ScoreUIManager = GetComponentInChildren<SC_ScoreUIManager>();
        HealthUIManager = GetComponentInChildren<SC_HealthUIManager>();
    }
}
