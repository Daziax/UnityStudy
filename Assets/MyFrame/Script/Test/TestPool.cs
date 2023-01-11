using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPool
{
    public TestPool()
    {
        Debug.Log("Im done");
    }
    public void Dead()
    {
        Debug.Log("Im dead");
        PoolManager.Instance.StoreObject(this);
    }

}
