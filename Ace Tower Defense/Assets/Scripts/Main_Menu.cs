
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_Menu : MonoBehaviour
{
    public int sceneNum;

    public void OpenScene()
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
