using UnityEngine;

public class SC_PowerupSuperShooterMode : SC_PowerupBase
{
    private SC_Player playerMovement;

    protected override void Start()
    {
        base.Start();
        playerMovement = FindObjectOfType<SC_Player>();
    }


    private void OnTriggerEnter(Collider other)
    {
        SC_PlayerHealth playerHealth = other.gameObject.GetComponent<SC_PlayerHealth>();

        if (playerHealth != null)
        {
            playerMovement.SuperShooterModeSpeed();
            Destroy(gameObject);
        }
    }

}