using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : ManagerBase<PoolManager>
{
    Dictionary<string, GameObjectPool> GOPools;
    Dictionary<string, ObjectPool> objectPools;
    public override void Init()
    {
        base.Init();
        GOPools = new Dictionary<string, GameObjectPool>();
        objectPools = new Dictionary<string, ObjectPool>();
    }
    #region GameObjectPoolManager
    /// <summary>
    /// 通过预制体获取GameObject
    /// </summary>
    /// <param name="prefab">需要被生成为GameObject的预制体</param>
    /// <returns></returns>
    public GameObject GetGameObject(GameObject prefab)
    {
        GameObject go;
        if (GOPools.ContainsKey(prefab.name) && GOPools[prefab.name].GOPool.Count > 0)
            go= GOPools[prefab.name].DePool();
        else
        {
            go = GameObject.Instantiate(prefab);
            go.name = prefab.name;
            //GOPools.Add(prefab.name, new GameObjectPool(go));
        }
        return go;
    }
    /// <summary>
    /// 获取GameObject的组件T
    /// </summary>
    /// <typeparam name="T">需要获取的组件类型</typeparam>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public T GetComponent<T>(GameObject prefab) where T: Component
    {
        return GetGameObject(prefab.gameObject).GetComponent<T>(); 
    }
    /// <summary>
    /// 存入GameObject
    /// </summary>
    /// <param name="go"></param>
    public void StoreGameObject(GameObject go)
    {
        if (GOPools.ContainsKey(go.name))
            GOPools[go.name].EnPool(go);
        else
            GOPools.Add(go.name, new GameObjectPool(go));
    }
    #endregion

    #region ObjectPoolManager
    /// <summary>
    /// 通过预制体获取GameObject
    /// </summary>
    /// <param name="prefab">需要被生成为GameObject的预制体</param>
    /// <returns></returns>
    public object GetObject<T>() where T : class, new()
    {
        Debug.Log(typeof(T).FullName);
        T obj;
        if (objectPools.ContainsKey(typeof(T).FullName) && objectPools[typeof(T).FullName].objectPool.Count > 0)
             obj= objectPools[typeof(T).FullName].DePool() as T;
        else
        {
            obj = new T();
        }
        return obj;
    }
    /// <summary>
    /// 存入Object(类)
    /// </summary>
    /// <param name="obj">需要存入的对象</param>
    public void StoreObject(object obj)
    {
        if (objectPools.ContainsKey(obj.GetType().FullName))
            objectPools[obj.GetType().FullName].EnPool(obj);
        else
            objectPools.Add(obj.GetType().FullName, new ObjectPool(obj));
    }
    #endregion

    /// <summary>
    /// 销毁所有GameObjectPools对象池
    /// </summary>
    public void ClearAllGOPools()
    {
        foreach(var item in GOPools)
        {
            item.Value.Clear();
        }
        GOPools.Clear();
    }
    /// <summary>
    /// 根据prefab清理对象池
    /// </summary>
    /// <param name="prefab">需要清理的对象</param>
    public void ClearGOPool(GameObject prefab)
    {
        if(GOPools.ContainsKey(prefab.name))
        {
            GameObjectPool value;
            GOPools.Remove(prefab.name,out value);
            value.Clear();
        }
    }
    /// <summary>
    /// 清理所有ObjectPools
    /// </summary>
    public void ClearAllObjectPools()
    {
        objectPools.Clear();
    }
    /// <summary>
    /// 根据类型T清理对象池
    /// </summary>
    /// <typeparam name="T">待清理的对象</typeparam>
    public void ClearObjectPool<T>()
    {
        if (GOPools.ContainsKey(typeof(T).FullName))
        {
            GOPools.Remove(typeof(T).FullName);
        }
    }
    public int GetDicCount
    {
        get => objectPools["TestPool"].objectPool.Count;
    }
}
