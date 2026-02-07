using UnityEngine;

public class ConfigLoader
{
    public static EventWeightsConfig LoadEventWeights(TextAsset json)
    {
        return JsonUtility.FromJson<EventWeightsConfig>(json.text);
    }

    public static OrderWeightsConfig LoadOrderWeights(TextAsset json)
    {
        return JsonUtility.FromJson<OrderWeightsConfig>(json.text);
    }
}
