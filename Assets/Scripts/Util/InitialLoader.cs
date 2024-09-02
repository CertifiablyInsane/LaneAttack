using UnityEngine;
using UnityEngine.SceneManagement;

public static class InitialLoader : object
{
    static readonly string persistentSceneName = "Persistent";
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void InitialLoad()
    {
        GameManager.gamePaused = false;
        if(SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
        {
            SceneManager.LoadScene(0);

        }
        if(SceneManager.GetSceneByName(persistentSceneName) != null)
        {
            SceneManager.LoadScene(persistentSceneName, LoadSceneMode.Additive);
        }
    }
}
