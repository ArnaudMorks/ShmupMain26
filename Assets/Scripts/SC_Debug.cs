using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_Debug : MonoBehaviour
{
    private string currentLevel;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene(); //haalt huidige scene op
        currentLevel = currentScene.name;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(currentLevel);       //herlaad het huidige level
        }
    }
}
