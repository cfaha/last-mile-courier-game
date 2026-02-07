using UnityEngine;

public class OrderTypeColor
{
    public static Color Get(OrderType type)
    {
        switch (type)
        {
            case OrderType.Fresh: return new Color(0.2f, 0.8f, 0.3f);
            case OrderType.Insured: return new Color(0.2f, 0.6f, 0.9f);
            case OrderType.Large: return new Color(0.9f, 0.6f, 0.2f);
            case OrderType.Night: return new Color(0.6f, 0.2f, 0.8f);
            default: return new Color(0.8f, 0.8f, 0.8f);
        }
    }
}
