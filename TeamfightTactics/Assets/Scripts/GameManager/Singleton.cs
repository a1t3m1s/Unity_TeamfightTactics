using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();

                //if (instance == null)
                //{
                //    GameObject gameObject = new GameObject(typeof(T).Name);
                //    instance = gameObject.AddComponent<T>();
                //}
            }

            return instance;
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(instance);
        }
    }

}
