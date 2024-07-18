using System.Collections.Generic;
using UnityEngine;

// InventoryManager 类，继承自 SingletonMonobehaviour
public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    // 存储物品详情的字典，键为物品代码，值为物品详情
    private Dictionary<int, ItemDetails> itemDetailsDictionary;

    // 各个库存列表的数组
    public List<InventoryItem>[] inventoryLists;

    // 库存列表容量数组，数组的索引是库存列表的编号（来自 InventoryLocation 枚举），值是该库存列表的容量
    [HideInInspector] public int[] inventoryListCapacityIntArray;

    // 可序列化的物品列表脚本对象
    [SerializeField] private SO_ItemList itemList = null;

    // 初始化
    protected override void Awake()
    {
        base.Awake();

        // 创建库存列表
        CreateInventoryLists();

        // 创建物品详情字典
        CreateItemDetailsDictionary();
    }

    // 创建库存列表
    private void CreateInventoryLists()
    {
        // 初始化库存列表数组
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];

        // 为每个库存位置创建一个新的库存列表
        for (int i = 0; i < (int)InventoryLocation.count; i++)
        {
            inventoryLists[i] = new List<InventoryItem>();
        }

        // 初始化库存列表容量数组
        inventoryListCapacityIntArray = new int[(int)InventoryLocation.count];

        // 初始化玩家库存列表容量
        inventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
    }

    /// <summary>
    /// 从脚本对象物品列表中填充 itemDetailsDictionary
    /// </summary>
    private void CreateItemDetailsDictionary()
    {
        // 初始化物品详情字典
        itemDetailsDictionary = new Dictionary<int, ItemDetails>();

        // 将脚本对象物品列表中的每个物品详情添加到字典中
        foreach (ItemDetails itemDetails in itemList.itemDetails)
        {
            itemDetailsDictionary.Add(itemDetails.itemCode, itemDetails);
        }
    }

    /// <summary>
    /// 将物品添加到指定位置的库存列表中，然后销毁 gameObjectToDelete
    /// </summary>
    public void AddItem(InventoryLocation inventoryLocation, Item item, GameObject gameObjectToDelete)
    {
        // 将物品添加到库存
        AddItem(inventoryLocation, item);

        // 销毁物品的游戏对象
        Destroy(gameObjectToDelete);
    }

    /// <summary>
    /// 将物品添加到指定位置的库存列表中
    /// </summary>
    public void AddItem(InventoryLocation inventoryLocation, Item item)
    {
        int itemCode = item.ItemCode;
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        // 检查库存中是否已经包含该物品
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);

        if (itemPosition != -1)
        {
            // 如果物品已经存在，则在指定位置添加物品
            AddItemAtPosition(inventoryList, itemCode, itemPosition);
        }
        else
        {
            // 如果物品不存在，则在库存末尾添加物品
            AddItemAtPosition(inventoryList, itemCode);
        }

        // 发送库存已更新事件
        EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
    }

    /// <summary>
    /// 将物品添加到库存末尾
    /// </summary>
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode)
    {
        InventoryItem inventoryItem = new InventoryItem();

        // 设置物品代码和数量
        inventoryItem.itemCode = itemCode;
        inventoryItem.itemQuantity = 1;
        inventoryList.Add(inventoryItem);

        // 调试打印库存列表
        DebugPrintInventoryList(inventoryList);
    }

    /// <summary>
    /// 在指定位置添加物品
    /// </summary>
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();

        // 增加物品数量
        int quantity = inventoryList[position].itemQuantity + 1;
        inventoryItem.itemQuantity = quantity;
        inventoryItem.itemCode = itemCode;
        inventoryList[position] = inventoryItem;

        // 调试打印库存列表
        DebugPrintInventoryList(inventoryList);
    }

    /// <summary>
    /// 查找库存中是否已经存在某个物品代码。返回物品在库存列表中的位置，如果物品不存在则返回 -1
    /// </summary>
    public int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];

        // 遍历库存列表，查找物品代码
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].itemCode == itemCode)
            {
                return i;
            }
        }

        // 如果物品不存在，返回 -1
        return -1;
    }

    /// <summary>
    /// 返回 itemCode 对应的物品详情（从 SO_ItemList 中获取），如果物品代码不存在则返回 null
    /// </summary>
    public ItemDetails GetItemDetails(int itemCode)
    {
        ItemDetails itemDetails;

        // 尝试从字典中获取物品详情
        if (itemDetailsDictionary.TryGetValue(itemCode, out itemDetails))
        {
            return itemDetails;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 调试打印库存列表
    /// </summary>
    private void DebugPrintInventoryList(List<InventoryItem> inventoryList)
    {
        // 遍历库存列表，打印每个物品的描述和数量
        foreach (InventoryItem inventoryItem in inventoryList)
        {
            Debug.Log("Item Description:" + InventoryManager.Instance.GetItemDetails(inventoryItem.itemCode).itemDescription + "    Item Quantity: " + inventoryItem.itemQuantity);
        }
        Debug.Log("******************************************************************************");
    }
}
