using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectPool
{
    public Queue<GameObject> GOPool { get; private set; }
    /// <summary>
    /// GameObject根节点，用于分类GameObject
    /// </summary>
    GameObject GOPoolRoot;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameObject">当前对象池存储的具体GameObject</param>
    /// <param name="poolCapacity">默认Pool容量，便于优化</param>
    public GameObjectPool(GameObject gameObject,int poolCapacity=32)
    {
        GOPool = new Queue<GameObject>(poolCapacity);
        GOPoolRoot = new GameObject(gameObject.name);
        GOPoolRoot.transform.SetParent(GameObject.Find("PoolRoot").transform);
        GOPoolRoot.SetActive(false);
        gameObject.transform.SetParent(GOPoolRoot.transform);
        EnPool(gameObject);
        //gameObject.transform.SetParent(GOPoolRoot.transform);
    }
    public void EnPool(GameObject gameObject)
    {
        GOPool.Enqueue(gameObject);
        gameObject.transform.SetParent(GOPoolRoot.transform);
    }
    /// <summary>
    /// 从队列中取出GameObject
    /// </summary>
    /// <returns></returns>
    public GameObject DePool()
    {
        GameObject gameObject = GOPool.Dequeue();
        if(gameObject.transform.parent!=null)
            gameObject.transform.SetParent(null);
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        return gameObject;
    }
    /// <summary>
    /// 销毁改对象池中根节点
    /// </summary>
    public void Clear()
    {
        GameObject.Destroy(GOPoolRoot);
    }

}
