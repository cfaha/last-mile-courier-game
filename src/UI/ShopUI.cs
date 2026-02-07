using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public ShopSystem ShopSystem;
    public CurrencySystem CurrencySystem;
    public Transform ListRoot;
    public ShopItemUI ItemPrefab;

    public void Build(ShopItem[] items)
    {
        foreach (var item in items)
        {
            var ui = Object.Instantiate(ItemPrefab, ListRoot);
            ui.Bind(item);
        }
    }

    public void OnBuy(ShopItem item)
    {
        ShopSystem?.Buy(item, CurrencySystem);
    }
}
