using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SlugMirrorScript : MonoBehaviour
{
    private int mirrorModel;


    private void Start()
    {
        MirrorModel();
    }

    private void OnEnable()
    {
        MirrorModel();
    }

    private void MirrorModel()
    {
        mirrorModel = Random.Range(0, 2);           //willekeurig of de model gespiegeld is

        if (mirrorModel == 1) { transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); }
    }
}
