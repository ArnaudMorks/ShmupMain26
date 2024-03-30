using UnityEngine;

public class SC_ModelMovementPlayer : MonoBehaviour         //werkt niet goed genoeg; nu niet gebruiken
{

    [SerializeField] private float movementX;
    private bool rotatingRight = false;
    private bool rotatingLeft = false;
    private bool notRotating = true;

    [SerializeField] private Transform from;
    [SerializeField] private Transform to;
    [SerializeField] private Transform right;
    [SerializeField] private Transform left;        //z = 26.8
    [SerializeField] private Transform middle;

    [SerializeField] private float speed;
    private float timeCount = 0.0f;


    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        /*        float targetFov = sprintMode ? sprintingFov : defaultFov;       //links is true, rechts is false

                if (Mathf.Abs(Camera.main.fieldOfView - targetFov) >= 0.1f)
                {
                    Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFov, fovLerpTimer * Time.deltaTime);
                }*/

        if (movementX == 0)
        {
            if (notRotating == false)
            {
                timeCount = 0;
                notRotating = true;
            }

            if (rotatingRight == true)
            {
                transform.rotation = Quaternion.Lerp(right.rotation, middle.rotation, timeCount * speed);
            }
            else if (rotatingLeft == true)
            {
                transform.rotation = Quaternion.Lerp(left.rotation, middle.rotation, timeCount * speed);
            }

            timeCount = timeCount + Time.deltaTime;
        }
        else if (movementX == 1)
        {
            if (rotatingRight == false)
            {
                timeCount = 0;
                notRotating = false;
                rotatingLeft = false;
                from.rotation = gameObject.transform.rotation;
                rotatingRight = true;
            }

            transform.rotation = Quaternion.Lerp(from.rotation, right.rotation, timeCount * speed);
            timeCount = timeCount + Time.deltaTime;
        }

    }


}
