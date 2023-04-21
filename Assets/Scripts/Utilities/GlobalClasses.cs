using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Interest
{
    little,
    bit,
    intrigued,
    interest,
    lot,
    tooMuch,
    essential
}
[System.Serializable]
public class InventoryResource
{
    public ItemData data;
    public int amount;
    public int price;

    public InventoryResource(ItemData item, int amount, int price)
    {
        this.data = item;
        this.amount = amount;
        this.price = price;
    }
}