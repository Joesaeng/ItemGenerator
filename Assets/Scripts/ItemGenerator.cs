using Define;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int monsterLv;

    public void GenreateItem()
    {
        List<BaseEquipmentData> items = new();
        foreach (var item in DataManager.Instance.BaseEquipmentData)
            items.Add(item.Value);

        // JSON 데이터로 저장된 BaseEquipmentData를 가져온 후에, 설정된 몬스터의 레벨보다
        // 낮은 아이템레벨을 가진 아이템을 필터합니다.
        List<BaseEquipmentData> filteredItems = items.FindAll(item => item.itemLevel <= monsterLv);

        if (filteredItems.Count <= 0)
        {
            Debug.Log("No items available for this monster's level");
            return;
        }

        Equipment equipment;
        CreateEquipment(out equipment, SelectRandomItem(filteredItems));

        // ========== UI로 생성된 아이템을 확인하기 위한 부분 ==========
        string value = "";
        if (equipment is Weapon weapon)
            value = $"Damage : {weapon.minDamage} - {weapon.maxDamage}";
        else if (equipment is Armor armor)
            value = "Defense : " + armor.defense.ToString();

        string att = "";
        foreach (var option in equipment.attributes)
        {
            if (option.Value is FloatEquipmentOption fo)
            {
                att += $"{option.Key} : {fo.Value}\n";
            }
            else if (option.Value is IntegerEquipmentOption io)
            {
                att += $"{option.Key} : {io.Value}\n";
            }
        }

        text.text = "";
        text.text += "Rarity : " + equipment.equipmentRarity + "\n";
        text.text += "Name : " + equipment.name + "\n";
        text.text += value + "\n";
        text.text += "Attribute : \n" + att;
    }

    private BaseEquipmentData SelectRandomItem(List<BaseEquipmentData> items)
    {
        int randomValue = Random.Range(0, items.Count);

        return items[randomValue];
    }

    private void CreateEquipment(out Equipment equipment, BaseEquipmentData data)
    {
        EquipmentRarity rarity;
        equipment = NewEquipment(data);
        equipment.name = data.name;
        int totalWeight = 0;

        // EquipmentType으로 장비의 부위를 정하고, 클래스로 Weapon, Armor, Accecsory 등으로 분리하고 있기 때문에
        // 클래스를 Type으로 파싱하여 옵션을 가져옴
        EquipmentType parentType = (EquipmentType)System.Enum.Parse(typeof(EquipmentType), equipment.GetType().ToString());
        List<EquipmentOptionData> options = new List<EquipmentOptionData>();

        // 장비의 부위별 부여 옵션을 확인하는 프로세스
        foreach (var option in DataManager.Instance.EquipmentOptionData)
        {
            if (option.Value.applicableParts.Contains(data.equipmentType)
                || option.Value.applicableParts.Contains(parentType))
            {
                options.Add(option.Value);
                totalWeight += option.Value.weight;
            }
            if (equipment is Weapon weapon)
            {
                if (weapon.isRange)
                {
                    if (option.Value.applicableParts.Contains(EquipmentType.RangeWeapon))
                    {
                        options.Add(option.Value);
                        totalWeight += option.Value.weight;
                    }
                }
                else
                {
                    if (option.Value.applicableParts.Contains(EquipmentType.MeleeWeapon))
                    {
                        options.Add(option.Value);
                        totalWeight += option.Value.weight;
                    }
                }
            }
        }


        // 설정된 확률을 사용하여 옵션 희귀도 결정
        int[] rarityThresholds = new int[] { 50, 50, 30, 30 };
        int optionCount = 0;

        for (int i = 0; i < rarityThresholds.Length; i++)
        {
            int chance = Random.Range(0, 100);

            // 생성된 장비가 장신구일 때, 한개의 옵션은 필히 부여하게끔 함.
            if (i == 0 && parentType == EquipmentType.Accessory)
                chance = 0;

            if (chance < rarityThresholds[i])
            {
                int randomValue = Random.Range(0, totalWeight);
                int cumulativeWeight = 0;
                foreach (var option in options)
                {
                    cumulativeWeight += option.weight;
                    if (randomValue < cumulativeWeight)
                    {
                        if (option.isFloat)
                        {
                            float optionValue = Random.Range(option.minValue, option.maxValue + 0.01f);

                            optionValue = Mathf.Round(optionValue * 100f) / 100f;

                            FloatEquipmentOption newOption = new FloatEquipmentOption(
                            option.id,
                            option.equipmentOptionType,
                            optionValue);

                            newOption.Apply(equipment);
                            optionCount++;
                            break; // 옵션이 적용되었으므로 루프 탈출
                        }
                        else
                        {
                            int optionValue = Random.Range((int)option.minValue, (int)option.maxValue + 1);

                            IntegerEquipmentOption newOption = new IntegerEquipmentOption(
                            option.id,
                            option.equipmentOptionType,
                            optionValue);

                            newOption.Apply(equipment);
                            optionCount++;
                            break; // 옵션이 적용되었으므로 루프 탈출
                        }
                    }
                }
            }
        }

        // 옵션의 개수에 따라서 장비의 희귀도 설정
        rarity = optionCount switch
        {
            4 => EquipmentRarity.Epic,
            3 => EquipmentRarity.Rare,
            0 => EquipmentRarity.Normal,
            _ => EquipmentRarity.Magic
        };

        equipment.equipmentRarity = rarity;
    }

    private Equipment NewEquipment(BaseEquipmentData data)
    {
        return data.equipmentType switch
        {
            EquipmentType.Sword or EquipmentType.Spear or EquipmentType.Axe or EquipmentType.Hammer
            => new Weapon
            {
                isRange = false,
                equipmentType = data.equipmentType,
                minDamage = data.minValue,
                maxDamage = data.maxValue
            },
            EquipmentType.Bow or EquipmentType.CrossBow or EquipmentType.HandGun or
            EquipmentType.Rifle => new Weapon
            {
                isRange = true,
                equipmentType = data.equipmentType,
                minDamage = data.minValue,
                maxDamage = data.maxValue
            },
            EquipmentType.Helmet or EquipmentType.ChestPlate or EquipmentType.Gloves or EquipmentType.Shoes => new Armor
            {
                equipmentType = data.equipmentType,
                defense = Random.Range(data.minValue, data.maxValue + 1)
            },
            EquipmentType.Ring or EquipmentType.Bracelet or EquipmentType.Necklace => new Accessory
            {
                equipmentType = data.equipmentType
            },
            _ => throw new System.ArgumentException($"Unknown EquipmentType: {data.equipmentType}")
        };
    }
}
