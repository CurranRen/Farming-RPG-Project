using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    // 存储物品详细信息的字典
    private Dictionary<int, ItemDetails> itemDetailsDictionary;

    // 从脚本化对象中获取的物品列表，使用SerializeField使其在Unity编辑器中可见
    [SerializeField] private SO_ItemList itemList = null;

    protected override void Awake()
    {
        base.Awake();

        CreateItemDetailsDictionary();
    }

    /// 从物品列表中填充字典
    private void CreateItemDetailsDictionary()
    {
        itemDetailsDictionary = new Dictionary<int, ItemDetails>();

        // 遍历itemList中的每个ItemDetails对象，将其添加到字典中
        foreach (ItemDetails itemDetails in itemList.itemDetails)
        {
            itemDetailsDictionary.Add(itemDetails.itemCode, itemDetails);
        }
    }

    /// 根据物品编码返回物品详细信息，如果物品编码不存在则返回null
    public ItemDetails GetItemDetails(int itemCode)
    {
        ItemDetails itemDetails;

        // 尝试从字典中获取物品详细信息，如果成功则返回，否则返回null
        if (itemDetailsDictionary.TryGetValue(itemCode, out itemDetails))
        {
            return itemDetails;
        }
        else
        {
            return null;
        }
    }
}
