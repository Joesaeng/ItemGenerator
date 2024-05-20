using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EquipmentDataWriter))]
public class EquipmentDataWriterEditorButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EquipmentDataWriter writer = (EquipmentDataWriter)target;
        if (GUILayout.Button("Write Data"))
        {
            writer.WriteData();
        }
    }
}

[CustomEditor(typeof(EquipmentOptionWriter))]
public class EquipmentOptionWriterEditorButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EquipmentOptionWriter writer = (EquipmentOptionWriter)target;
        if (GUILayout.Button("Write Data"))
        {
            writer.WriteData();
        }
    }
}

[CustomEditor(typeof(ItemGenerator))]
public class ItemGeneratorButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemGenerator generator = (ItemGenerator)target;
        if (GUILayout.Button("Generate Item"))
        {
            generator.GenreateItem();
        }
    }
}
