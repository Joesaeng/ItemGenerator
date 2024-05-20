using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

[System.Serializable]
public class DataManager: Singleton<DataManager>
{
    public Dictionary<int, BaseEquipmentData> BaseEquipmentData { get; private set; } = new Dictionary<int, BaseEquipmentData>();
    public Dictionary<int, EquipmentOptionData> EquipmentOptionData{ get; private set; } = new Dictionary<int, EquipmentOptionData>();

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        BaseEquipmentData = LoadJson<Datas<BaseEquipmentData>, int, BaseEquipmentData>("BaseEquipmentData").MakeDict();
        EquipmentOptionData = LoadJson<Datas<EquipmentOptionData>, int, EquipmentOptionData>("EquipmentOptionData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        if (textAsset == null)
        {
            Debug.LogError($"Failed to load JSON file at path: Data/{path}");
            return default(Loader);
        }

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new StringEnumConverter() }
        };

        return JsonConvert.DeserializeObject<Loader>(textAsset.text, settings);
    }
}
