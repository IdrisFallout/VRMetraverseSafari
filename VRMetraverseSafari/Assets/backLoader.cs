using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backLoader : MonoBehaviour
{
    public string backScene;
    public string mainScene = "NairobiNationalParkScene";

    public void LoadScene(){
        SceneManager.LoadScene(backScene);
    }

    public void LoadNairobiScene()
    {
        SceneManager.LoadScene(mainScene);
    }
}
