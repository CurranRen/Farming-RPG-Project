using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    // 当另一个碰撞体进入触发器时调用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 获取碰撞体上的Item组件
        Item item = collision.GetComponent<Item>();

        if (item != null)
        {
            // 获取物品详细信息
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(item.ItemCode);

            // 将物品描述打印到控制台
            Debug.Log(itemDetails.itemDescription);
        }
    }
}
