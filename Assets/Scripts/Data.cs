using Define;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Diagnostics;

public interface IData
{
    public int id { get; }
}
[Serializable]
public class BaseEquipmentData : IData
{
    int IData.id => id;
    public int id;
    public int itemLevel;
    public string name;
    public EquipmentType equipmentType;
    public int minValue;
    public int maxValue;
}

[Serializable]
public class EquipmentOptionData : IData
{
    int IData.id => id;
    public int id;
    public EquipmentOptionGrade equipmentOptionGrade;
    public EquipmentOptionType equipmentOptionType;
    public List<EquipmentType> applicableParts;
    public bool isFloat;
    public int weight;
    public float minValue;
    public float maxValue;
}
 

[Serializable]
public class Datas<T> : ILoader<int, T> where T : IData
{
    public List<T> datas = new List<T>();

    public Dictionary<int, T> MakeDict()
    {
        Dictionary<int,T> dict = new Dictionary<int,T>();
        foreach (T data in datas)
        {
            dict.Add(data.id, data);
        }
        return dict;
    }
}
