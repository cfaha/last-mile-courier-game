public class CurrencySystem
{
    public int Coins { get; private set; }
    public System.Action<int> OnChanged;

    public void AddCoins(int amount)
    {
        Coins += amount;
        OnChanged?.Invoke(Coins);
    }
}
