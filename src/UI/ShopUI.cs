using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public ShopSystem ShopSystem;
    public CurrencySystem CurrencySystem;
    public Transform ListRoot;
    public ShopItemUI ItemPrefab;
    public TextAsset ShopJson;

    public void Build(ShopItem[] items)
    {
        foreach (var item in items)
        {
            var ui = Object.Instantiate(ItemPrefab, ListRoot);
            ui.Bind(item);
        }
    }

    public void LoadAndBuild()
    {
        if (ShopJson == null) return;
        var items = ShopConfig.Load(ShopJson);
        Build(items);
    }

    public void OnBuy(ShopItem item)
    {
        ShopSystem?.Buy(item, CurrencySystem);
    }
}
