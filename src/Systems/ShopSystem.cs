public class ShopSystem
{
    public bool Buy(ShopItem item, CurrencySystem currency)
    {
        if (currency == null || item == null) return false;
        if (currency.Coins < item.Price) return false;
        currency.AddCoins(-item.Price);
        return true;
    }
}
