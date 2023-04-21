using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Trader", order = 1)]
public class MerchantData : ScriptableObject
{
    public new string name;
    public Sprite artwork;

    public List<CommercialItem> buyItem;
    public List<CommercialItem> sellItem;
    public Vector2Int moneyRange;
}
[System.Serializable]
public class CommercialItem
{
    public ItemData item;
    public Interest interest;
    [Range(0.01f, 1f)]
    public float percentToAppear = .5f;
    public Vector2Int amountRange;
}
