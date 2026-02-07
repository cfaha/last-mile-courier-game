using System;

[Serializable]
public class EventWeightsConfig
{
    public ZoneEventWeights residential;
    public ZoneEventWeights commercial;
    public ZoneEventWeights industrial;
}

[Serializable]
public class OrderWeightsConfig
{
    public float normal;
    public float fresh;
    public float insured;
    public float large;
    public float night;
}
