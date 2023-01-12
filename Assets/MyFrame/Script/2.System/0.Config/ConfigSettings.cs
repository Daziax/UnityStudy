using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "ConfigSetting", menuName = "Config/详细配置")]
public class ConfigSettings : ScriptableObject
{
    [ShowInInspector]
    public int ID { get; private set; }
    [ShowInInspector]
    public string Name { get; private set; }
}
