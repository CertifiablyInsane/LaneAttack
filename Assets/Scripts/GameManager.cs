using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    public static bool gamePaused = false;
    public static SaveInfo saveInfo { get; private set; }
    public static LevelData currentLevel { get; private set; }
    // Events
    public delegate void SaveEvent(string username);
    public static event SaveEvent OnChangedUser;
    public static string SAVEFILE_DATAPATH { get; private set; }
    // Savefile Filename changes based on current file
    public static string PERSISTENT_DATAPATH { get; private set; }
    public static string PERSISTENT_FILENAME {  get; private set; }
    private new void Awake()
    {
        SAVEFILE_DATAPATH = Path.Combine(Application.persistentDataPath, "Resources");
        PERSISTENT_DATAPATH = Path.Combine(Application.persistentDataPath, "Resources", "Persistent");
        PERSISTENT_FILENAME = JsonFilename("PersistentInfo");
        if (!Directory.Exists(SAVEFILE_DATAPATH))
            Directory.CreateDirectory(SAVEFILE_DATAPATH);
        if (!Directory.Exists(PERSISTENT_DATAPATH)) 
            Directory.CreateDirectory(PERSISTENT_DATAPATH);
        base.Awake();
    }
    private void Start()
    {
        LoadPersistentInfo();
        Physics2D.queriesHitTriggers = true;
    }
    public static void Pause()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }
    public static void UnPause()
    {
        Time.timeScale = 1;
        gamePaused = false;
    }
    public static void LoadLevel(LevelData level)
    {
        print("Loading a Level");
        currentLevel = level;
        SceneLoader.Instance.LoadScene("Level");
    }
    public static void CreateNewFile(string username)
    {
        SaveInfo newSave = new()
        {
            username = username,
            level = 1,
            points = 0,
        };
        string json = JsonUtility.ToJson(newSave);
        string fileName = JsonFilename(username);
        File.WriteAllText(Path.Combine(SAVEFILE_DATAPATH, fileName), json);
        saveInfo = newSave;
        OnChangedUser?.Invoke(username);
        Debug.Log("Created new Save File '" + username + ".json'");
    }
    public static void Save()
    {
        // Save Persistent Info
        PersistentInfo info = new()
        {
            currentUsername = saveInfo.username,
        };
        string json = JsonUtility.ToJson(info);
        File.WriteAllText(Path.Combine(PERSISTENT_DATAPATH, PERSISTENT_FILENAME), json);
        // Save File
        json = JsonUtility.ToJson(saveInfo);
        string sFileName = JsonFilename(saveInfo.username);
        File.WriteAllText(Path.Combine(SAVEFILE_DATAPATH, sFileName), json);
        Debug.Log("Saved File '" + saveInfo.username + ".json'");
    }
    public static void Load(string username)
    {
        try
        {
            string fileName = JsonFilename(username);
            string json = File.ReadAllText(Path.Combine(SAVEFILE_DATAPATH, fileName));
            SaveInfo readSave = JsonUtility.FromJson<SaveInfo>(json);
            if( readSave != null )
            {
                saveInfo = readSave;
                OnChangedUser?.Invoke(username);
                Debug.Log("Successfully Loaded File '" + username + ".json'");
                return;
            }
            else
            {
                Debug.Log("Could not parse JSON from File '" + username + ".json'");
                return;
            }
        }
        catch
        {
            Debug.LogError("Failed to read file '" + username + ".json'");
            return;
        }

    }
    public static void LoadPersistentInfo()
    {
        Debug.Log("Loading Persistent Info");
        try
        {
            string json = File.ReadAllText(Path.Combine(PERSISTENT_DATAPATH, PERSISTENT_FILENAME));
            PersistentInfo readInfo = JsonUtility.FromJson<PersistentInfo>(json);
            Load(readInfo.currentUsername);
        }
        catch
        {
            Debug.LogWarning("Could not find PersistentInfo.json! Creating a default one.");
            // Check to see if a save file exists
            var dInfo = new DirectoryInfo(SAVEFILE_DATAPATH);
            FileInfo[] files = dInfo.GetFiles("*.json");
            if (files.Length > 0)
            {
                string name = files[0].Name.Replace(".json", "");
                Load(name);
            }
            else
            {
                // No save files exist. Create a default one.
                CreateNewFile("Default");
            }
            PersistentInfo pInfo = new()
            {
                currentUsername = saveInfo.username,
            };
            string json = JsonUtility.ToJson(pInfo);
            File.WriteAllText(Path.Combine(PERSISTENT_DATAPATH, PERSISTENT_FILENAME), json);
        }
    }
    private static string JsonFilename(string file)
    {
        return file + ".json";
    }
    public static string[] GetAllUsernames()
    {
        var dInfo = new DirectoryInfo(SAVEFILE_DATAPATH);
        FileInfo[] files = dInfo.GetFiles("*.json");
        string[] usernames = new string[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            string json = File.ReadAllText(Path.Combine(SAVEFILE_DATAPATH, files[i].Name));
            SaveInfo readSave = JsonUtility.FromJson<SaveInfo>(json);
            if (readSave != null)
            {
                usernames[i] = readSave.username;
            }
            else
            {
                Debug.LogError("Failed to read file " + files[i].Name);
            }
        }
        return usernames;
    }
}
