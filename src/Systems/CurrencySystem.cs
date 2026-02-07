public class CurrencySystem
{
    public int Coins { get; private set; }

    public void AddCoins(int amount)
    {
        Coins += amount;
    }
}
