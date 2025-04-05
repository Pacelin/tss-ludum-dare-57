using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LudumDare57.Inventory
{
    [CustomPropertyDrawer(typeof(InventoryItemAttribute))]
    public class InventoryItemPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var asset = AssetDatabase.LoadAssetAtPath<ItemsCollection>("Assets/_Project/Configs/Inventory Items.asset");
            var buttonRect = EditorGUI.PrefixLabel(position, label);
            if (EditorGUI.DropdownButton(buttonRect, new GUIContent(property.stringValue), FocusType.Passive))
            {
                var menu = new GenericMenu();
                foreach (KeyValuePair<string, ItemConfig> pair in asset)
                {
                    menu.AddItem(new GUIContent(pair.Key), false, () =>
                    {
                        property.stringValue = pair.Key;
                        property.serializedObject.ApplyModifiedProperties();
                        property.serializedObject.Update();
                    });
                }
                menu.ShowAsContext();
            }
        }
    }
}