using UnityEngine;

// 使该类可序列化
[System.Serializable]
public class ItemDetails
{
    public int itemCode;
    public ItemType itemType;
    public string itemDescription;
    public Sprite itemSprite;
    public string itemLongDescription;

    // 用于网格系统中的距离测量
    public short itemUseGridRadius;

    // 用于世界坐标中的距离测量
    public float itemUseRadius;

    public bool isStartingItem;
    public bool canBePickedUp;
    public bool canBeDropped;
    public bool canBeEaten;
    public bool canBeCarried;
}
