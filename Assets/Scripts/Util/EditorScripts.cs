#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorScripts : EditorWindow
{
    [MenuItem("Play/Playtest _%g")]
    [System.Obsolete]
    public static void RunMainScene()
    {
        if (!EditorApplication.isPlaying)
        {
            string currentSceneName = EditorApplication.currentScene;
            File.WriteAllText(".lastScene", currentSceneName);
            EditorSceneManager.OpenScene("Assets/Scenes/Main Menu.unity");
            EditorApplication.isPlaying = true;
        }
        else
        {
            string lastScene = File.ReadAllText(".lastScene");
            EditorApplication.isPlaying = false;
            EditorSceneManager.OpenScene(lastScene);
        }
    }

    [MenuItem("Play/Return To Previous Scene _%h")]
    public static void ReturnToLastScene()
    {
        string lastScene = File.ReadAllText(".lastScene");
        EditorSceneManager.OpenScene(lastScene);
    }
}
#endif
 //helen has lost a total of 19476483756827 brain cells playing this game and cried a total of one time. 10/10 would mouse again :)