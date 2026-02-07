using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public Text CoinText;

    public void Bind(int coins)
    {
        if (CoinText != null) CoinText.text = coins.ToString();
    }
}
