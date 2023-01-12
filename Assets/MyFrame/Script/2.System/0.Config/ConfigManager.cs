using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;


//[CreateAssetMenu(fileName ="ConfigManager",menuName ="Config/ConfigManager")]
public class ConfigManager:SerializedScriptableObject
{ 
    [SerializeField]
    [DictionaryDrawerSettings(KeyLabel ="类型",ValueLabel ="详细配置")]
    private Dictionary<ConfigType, ConfigSettings> configs;

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <param name="configType"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception">不存在该类型时抛出异常</exception>
    public ConfigSettings GetConfig(ConfigType configType)
    {
        if (!configs.ContainsKey(configType))
            throw new System.Exception($"不存在该类型{configType}");
        return configs[configType];
    }
}
public enum ConfigType
{
    武器,
    角色,
    天气
}

