using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUpKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        } 
    }

    void resume()
    {

    }
    void Pause ()
    {
        
    }
}
