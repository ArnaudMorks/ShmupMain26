using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_Music : MonoBehaviour
{
    public static SC_Music thisScript;

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
