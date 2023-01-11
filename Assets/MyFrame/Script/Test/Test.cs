using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test:MonoBehaviour
{
    ObjectPool objectPool;
    [SerializeField]
    GameObject go;
    Dictionary<string, List<string>> dic;
    TestPool testPool;
    Queue<TestPool> gotObjects;
    private void Awake()
    {
        //objectPool = new ObjectPool(new List<string>());

        //dic = new Dictionary<string, List<string>>();
        //dic.Add("1", new List<string>());
        testPool = new TestPool();
        gotObjects = new Queue<TestPool>();
        gotObjects.Enqueue(testPool);
        gotObjects.Enqueue(new TestPool());
        gotObjects.Enqueue(new TestPool());
        gotObjects.Enqueue(new TestPool());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            while (gotObjects.Count > 0)
            {
                gotObjects.Dequeue().Dead();
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            gotObjects.Enqueue(PoolManager.Instance.GetObject<TestPool>() as TestPool);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(PoolManager.Instance.GetDicCount);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(gotObjects.Count);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < gotObjects.Count; ++i)
            {
                Debug.Log($"count{gotObjects.Count}当前{i}");
                //gotObjects.Dequeue().Dead();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PoolManager.Instance.GetGameObject(go);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("清理ObjectPool");
            PoolManager.Instance.ClearAllObjectPools();
        }
    }
}
/*[CreateAssetMenu(fileName ="Test1",menuName ="Test2/test2",order =0)]
public class Test : ScriptableObject
{
    [SerializeField]
    private GameObject gameObject;
    [SerializeField]
    List<GameObject> tests;
}
interface IA
{
    void Func();

}*/
