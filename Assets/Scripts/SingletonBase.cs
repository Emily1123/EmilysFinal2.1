using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
    static T instance;

    bool persist = false;

    public static T Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    return null;
                }
                instance.Init();
            }
            return instance;
        }
    }

    public bool Persist
    {
        get { return persist; }
        protected set { persist = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            instance.Init();
            var singletonObject = new GameObject();
            instance = singletonObject.AddComponent<T>();
            singletonObject.name = typeof(T).ToString() + " (Singleton)";
            DontDestroyOnLoad(singletonObject);
        }
    }

    virtual protected void Init() { }

    void OnApplicationQuit()
    {
        instance = null;
    }
}
