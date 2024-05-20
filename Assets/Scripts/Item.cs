using Define;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public int id;
    public int itemLevel;
    public string name;
    public List<ItemOption> options = new();
    
    public void AddOption(ItemOption option)
    {
        options.Add(option);
    }

    public void ApplyOptions()
    {
        foreach (var option in options)
        {
            option.Apply(this);
        }
    }
}

public class ConsumptionItem : Item
{
    public ComsumptionOptionType type;
    public float value;
    
}

public abstract class Equipment : Item
{
    public EquipmentRarity equipmentRarity;
    public EquipmentType equipmentType;
    public Dictionary<EquipmentOptionType, EquipmentOption> attributes = new();
}

public class Weapon : Equipment
{
    public bool isRange;
    public int minDamage;
    public int maxDamage;
}

public class Armor : Equipment
{
    public int defense;
}

public class Accessory : Equipment
{

}
