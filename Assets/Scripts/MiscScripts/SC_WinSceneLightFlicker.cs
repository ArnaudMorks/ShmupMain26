using UnityEngine;

public class SC_WinSceneLightFlicker : MonoBehaviour
{
    [SerializeField] private GameObject lights;
    [SerializeField] private float randomLightFlickerTime;
    [SerializeField] private float randomLightOnFlickerTime;
    private float effectTimer;

    private bool lightsOn = true;

    private bool lightOffIsRandomized = true;
    private bool lightOnIsRandomized = false;


    private int longOnRandom = 1;       //0 is true
    private int longOffRandom = 1;

    private float randomLongOnTime;
    private float randomLongOffTime;
    private float longEffectTimer;


    private void Start()
    {
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        if (lightOffIsRandomized == false)
        {
            randomLightFlickerTime = Random.Range(0, 0.4f);
            lightOffIsRandomized = true;
        }

        if (lightOnIsRandomized == false)
        {
            randomLightOnFlickerTime = Random.Range(0, 0.2f);
            lightOnIsRandomized = true;
        }


        if (lightsOn == true && longOnRandom != 0)
        {
            if (effectTimer < randomLightFlickerTime)
            {
                effectTimer += Time.fixedDeltaTime;
            }
            else
            {
                lights.SetActive(true);     //zijn nu omgedraaid
                effectTimer = 0;
                lightOffIsRandomized = false;
                longOffRandom = Random.Range(0, 4);
                if (longOffRandom == 0){ randomLongOffTime = Random.Range(1, 7); }
                lightsOn = false;
            }
        }
        else if (longOffRandom != 0)
        {
            if (effectTimer < randomLightOnFlickerTime)
            {
                effectTimer += Time.fixedDeltaTime;
            }
            else
            {
                lights.SetActive(false);     //zijn nu omgedraaid
                effectTimer = 0;
                lightOnIsRandomized = false;
                longOnRandom = Random.Range(0, 7);
                if (longOnRandom == 0) { randomLongOnTime = Random.Range(1, 3); }
                lightsOn = true;
            }
        }


        //Long
        if (longOnRandom == 0 && lightsOn == true)
        {
            if (longEffectTimer < randomLongOnTime)
            {
                longEffectTimer += Time.fixedDeltaTime;
            }
            else
            {
                longEffectTimer = 0;
                longOnRandom = Random.Range(0, 1);
                if (longOnRandom == 0) { randomLongOnTime = Random.Range(1, 3); }
            }
        }
        else if (longOffRandom == 0 && lightsOn == false)
        {
            if (longEffectTimer < randomLongOffTime)
            {
                longEffectTimer += Time.fixedDeltaTime;
            }
            else
            {
                longEffectTimer = 0;
                longOffRandom = Random.Range(0, 2);
                if (longOffRandom == 0) { randomLongOffTime = Random.Range(1, 3); }
            }
        }



    }


}
