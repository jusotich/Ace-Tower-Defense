using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{
   public void OpenScene()
   {
        SceneManager.LoadScene("Map 1");
   }
}
