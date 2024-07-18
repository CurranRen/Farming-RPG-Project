using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    // 当有其他物体进入触发器区域时调用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 获取碰撞物体上的Item组件
        Item item = collision.GetComponent<Item>();

        // 如果碰撞物体上存在Item组件
        if (item != null)
        {
            // 获取物品详情
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(item.ItemCode);

            // 如果物品可以被拾取
            if (itemDetails.canBePickedUp == true)
            {
                // 将物品添加到玩家的库存
                InventoryManager.Instance.AddItem(InventoryLocation.player, item, collision.gameObject);
            }
        }
    }
}
