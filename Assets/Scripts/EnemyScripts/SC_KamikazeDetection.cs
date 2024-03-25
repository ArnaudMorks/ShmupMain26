using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_KamikazeDetection : MonoBehaviour
{
    private SC_KamikazeEnemy thisKamikazeEnemy;


    private void Start()
    {
        thisKamikazeEnemy = transform.parent.GetComponentInChildren<SC_KamikazeEnemy>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SC_EmptyDetectionReference>(out _) && thisKamikazeEnemy != null)
        {
            thisKamikazeEnemy.PlayerDetection(other.transform);
        }
    }
}
