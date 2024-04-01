using UnityEngine;

public class SC_MapEdgeOnSwitch : MonoBehaviour
{
    private SC_Player player;
    [SerializeField] float setLocation = 18.16f;


    void Start()
    {
        player = FindObjectOfType<SC_Player>();
    }


    private void FixedUpdate()
    {
        if (transform.position.z <= setLocation && isActiveAndEnabled)
        {
            player.SetMapWidthLight();
            gameObject.SetActive(false);
        }
    }
}
