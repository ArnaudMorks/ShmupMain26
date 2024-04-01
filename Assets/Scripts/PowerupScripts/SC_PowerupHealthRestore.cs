using UnityEngine;

public class SC_PowerupHealthRestore : SC_PowerupBase
{
    private SC_Player playerMovement;

    protected override void Start()
    {
        base.Start();
        playerMovement = FindObjectOfType<SC_Player>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SC_PlayerHealth>(out var playerHealth))
        {
            playerHealth.RestoreHealth();
            Destroy(gameObject);
        }
    }

}
