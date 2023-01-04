using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : ManagerBase<PoolManager>,IManagerBase
{
    void IManagerBase.Init()
    {
        if (Instance == null)
            Debug.Log("PoolManager未初始");
        base.Init();
        if (Instance != null)
            Debug.Log("PoolManager初始化完成");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
