using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public MonoBehaviour[] scriptsToDisable;
    public PolygonCollider2D[] collidersToDisable;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        foreach (var script in scriptsToDisable)
        {
            script.enabled = true;
        }
        foreach (var col in collidersToDisable)
        {
            col.enabled = true;
        }
    }

    void Pause ()
    {
        Debug.Log("Game is Paused!");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        foreach (var script in scriptsToDisable)
        {
            script.enabled = false;
        }
        foreach (var col in collidersToDisable)
        {
            col.enabled = false;
        }
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        SceneManager.LoadScene("main menu");
    }

    public void Settings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
}
