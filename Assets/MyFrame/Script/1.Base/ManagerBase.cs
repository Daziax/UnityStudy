using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManagerBase<T>  where T : ManagerBase<T>,new() //IManagerBase
{
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
              return instance=new T(); 
            return instance;
        }
    }
    /*public virtual void Init()
    {
        //Instance = this as T;
    }*/
}
//public class ManagerBase : MonoBehaviour
//{

//}