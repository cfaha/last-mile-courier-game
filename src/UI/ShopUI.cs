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
        foreach (Transform child in ListRoot)
        {
            Object.Destroy(child.gameObject);
        }
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

    public ToastUI ToastUI;

    public void OnBuy(ShopItem item)
    {
        bool ok = ShopSystem != null && ShopSystem.Buy(item, CurrencySystem);
        ToastUI?.Show(ok ? "购买成功" : "余额不足");
        LoadAndBuild();
    }
}
