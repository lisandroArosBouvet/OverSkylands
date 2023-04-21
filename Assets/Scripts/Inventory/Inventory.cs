using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private Transform layoutInventory;
    [SerializeField]
    private ItemSelection buttonModel;

    private int money;
    private Dictionary<string, ItemSelection> selections;
    private bool isPlayer;
    private Func<List<string>> evCreateNewBtn;


    public void InitialSets(bool isPlayer, InventoryResource[] list, int gold, Action<ItemSelection, bool> selection)
    {
        if (selections != null)
        {
            foreach (var button in selections)
            {
                Destroy(button.Value.gameObject);
            }
            selections.Clear();
        }
        else
            selections = new Dictionary<string, ItemSelection>();
        money = 0;
        ChangeGold(gold);
        this.isPlayer = isPlayer;
        foreach (var resource in list)
        {
            AddItem(resource,selection);
        }
    }

    public void SetEvCreateNewButton(Func<List<string>> evCreateNewBtn)
    {
        this.evCreateNewBtn = evCreateNewBtn;
    }
    public int GetCurrentMoney()
    {
        return money;
    }
    public void ChangeGold(int gold)
    {
        money += gold;
        moneyText.text = money.ToString();
    }
    public void AddItem(InventoryResource resource, Action<ItemSelection, bool> selection)
    {
        int amount = resource.amount;
        string idName = resource.data.name;
        if(amount <= 0)
            amount *= -1;
        if (selections.Keys.Contains(idName))
            selections[idName].ChangeAmount(amount);
        else
            CreateButtonSelection(resource, selection);
    }
    public void RemoveItem(string idName, int amount = 1)
    {
        if (amount >= 0)
            amount *= -1;
        selections[idName].ChangeAmount(-amount);
    }
    public void CheckInteractuableItems(List<InventoryResource> listResources)
    {
        List<string> listNames = listResources.Select(i => i.data.name).ToList();
        foreach (var item in selections)
        {
            bool interactuable = listNames.Contains(item.Key);
            item.Value.Usable(interactuable);
            if (interactuable)
            {
                int price = listResources.Where(x => x.data.name == item.Key).First().price;
                item.Value.SetPrice(price);
            }
            else
                item.Value.SetPrice(0);
        }
    }
    private ItemSelection CreateButtonSelection(InventoryResource resource, Action< ItemSelection, bool> selection)
    {
        var btn = Instantiate(buttonModel,layoutInventory);
        btn.CreateButton(isPlayer, resource,selection);
        selections.Add(resource.data.name, btn);
        List<string> buys = evCreateNewBtn?.Invoke();
        if(buys != null)
            btn.Usable(buys.Contains(resource.data.name));

        return btn;
    }
}
