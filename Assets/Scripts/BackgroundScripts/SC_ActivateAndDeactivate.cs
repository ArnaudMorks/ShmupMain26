using UnityEngine;

public class SC_ActivateAndDeactivate : MonoBehaviour       //beweegt met de map mee
{
    [SerializeField] private float spawnLocation = 18.16f;
    [SerializeField] private float despawnLocation = -20;
    [SerializeField] private GameObject toEnable;
    
    private void FixedUpdate()
    {
        if (transform.position.z <= spawnLocation)
        {
            toEnable.SetActive(true);
        }

        if (transform.position.z <= despawnLocation && isActiveAndEnabled)
        {
            gameObject.SetActive(false);
        }
    }
}
