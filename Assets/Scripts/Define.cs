using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    [Serializable]
    public enum EquipmentRarity
    {
        Normal,
        Magic,
        Rare,
        Epic,
    }

    [Serializable]
    public enum EquipmentType
    {
        Weapon,
        MeleeWeapon,
        Sword,
        Spear,
        Axe,
        Hammer,
        RangeWeapon,
        Bow,
        CrossBow,
        HandGun,
        Rifle,

        Armor,
        Helmet,
        ChestPlate,
        Gloves,
        Shoes,

        Accessory,
        Ring,
        Bracelet,
        Necklace,
    }

    [Serializable]
    public enum EquipmentOptionGrade
    {
        Low,
        Middle,
        High,
    }

    [Serializable]
    public enum EquipmentOptionType
    {
        IncreaseDamage,
        IncreaseAttackSpeed,
        IncreaseMoveSpeed,
        IncreaseDefense,
        IncreaseHealth,
        IncreaseAttackRange,

        AdditionalDamage,
        DamageReduce,

        AdditionalStr,
        AdditionalDex,
        AdditionalVit,
        AdditionalAgi,
        AdditionalHp,
        AdditionalMp,
    }

    [Serializable]
    public enum ComsumptionOptionType
    {
        HPRecover,
        MPRecover,
    }
}
