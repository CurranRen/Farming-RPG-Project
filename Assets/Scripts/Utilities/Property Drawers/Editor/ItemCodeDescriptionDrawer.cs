using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))]
public class ItemCodeDescriptionDrawer : PropertyDrawer
{
    // 获取属性高度
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // 改变返回的属性高度为两倍，以适应我们将绘制的额外项目代码描述
        return EditorGUI.GetPropertyHeight(property) * 2;
    }

    // 绘制属性的GUI
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 使用BeginProperty / EndProperty在父属性上意味着预制件覆盖逻辑适用于整个属性
        EditorGUI.BeginProperty(position, label, property);

        // 如果属性类型为整数
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            // 开始检查值是否更改
            EditorGUI.BeginChangeCheck();

            // 绘制项目代码
            var newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height / 2), label, property.intValue);

            // 绘制项目描述
            EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2), "Item Description", GetItemDescription(property.intValue));

            // 如果项目代码值已更改，则设置为新值
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = newValue;
            }
        }

        EditorGUI.EndProperty();
    }

    // 获取项目描述
    private string GetItemDescription(int itemCode)
    {
        SO_ItemList so_itemList;

        // 从资源路径加载项目列表Scriptable Object
        so_itemList = AssetDatabase.LoadAssetAtPath("Assets/Scriptable Object Assets/Item/so_ItemList.asset", typeof(SO_ItemList)) as SO_ItemList;

        // 获取项目详情列表
        List<ItemDetails> itemDetailsList = so_itemList.itemDetails;

        // 查找与项目代码匹配的项目详情
        ItemDetails itemDetail = itemDetailsList.Find(x => x.itemCode == itemCode);

        // 如果找到匹配项，则返回项目描述，否则返回空字符串
        if (itemDetail != null)
        {
            return itemDetail.itemDescription;
        }
        else
        {
            return "";
        }
    }
}
