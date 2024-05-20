using Define;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemOption
{
    public int ID { get; protected set; }
    public abstract void Apply(Item item);
}

public abstract class EquipmentOption : ItemOption
{
    public EquipmentOptionType Type { get; protected set; }

    public override void Apply(Item item) { }
}

public class FloatEquipmentOption : EquipmentOption
{
    public float Value { get; private set; }

    public FloatEquipmentOption(int id,EquipmentOptionType type, float value)
    {
        ID = id;
        Type = type;
        Value = value;
    }

    public override void Apply(Item item)
    {
        base.Apply(item);
        Equipment equipment = item as Equipment;
        if (equipment.attributes.ContainsKey(Type))
        {
            FloatEquipmentOption option = equipment.attributes[Type] as FloatEquipmentOption;
            option.Value += Value;
        }
        else
            equipment.attributes.Add(Type, this);
    }
}

public class IntegerEquipmentOption : EquipmentOption
{
    public int Value { get; private set; }

    public IntegerEquipmentOption(int id, EquipmentOptionType type, int value)
    {
        ID = id;
        Type = type;
        Value = value;
    }

    public override void Apply(Item item)
    {
        base.Apply(item);
        Equipment equipment = item as Equipment;
        if (equipment.attributes.ContainsKey(Type))
        {
            IntegerEquipmentOption option = equipment.attributes[Type] as IntegerEquipmentOption;
            option.Value += Value;
        }
        else
            equipment.attributes.Add(Type, this);
    }
}


//public class ConsumptionOption : ItemOption
//{
//    public ComsumptionOptionType Type { get; private set; }

//    public ConsumptionOption(int id,string name, float value, ComsumptionOptionType type)
//    {
//        ID = id;
//        Value = value;
//        Type = type;
//    }

//    public override void Apply(Item item)
//    {
//        ConsumptionItem consumptionItem = item as ConsumptionItem;
//        consumptionItem.type = Type;
//        consumptionItem.value = Value;
//    }
//}
