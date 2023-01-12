using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Sirenix.OdinInspector;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    public static T Instance{get;private set;}
    protected virtual void Awake()
    {
        if(Instance==null)
            Instance = this as T;

    }

}
