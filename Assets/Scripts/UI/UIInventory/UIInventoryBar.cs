using UnityEngine;

public class UIInventoryBar : MonoBehaviour
{
    private RectTransform rectTransform;

    // 标志库存栏是否在底部位置
    private bool _isInventoryBarPositionBottom = true;

    // 用于访问和设置库存栏是否在底部位置
    public bool IsInventoryBarPositionBottom { get => _isInventoryBarPositionBottom; set => _isInventoryBarPositionBottom = value; }

    // 获取RectTransform组件
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // 根据玩家位置切换库存栏位置
        SwitchInventoryBarPosition();
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
