using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                T[] objs = FindObjectsOfType<T>();
                if(objs.Length > 0)
                {
                    _instance = objs[0];
                }
                if(objs.Length > 1)
                {
                    Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                }
                if(_instance == null)
                {
                    Debug.LogError("Tried to access a singleton " + typeof(T).Name + " when one does not exist in the scene.");
                }
            }
            return _instance;
        }
    }
}
public class PersistentSingleton<T> : Singleton<T>
    where T : Component
{
    protected void Awake()
    {
        if(Instance == this)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
}
