using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManagerBase
{
    void Init();
}
public class ManagerBase<T> : MonoBehaviour, IManagerBase where T : ManagerBase<T>
{
    public static T Instance { get; private set; }
    public virtual void Init()
    {
        Instance = this as T;
    }
}
//public class ManagerBase : MonoBehaviour
//{

//}