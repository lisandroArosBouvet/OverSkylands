using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TradeManager : MonoBehaviour
{
    /*
     +InventarioPlayer
     +InventarioNPC
    +ExchangeManager
    -ComercianteRandom(Comerciante)
    -PlayerInventoryInit()
     */
    [SerializeField]
    private Inventory playerInventory,
                      merchantInventory;
    [SerializeField]
    private MerchantManager merchantManager;
    [SerializeField]
    private ExchangeManager exchangeManager;

    [SerializeField]
    private InventoryResource[] initialPlayerItems;

    public void NextDay()
    {
        ChangeMerchant();
    }

    private void Start()
    {
        exchangeManager.SetSellItem(SellItem);
        FillPlayerInventory();
        ChangeMerchant();
        playerInventory.SetEvCreateNewButton(GetBuyMerchant);
        merchantInventory.SetEvCreateNewButton(GetSellMerchant);
        exchangeManager.ClearSelection();
    }
    private List<string> GetBuyMerchant()
    {
        return merchantManager.GetCurrentMerchant().buyItems.Select(i=>i.data.name).ToList();
    }
    private List<string> GetSellMerchant()
    {
        return merchantManager.GetCurrentMerchant().sellItems.Select(i => i.data.name).ToList();
    }
    private void FillPlayerInventory()
    {
        playerInventory.InitialSets(true, initialPlayerItems, 1000, exchangeManager.Selection);
    }
    private void ChangeMerchant()
    {
        Merchant merchant = merchantManager.GetRandomMerchant();
        merchantInventory.InitialSets(false,merchant.sellItems.ToArray(),merchant.currentMoney, exchangeManager.Selection);
        playerInventory.CheckInteractuableItems(merchant.buyItems);
        exchangeManager.ClearSelection();
    }
    private void SellItem(ItemSelection sellItem, int amount)
    {
        if (sellItem == null)
            return;
        InventoryResource resource = sellItem.GetResource();
        int totalMoneyNeed = resource.price * amount;
        bool cut = false;

        if(sellItem.IsPlayerOwner())
            cut = merchantInventory.GetCurrentMoney() < totalMoneyNeed;
        else
            cut = playerInventory.GetCurrentMoney() < totalMoneyNeed;

        if (cut)
            return;

        if (amount<= sellItem.GetResource().amount)
        {
            sellItem.ChangeAmount(-amount);
            exchangeManager.ChangeAmount(resource.amount);

            InventoryResource equivalentResource = new InventoryResource(resource.data,amount,resource.price);

            if (sellItem.IsPlayerOwner())
            {
                playerInventory.ChangeGold(totalMoneyNeed);
                merchantInventory.AddItem(equivalentResource, exchangeManager.Selection);
                merchantInventory.ChangeGold(-totalMoneyNeed);
            }
            else
            {
                merchantInventory.ChangeGold(totalMoneyNeed);
                playerInventory.AddItem(equivalentResource, exchangeManager.Selection);
                playerInventory.ChangeGold(-totalMoneyNeed);
            }
        }
    }
}
