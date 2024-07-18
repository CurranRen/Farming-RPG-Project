using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryBar : MonoBehaviour
{
    [SerializeField] private Sprite blank16x16sprite = null;

    [SerializeField] private UIInventorySlot[] inventorySlot = null;

    private RectTransform rectTransform;

    // 标志库存栏是否在底部位置
    private bool _isInventoryBarPositionBottom = true;

    // 公共属性，用于访问和设置库存栏是否在底部位置
    public bool IsInventoryBarPositionBottom { get => _isInventoryBarPositionBottom; set => _isInventoryBarPositionBottom = value; }

    // 获取RectTransform组件
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // 当脚本被禁用时，取消订阅库存更新事件
    private void OnDisable()
    {
        EventHandler.InventoryUpdatedEvent -= InventoryUpdated;
    }

    // 当脚本被启用时，订阅库存更新事件
    private void OnEnable()
    {
        EventHandler.InventoryUpdatedEvent += InventoryUpdated;
    }

    private void Update()
    {
        // 根据玩家位置切换库存栏位置
        SwitchInventoryBarPosition();
    }

    // 清空库存槽中的内容
    private void ClearInventorySlots()
    {
        if (inventorySlot.Length > 0)
        {
            // 遍历库存槽并更新为空白精灵图像
            for (int i = 0; i < inventorySlot.Length; i++)
            {
                inventorySlot[i].inventorySlotImage.sprite = blank16x16sprite;
                inventorySlot[i].textMeshProUGUI.text = "";
                inventorySlot[i].itemDetails = null;
                inventorySlot[i].itemQuantity = 0;
            }
        }
    }

    // 当库存更新时调用的方法
    private void InventoryUpdated(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        if (inventoryLocation == InventoryLocation.player)
        {
            // 清空库存槽
            ClearInventorySlots();

            if (inventorySlot.Length > 0 && inventoryList.Count > 0)
            {
                // 遍历库存槽并用对应的库存列表项更新
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    if (i < inventoryList.Count)
                    {
                        int itemCode = inventoryList[i].itemCode;

                        // 获取物品详情
                        ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(itemCode);

                        if (itemDetails != null)
                        {
                            // 将图像和详情添加到库存槽中
                            inventorySlot[i].inventorySlotImage.sprite = itemDetails.itemSprite;
                            inventorySlot[i].textMeshProUGUI.text = inventoryList[i].itemQuantity.ToString();
                            inventorySlot[i].itemDetails = itemDetails;
                            inventorySlot[i].itemQuantity = inventoryList[i].itemQuantity;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    // 切换库存栏位置的方法
    private void SwitchInventoryBarPosition()
    {
        // 获取玩家在视口中的位置
        Vector3 playerViewportPosition = Player.Instance.GetPlayerViewportPosition();

        // 如果玩家位置高于视口的30%并且库存栏不在底部
        if (playerViewportPosition.y > 0.3f && IsInventoryBarPositionBottom == false)
        {
            // 将库存栏设置到视口底部位置
            rectTransform.pivot = new Vector2(0.5f, 0f);
            rectTransform.anchorMin = new Vector2(0.5f, 0f);
            rectTransform.anchorMax = new Vector2(0.5f, 0f);
            rectTransform.anchoredPosition = new Vector2(0f, 2.5f);

            IsInventoryBarPositionBottom = true;
        }
        // 如果玩家位置低于或等于视口的30%并且库存栏在底部
        else if (playerViewportPosition.y <= 0.3f && IsInventoryBarPositionBottom == true)
        {
            // 将库存栏设置到视口顶部位置
            rectTransform.pivot = new Vector2(0.5f, 1f);
            rectTransform.anchorMin = new Vector2(0.5f, 1f);
            rectTransform.anchorMax = new Vector2(0.5f, 1f);
            rectTransform.anchoredPosition = new Vector2(0f, -2.5f);

            IsInventoryBarPositionBottom = false;
        }
    }
}
