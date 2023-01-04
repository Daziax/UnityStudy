using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Test1",menuName ="Test2/test2",order =0)]
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

}
