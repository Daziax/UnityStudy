using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameRoot : SingletonMono<GameRoot>
{
    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();
        DontDestroyOnLoad(gameObject);
        InitManagers();
    }
    /// <summary>
    /// 初始化所有管理器
    /// </summary>
    private void InitManagers()
    {
        IManagerBase[] managers = GetComponents<IManagerBase>();
        foreach(IManagerBase manager in managers)
        {
            manager.Init();
        }
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


