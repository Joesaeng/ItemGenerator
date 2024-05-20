using Define;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class EquipmentOptionWriter : MonoBehaviour
{
    string jsonpath = "Resources/Data/EquipmentOptionData.json";

    public EquipmentOptionGrade equipmentOptionGrade;
    public EquipmentOptionType equipmentOptionType;
    public List<EquipmentType> applicableParts = new List<EquipmentType>();
    public float minValue;
    public float maxValue;
    public int weight;
    public bool isFloat;

    public void WriteData()
    {
        EquipmentOptionData newData = new EquipmentOptionData()
        {
            equipmentOptionGrade = equipmentOptionGrade,
            equipmentOptionType = equipmentOptionType,
            applicableParts = applicableParts,
            minValue = minValue,
            maxValue = maxValue,
            weight = weight,
            isFloat = isFloat
        };
        JsonDataWriter.WriteData(jsonpath,newData);
    }
}
