using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SrcManager:Singleton<SrcManager>
{
    Type[] needPools;
    public SrcManager()
    {
        needPools = new Type[15];
    }
    public bool IsNeedPool(Type type)
    {
        foreach(Type typeInPool in needPools )
        {
            if (type == typeInPool)
                return true;
        }
        return false;
    }
    public T New<T>() where T : class, new()
    {
        if (IsNeedPool(typeof(T)))
            return PoolManager.Instance.GetObject<T>() as T;
        return new T();
    }
}
