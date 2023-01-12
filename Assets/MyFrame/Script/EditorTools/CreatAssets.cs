using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName ="CreateAssets")]
public class CreateAssets:ScriptableObject
{
    [SerializeField]
    int a;
    
    public ScriptableObject b;
#if UNITY_EDITOR
    [Button(ButtonHeight =100)]
    void CreateAsset()
    {
        Debug.Log($"{a}");
    }

#endif
}
