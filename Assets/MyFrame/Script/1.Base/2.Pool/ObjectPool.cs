using System.Collections;
using System.Collections.Generic;

public class ObjectPool
{
    public Queue<object> objectPool { get; private set; }
    //object objectPoolRoot;
    /// <summary>
    /// 初始化ObjectPool
    /// </summary>
    /// <param name="obj">当前对象池存储的具体Object</param>
    /// <param name="poolCapacity">默认Pool容量，便于优化</param>
    public ObjectPool(object obj, int poolCapacity = 32)
    {
        objectPool = new Queue<object>(poolCapacity);
        //objectPoolRoot = new object();
        EnPool(obj);
        //Object.transform.SetParent(GOPoolRoot.transform);
    }
    public void EnPool(object Object)
    {
        objectPool.Enqueue(Object);
    }
    /// <summary>
    /// 从队列中取出Object
    /// </summary>
    /// <returns></returns>
    public object DePool()
    {
        object obj = objectPool.Dequeue();
        return obj;
    }
}
