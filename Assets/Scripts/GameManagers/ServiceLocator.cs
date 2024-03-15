using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator main { get; private set; }

    public SC_ScoreManager scoreManager { get; private set; }

    private void Awake()
    {
        if(main != null && main != this)
        {
            // Destroy this instance of ServiceLocator if another instance exists as there can be only one instance
            Destroy(this);
            return;
        }

        main = this;

        scoreManager = GetComponentInChildren<SC_ScoreManager>();
    }
}
