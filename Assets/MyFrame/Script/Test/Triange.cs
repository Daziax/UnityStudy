using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triange : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("I'm done");
        Invoke("Dead", 3);

    }
    // Start is called before the first frame update
    private void Start()
    {
        //Dead();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void Dead()
    {
        Debug.Log("I'm dead");
        PoolManager.Instance.StoreGameObject(gameObject);
    }
}
