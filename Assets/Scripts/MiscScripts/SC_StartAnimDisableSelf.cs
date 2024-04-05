using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_StartAnimDisableSelf : MonoBehaviour
{
    [SerializeField] private GameObject normalPlayerModel;//

    void Start()
    {
        disableSelfTimer();
    }

    private void disableSelfTimer()
        {
        Invoke("disableSelf", 2.5f);
        }

    private void disableSelf()
    {
        normalPlayerModel.SetActive(true);
        gameObject.SetActive(false);
    }

}
