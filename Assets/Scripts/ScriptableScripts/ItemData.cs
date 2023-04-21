using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Resource", order = 1)]
public class ItemData : ScriptableObject
{
    public new string name;
    public Sprite artwork;
    [TextArea]
    public string description, noteCost;
    public int baseCost;
}
