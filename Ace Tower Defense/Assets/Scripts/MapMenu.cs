
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{

    public int sceneNum;

   public void OpenScene()
   {
        SceneManager.LoadScene(sceneNum);
   }
}
