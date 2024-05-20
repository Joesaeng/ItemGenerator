using Define;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EquipmentDataWriter : MonoBehaviour
{
    string jsonpath = "Resources/Data/BaseEquipmentData.json";

    public int itemLevel;
    public string itemname;
    public EquipmentType equipmentType;
    public int minValue;
    public int maxValue;

    public void WriteData()
    {
        BaseEquipmentData newData = new BaseEquipmentData()
        {
            itemLevel = itemLevel,
            name = itemname,
            equipmentType = equipmentType,
            minValue = minValue,
            maxValue = maxValue,
        };
        JsonDataWriter.WriteData(jsonpath,newData);
    }
}
