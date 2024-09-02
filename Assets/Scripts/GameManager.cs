using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    public static bool gamePaused = false;
    public static SaveInfo SaveInfo { get; private set; }
    public static LevelData CurrentLevel { get; private set; }
    // Events
    public delegate void SaveEvent(string username);
    public static event SaveEvent OnChangedUser;
    public delegate void PointsEvent(int amount);
    public static event PointsEvent OnPointsChanged;
    public delegate void LevelEvent(int level);
    public static event LevelEvent OnLevelIncremented;
    public delegate void ProtagonistUpgradeEvent(ProtagonistData protagonist, int level);
    public static event ProtagonistUpgradeEvent OnPlayerUpgraded;
    public delegate void UnitUpgradeEvent(UnitData unit, int level);
    public static event UnitUpgradeEvent OnUnitUpgraded;

    public static string SAVEFILE_DATAPATH { get; private set; }
    // Savefile Filename changes based on current file
    public static string PERSISTENT_DATAPATH { get; private set; }
    public static string PERSISTENT_FILENAME {  get; private set; }

    [Header("Protagonist Data")]
    [SerializeField] private ProtagonistData _robotProtagonistData;
    [SerializeField] private ProtagonistData _martianProtagonistData;

    [Header("Robot Unit Data")]
    [SerializeField] private UnitData _robotBasicUnitData;
    [SerializeField] private UnitData _robotSwarmUnitData;
    [SerializeField] private UnitData _robotRangedUnitData;
    [SerializeField] private UnitData _robotKnockbackUnitData;
    [SerializeField] private UnitData _robotSupportUnitData;

    [Header("Martian Unit Data")]
    [SerializeField] private UnitData _martianBasicUnitData;
    [SerializeField] private UnitData _martianHeavyUnitData;
    [SerializeField] private UnitData _martianRangedUnitData;
    [SerializeField] private UnitData _martianDodgerUnitData;
    [SerializeField] private UnitData _martianSupportUnitData;

    #region UnitDataHelper Initialization
    private static UnitDataHelper _unitData;
    public static UnitDataHelper UnitData
    {
        get
        {
            _unitData ??= AssignUnitData();
            return _unitData;
        }
    }
    private static UnitDataHelper AssignUnitData()
    {
        return new UnitDataHelper()
        {
            robotBasicUnitData = Instance._robotBasicUnitData,
            robotSwarmUnitData = Instance._robotSwarmUnitData,
            robotRangedUnitData = Instance._robotRangedUnitData,
            robotKnockbackUnitData = Instance._robotKnockbackUnitData,
            robotSupportUnitData = Instance._robotSupportUnitData,

            martianBasicUnitData = Instance._martianBasicUnitData,
            martianHeavyUnitData = Instance._martianHeavyUnitData,
            martianRangedUnitData = Instance._martianRangedUnitData,
            martianDodgerUnitData = Instance._martianDodgerUnitData,
            martianSupportUnitData = Instance._martianSupportUnitData
        };
    }
    #endregion
    #region ProtagonistDataHelper Initialization
    private static ProtagonistDataHelper _protagonistData;
    public static ProtagonistDataHelper ProtagonistData
    {
        get
        {
            _protagonistData ??= AssignProtagonistData();
            return _protagonistData;
        }
    }
    private static ProtagonistDataHelper AssignProtagonistData()
    {
        return new ProtagonistDataHelper()
        {
            robotProtagonistData = Instance._robotProtagonistData,
            martianProtagonistData = Instance._martianProtagonistData
        };
    }
    #endregion



    private new void Awake()
    {
#if UNITY_EDITOR

        Debug.Log("Running on Unity Editor");
        SAVEFILE_DATAPATH = Path.Combine(Application.dataPath, "Resources");
        PERSISTENT_DATAPATH = Path.Combine(Application.dataPath, "Resources", "Persistent");

#elif UNITY_ANDROID

        Debug.Log("Running on Unity Android");
        SAVEFILE_DATAPATH = Path.Combine(Application.persistentDataPath, "Resources");
        PERSISTENT_DATAPATH = Path.Combine(Application.persistentDataPath, "Resources", "Persistent");

#else

        Debug.LogWarning("No Directive in place for this Build Type!");

#endif

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
        CurrentLevel = level;
        SceneLoader.Instance.LoadScene(Scene.LEVEL);
    }
    public static void CreateNewFile(string username)
    {
        SaveInfo newSave = new()
        {
            username = username,
            level = 1,
        };
        string json = JsonUtility.ToJson(newSave);
        string fileName = JsonFilename(username);
        File.WriteAllText(Path.Combine(SAVEFILE_DATAPATH, fileName), json);
        SaveInfo = newSave;
        OnChangedUser?.Invoke(username);
        Debug.Log("Created new Save File '" + username + ".json'");
    }
    public static void Save()
    {
        // Save Persistent Info
        PersistentInfo info = new()
        {
            currentUsername = SaveInfo.username,
        };
        string json = JsonUtility.ToJson(info);
        File.WriteAllText(Path.Combine(PERSISTENT_DATAPATH, PERSISTENT_FILENAME), json);
        // Save File
        json = JsonUtility.ToJson(SaveInfo);
        string sFileName = JsonFilename(SaveInfo.username);
        File.WriteAllText(Path.Combine(SAVEFILE_DATAPATH, sFileName), json);
        Debug.Log("Saved File '" + SaveInfo.username + ".json'");
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
                SaveInfo = readSave;
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
                currentUsername = SaveInfo.username,
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
    public static void AddPoints(int amount)
    {
        SaveInfo.points += amount;
        OnPointsChanged?.Invoke(SaveInfo.points);
    }
    public static void IncrementLevel()
    {
        SaveInfo.level++;
        OnLevelIncremented?.Invoke(SaveInfo.level);
    }
    public static void Upgrade(ProtagonistData protag)
    {
        switch (protag)
        {
            case var _ when protag.Equals(ProtagonistData.robotProtagonistData):
                SaveInfo.robotPlayerLevel++;
                OnPlayerUpgraded?.Invoke(protag, SaveInfo.robotPlayerLevel);
                break;
            case var _ when protag.Equals(ProtagonistData.martianProtagonistData):
                SaveInfo.martianPlayerLevel++;
                OnPlayerUpgraded?.Invoke(protag, SaveInfo.martianPlayerLevel);
                break;
            default:
                throw new NotImplementedException();
        }
    }
    public static void Upgrade(UnitData unit)
    {
        switch (unit)
        {
            // ROBOT UNITS
            case var _ when unit.Equals(UnitData.robotBasicUnitData):
                SaveInfo.robotBasicUnitLevel++;
                OnUnitUpgraded?.Invoke(unit, SaveInfo.robotBasicUnitLevel);
                break;
            case var _ when unit.Equals(UnitData.robotSwarmUnitData):
                SaveInfo.robotSwarmUnitLevel++;
                OnUnitUpgraded?.Invoke(unit, SaveInfo.robotSwarmUnitLevel);
                break;
            case var _ when unit.Equals(UnitData.robotRangedUnitData):
                SaveInfo.robotRangedUnitLevel++;
                OnUnitUpgraded?.Invoke(unit, SaveInfo.robotRangedUnitLevel);
                break;
            case var _ when unit.Equals(UnitData.robotKnockbackUnitData):
                SaveInfo.robotKnockbackUnitLevel++;
                OnUnitUpgraded?.Invoke(unit, SaveInfo.robotKnockbackUnitLevel);
                break;
            case var _ when unit.Equals(UnitData.robotSupportUnitData):
                SaveInfo.robotSupportUnitLevel++;
                OnUnitUpgraded?.Invoke(unit, SaveInfo.robotSupportUnitLevel);
                break;

            // MARTIAN UNITS
            case var _ when unit.Equals(UnitData.martianBasicUnitData):
                SaveInfo.martianBasicUnitLevel++;
                OnUnitUpgraded?.Invoke(unit, SaveInfo.martianBasicUnitLevel);
                break;
            case var _ when unit.Equals(UnitData.martianHeavyUnitData):
                SaveInfo.martianHeavyUnitLevel++;
                OnUnitUpgraded?.Invoke(unit, SaveInfo.martianHeavyUnitLevel);
                break;
            case var _ when unit.Equals(UnitData.martianRangedUnitData):
                SaveInfo.martianRangedUnitLevel++;
                OnUnitUpgraded?.Invoke(unit, SaveInfo.martianRangedUnitLevel);
                break;
            case var _ when unit.Equals(UnitData.martianDodgerUnitData):
                SaveInfo.martianDodgerUnitLevel++;
                OnUnitUpgraded?.Invoke(unit, SaveInfo.martianDodgerUnitLevel);
                break;
            case var _ when unit.Equals(UnitData.martianSupportUnitData):
                SaveInfo.martianSupportUnitLevel++;
                OnUnitUpgraded?.Invoke(unit, SaveInfo.martianSupportUnitLevel);
                break;

            default:
                throw new NotImplementedException();
        }
    }
    public static int GetLevelOfProtagonist(ProtagonistData protag)
    {
        return protag switch
        {
            var _ when protag.Equals(ProtagonistData.robotProtagonistData) => SaveInfo.robotPlayerLevel,
            var _ when protag.Equals(ProtagonistData.martianProtagonistData) => SaveInfo.martianPlayerLevel,
            _ => throw new NotImplementedException("Could not determine level of protagonist"),
        };
    }
    public static int GetLevelOfUnit(UnitData unit)
    {
        return unit switch
        {
            // Robot Units
            var _ when unit.Equals(UnitData.robotBasicUnitData) => SaveInfo.robotBasicUnitLevel,
            var _ when unit.Equals(UnitData.robotSwarmUnitData) => SaveInfo.robotSwarmUnitLevel,
            var _ when unit.Equals(UnitData.robotRangedUnitData) => SaveInfo.robotRangedUnitLevel,
            var _ when unit.Equals(UnitData.robotKnockbackUnitData) => SaveInfo.robotKnockbackUnitLevel,
            var _ when unit.Equals(UnitData.robotSupportUnitData) => SaveInfo.robotSupportUnitLevel,
            // Martian Units
            var _ when unit.Equals(UnitData.martianBasicUnitData) => SaveInfo.martianBasicUnitLevel,
            var _ when unit.Equals(UnitData.martianHeavyUnitData) => SaveInfo.martianHeavyUnitLevel,
            var _ when unit.Equals(UnitData.martianRangedUnitData) => SaveInfo.martianRangedUnitLevel,
            var _ when unit.Equals(UnitData.martianDodgerUnitData) => SaveInfo.martianDodgerUnitLevel,
            var _ when unit.Equals(UnitData.martianSupportUnitData) => SaveInfo.martianSupportUnitLevel,
            _ => throw new NotImplementedException("Could not determine level of unit"),
        };
    }
}
public class ProtagonistDataHelper
{
    public ProtagonistData robotProtagonistData;
    public ProtagonistData martianProtagonistData;
}
public class UnitDataHelper
{
    public UnitData robotBasicUnitData;
    public UnitData robotSwarmUnitData;
    public UnitData robotRangedUnitData;
    public UnitData robotKnockbackUnitData;
    public UnitData robotSupportUnitData;

    public UnitData martianBasicUnitData;
    public UnitData martianHeavyUnitData;
    public UnitData martianRangedUnitData;
    public UnitData martianDodgerUnitData;
    public UnitData martianSupportUnitData;
}