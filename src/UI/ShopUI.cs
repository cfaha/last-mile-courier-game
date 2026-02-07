using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public ShopSystem ShopSystem;
    public CurrencySystem CurrencySystem;

    public void OnBuy(ShopItem item)
    {
        ShopSystem?.Buy(item, CurrencySystem);
    }
}
