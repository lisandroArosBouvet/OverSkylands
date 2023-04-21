using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ItemSelection : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI titleText, descriptionText, amountText, priceText;

    private Action<ItemSelection, bool> evSelect;
    private bool playerOwner;
    private InventoryResource itemData;

    private void Start()
    {
        button.onClick.AddListener(() =>PressButton());
    }

    public void CreateButton(bool playerOwner, InventoryResource data, Action<ItemSelection, bool> evSelect)
    {
        this.playerOwner = playerOwner;
        this.evSelect = evSelect;
        NewItem(data);
    }

    public void NewItem(InventoryResource data)
    {
        itemData = data;
        image.sprite = itemData.data.artwork;
        titleText.text = itemData.data.name;
        amountText.text = itemData.amount.ToString();
        priceText.text = itemData.price.ToString();
    }
    public bool CanExchange(int money)
    {
        return money >= itemData.price;
    }
    public void ChangeAmount(int value)
    {
        itemData.amount += value;
        amountText.text = itemData.amount.ToString();
    }
    public string GetID()
    {
        return itemData.data.name;
    }
    public void Usable(bool isInteractuable)
    {
        button.interactable = isInteractuable;
    }

    public void SetPrice(int newPrice)
    {
        itemData.price = newPrice;
        priceText.text = itemData.price.ToString();
    }
    public InventoryResource GetResource()
    {
        return itemData;
    }
    public bool IsPlayerOwner()
    {
        return playerOwner;
    }
    private void PressButton()
    {
        evSelect(this, playerOwner);
    }
}
