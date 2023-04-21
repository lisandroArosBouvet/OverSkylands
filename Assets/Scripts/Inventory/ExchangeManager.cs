using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ExchangeManager : MonoBehaviour
{
    [SerializeField]
    private Button exchangeBtn,
                    oneAmountBtn,
                    tenAmountBtn,
                    oneHundredAmountBtn;
    private Image currentButtonSelect;
    [SerializeField]
    private Sprite emptyItem;
    [SerializeField]
    private Image itemArt;
    [SerializeField]
    private TextMeshProUGUI amountText,
                            priceText,
                            exhangeText,
                            nameItemText;
    private int amountSelection = 1,
                price;

    private ItemSelection buttonSelect;
    private Action<ItemSelection, int> evSellItem;

    private void Start()
    {
        currentButtonSelect = oneAmountBtn.GetComponent<Image>();
        PaintAmountButton(currentButtonSelect);
        oneAmountBtn.onClick.AddListener(()=>
        {
            PaintAmountButton(oneAmountBtn.GetComponent<Image>());
            CommercialAmount(1);
        });
        tenAmountBtn.onClick.AddListener(() =>
        {
            PaintAmountButton(tenAmountBtn.GetComponent<Image>());
            CommercialAmount(10);
        });
        oneHundredAmountBtn.onClick.AddListener(() =>
        {
            PaintAmountButton(oneHundredAmountBtn.GetComponent<Image>());
            CommercialAmount(100);
        });

        exchangeBtn.onClick.AddListener(()=> SellItem(amountSelection));
    }
    public void ClearSelection()
    {
        buttonSelect = null;
        nameItemText.text = amountText.text = exhangeText.text = priceText.text = "";
        itemArt.sprite = emptyItem;
        price = 0;
    }
    public void SetSellItem(Action<ItemSelection,int> evSellItem)
    {
        this.evSellItem = evSellItem;
    }
    public void Selection(ItemSelection selection, bool isPlayer)
    {
        buttonSelect = selection;
        nameItemText.text = selection.GetResource().data.name;
        itemArt.sprite = selection.GetResource().data.artwork;
        amountText.text = $"Amount: {selection.GetResource().amount}"; 
        this.price = selection.GetResource().price;

        CommercialAmount(amountSelection);
        exhangeText.text = isPlayer ? "Sell" : "Buy";
    }
    public void ChangeAmount(int amount)
    {
        amountText.text = $"Amount: {amount}";;
    }
    public void SellItem(int amount)
    {
        evSellItem(buttonSelect, amount);
    }
    private void PaintAmountButton(Image newPaint)
    {
        currentButtonSelect.color = Color.white;
        currentButtonSelect = newPaint;
        currentButtonSelect.color = Color.green;
    }
    private void CommercialAmount(int amount)
    {
        amountSelection = amount;
        priceText.text = (price * amountSelection).ToString();
    }
}
