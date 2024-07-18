using System;
using System.Collections.Generic;

// 移动事件的委托，包含各种移动和工具使用状态的参数
public delegate void MovementDelegate(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect,
    bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
    bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
    bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
    bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight);

// 事件处理类，用于管理和触发游戏事件
public static class EventHandler
{
    // 库存更新事件，使用 Action 委托，参数为库存位置和物品列表
    public static event Action<InventoryLocation, List<InventoryItem>> InventoryUpdatedEvent;

    // 触发库存更新事件的方法
    public static void CallInventoryUpdatedEvent(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        // 如果有订阅者，触发事件
        if (InventoryUpdatedEvent != null)
            InventoryUpdatedEvent(inventoryLocation, inventoryList);
    }

    // 移动事件，使用 MovementDelegate 委托
    public static event MovementDelegate MovementEvent;

    // 触发移动事件的方法，供发布者调用
    public static void CallMovementEvent(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect,
    bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
    bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
    bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
    bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
    {
        // 如果有订阅者，触发事件并传递所有参数
        if (MovementEvent != null)
            MovementEvent(inputX, inputY,
                isWalking, isRunning, isIdle, isCarrying,
                toolEffect,
                isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
                isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown,
                isPickingRight, isPickingLeft, isPickingUp, isPickingDown,
                isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
                idleUp, idleDown, idleLeft, idleRight);
    }
}
