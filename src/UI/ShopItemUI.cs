using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Text NameText;
    public Text PriceText;
    private ShopItem _item;
    public UnityEngine.UI.Button BuyButton;

    public void Bind(ShopItem item, bool owned)
    {
        _item = item;
        if (NameText != null) NameText.text = item.Name;
        if (PriceText != null) PriceText.text = owned ? "已拥有" : item.Price.ToString();
        if (BuyButton != null) BuyButton.interactable = !owned;
    }

    public ShopItem GetItem() => _item;
}
