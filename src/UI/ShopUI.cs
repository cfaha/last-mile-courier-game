using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public ShopSystem ShopSystem;
    public CurrencySystem CurrencySystem;
    public Transform ListRoot;
    public ShopItemUI ItemPrefab;
    public TextAsset ShopJson;
    public ToastUI ToastUI;
    public OwnedItems OwnedItems = new OwnedItems();

    private void Start()
    {
        OwnedItems.Load(SaveManager.LoadOwned());
        LoadAndBuild();
    }

    public void Build(ShopItem[] items)
    {
        foreach (Transform child in ListRoot)
        {
            Object.Destroy(child.gameObject);
        }
        foreach (var item in items)
        {
            var ui = Object.Instantiate(ItemPrefab, ListRoot);
            bool owned = OwnedItems.IsOwned(item.Id);
            ui.Bind(item, owned);
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
        if (OwnedItems.IsOwned(item.Id)) return;
        bool ok = ShopSystem != null && ShopSystem.Buy(item, CurrencySystem);
        if (ok) {
            OwnedItems.Add(item.Id);
            SaveManager.SaveOwned(OwnedItems.ToCsv());
        }
        ToastUI?.Show(ok ? "购买成功" : "余额不足");
        LoadAndBuild();
    }
}
