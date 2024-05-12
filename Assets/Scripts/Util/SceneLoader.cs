using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public delegate void SceneEvent();
    public static SceneEvent OnStartLoadScene;
    public static SceneEvent OnSceneLoaded;

    public void LoadScene(int sceneIndex)
    {
        GameManager.UnPause();
        StartCoroutine(C_LoadScene(sceneIndex));
    }

    public void LoadScene(string name)
    {
        GameManager.UnPause();
        StartCoroutine(C_LoadScene(name));
    }
    public void LoadScene(Scene scene)
    {
        GameManager.UnPause();
        StartCoroutine(C_LoadScene(Enum.SceneToString(scene)));
    }
    public void ReloadScene()
    {
        GameManager.UnPause();
        StartCoroutine(C_LoadScene(GetScene()));
    }
    private IEnumerator C_LoadScene(int index)
    {
        OnStartLoadScene?.Invoke();
        var asyncLoad = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        OnSceneLoaded?.Invoke();
    }
    private IEnumerator C_LoadScene(string name)
    {
        OnStartLoadScene?.Invoke();
        var asyncLoad = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        OnSceneLoaded?.Invoke();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public int GetScene()
    {
        int n = SceneManager.GetActiveScene().buildIndex;
        return n;
    }
}
