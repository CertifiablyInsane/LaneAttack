using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class InitialLoader : object
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void LoadMainMenu()
    {
        GameManager.gamePaused = false;
        if(SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
        {
            SceneManager.LoadScene(0);

        }
    }
}
