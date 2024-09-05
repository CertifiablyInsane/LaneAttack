#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EditorScripts : EditorWindow
{
    [MenuItem("Util/Playtest _%g")]
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

    [MenuItem("Util/Return To Previous Scene _%h")]
    public static void ReturnToLastScene()
    {
        string lastScene = File.ReadAllText(".lastScene");
        EditorSceneManager.OpenScene(lastScene);
    }

    [MenuItem("Util/Wipe Game Memory")]
    public static void WipeMemory()
    {
        string path = Path.Combine(Application.dataPath, "Resources");
        if (Directory.Exists(path))
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                File.Delete(file);
            }
            AssetDatabase.Refresh();
        }
    }
}
#endif
