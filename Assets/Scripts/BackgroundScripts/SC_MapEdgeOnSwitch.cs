using UnityEngine;

public class SC_MapEdgeOnSwitch : MonoBehaviour
{
    private SC_Player player;
    [SerializeField] float setLocation = 18.16f;

    [SerializeField] private bool setToLight;
    [SerializeField] private bool setToMedium;

    void Start()
    {
        player = FindObjectOfType<SC_Player>();
    }


    private void FixedUpdate()
    {
        if (transform.position.z <= setLocation && isActiveAndEnabled)
        {
            if (setToLight) { player.SetMapWidthLight(); }
            else if (setToMedium) { player.SetMapWidthMedium(); }
            gameObject.SetActive(false);
        }
    }
}
