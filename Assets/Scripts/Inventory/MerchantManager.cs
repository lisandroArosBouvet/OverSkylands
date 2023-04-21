using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantManager : MonoBehaviour
{
    [SerializeField]
    private Image splashArt;
    [SerializeField]
    private TMPro.TextMeshProUGUI merchantName;
    [SerializeField]
    private MerchantData[] merchantsData;
    private Merchant[] merchants;
    private Merchant currentMerchant;
    private void Awake()
    {
        CreateConcretMerchants();
    }

    public Merchant GetRandomMerchant()
    {
        Merchant merchant = merchants[Random.Range(0, merchants.Length)];
        merchant.ShuffleItemsCommercials();
        splashArt.sprite = merchant.artwork;
        merchantName.text = merchant.idName;
        currentMerchant = merchant;
        return merchant;
    }
    public Merchant GetCurrentMerchant()
    {
        return currentMerchant;
    }
    private void CreateConcretMerchants()
    {
        merchants = new Merchant[merchantsData.Length];
        for (int i = 0; i < merchants.Length; i++)
        {
            merchants[i] = new Merchant(merchantsData[i]);
        }
    }

}
public class Merchant
{
    public string idName;
    public Sprite artwork;
    public List<InventoryResource> buyItems;
    public List<InventoryResource> sellItems;
    public int currentMoney;

    private List<CommercialItem> buyItemsData;
    private List<CommercialItem> sellItemsData;
    private const float errorRange = .1f;
    private Vector2Int rangeMoney;

    public Merchant(MerchantData data)
    {
        idName = data.name;
        artwork = data.artwork;
        buyItems = new List<InventoryResource>();
        sellItems = new List<InventoryResource>();
        buyItemsData = data.buyItem;
        sellItemsData = data.sellItem;
        rangeMoney = data.moneyRange;
    }

    public void ShuffleItemsCommercials()
    {
        TakeConcretItems(buyItems, buyItemsData);
        TakeConcretItems(sellItems, sellItemsData);
        currentMoney = Random.Range(rangeMoney.x, rangeMoney.y);
    }
    private void TakeConcretItems(List<InventoryResource> receptor, List<CommercialItem> thrower)
    {
        receptor.Clear();
        foreach (var item in thrower)

        {
            if(Random.value <= item.percentToAppear)
            {
                int amount = Random.Range(item.amountRange.x, item.amountRange.y);
                int price = TakePriceByInterest(item.interest, item.item.baseCost);
                InventoryResource resource = new InventoryResource(item.item, amount,price);
                receptor.Add(resource);
            }
        }
    }

    private int TakePriceByInterest(Interest interest, int baseCost)
    {
        int price = 0;
        float midderPercent = 0;
        switch (interest)
        {
            case Interest.little:
                midderPercent = .35f;
                break;
            case Interest.bit:
                midderPercent = .65f;
                break;
            case Interest.intrigued:
                midderPercent = 1;
                break;
            case Interest.interest:
                midderPercent = 1.3f;
                break;
            case Interest.lot:
                midderPercent = 1.65f;
                break;
            case Interest.tooMuch:
                midderPercent = 2f;
                break;
            case Interest.essential:
                midderPercent = 2.5f;
                break;
            default:
                break;
        }
        price = Mathf.RoundToInt(baseCost * Random.Range(midderPercent - errorRange, midderPercent+errorRange));
        if (price <= 1)
            price = 1;
        return price;
    }
}
