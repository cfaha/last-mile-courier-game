using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Text NameText;
    public Text PriceText;
    private ShopItem _item;

    public void Bind(ShopItem item)
    {
        _item = item;
        if (NameText != null) NameText.text = item.Name;
        if (PriceText != null) PriceText.text = item.Price.ToString();
    }

    public ShopItem GetItem() => _item;
}
