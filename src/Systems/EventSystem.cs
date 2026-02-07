using UnityEngine;

public class EventSystem : MonoBehaviour
{
    [Range(0f, 1f)] public float EventChancePerMinute = 0.2f;
    public ZoneType Zone = ZoneType.Residential;
    public ZoneEventWeights Residential = new ZoneEventWeights { Gate = 0.6f, Rain = 0.25f, Traffic = 0.15f };
    public ZoneEventWeights Commercial = new ZoneEventWeights { Gate = 0.4f, Rain = 0.3f, Traffic = 0.3f };
    public ZoneEventWeights Industrial = new ZoneEventWeights { Gate = 0.2f, Rain = 0.4f, Traffic = 0.4f };

    public System.Action<string, string, float, float> OnEvent;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 60f)
        {
            _timer = 0f;
            TryTriggerEvent();
        }
    }

    private void TryTriggerEvent()
    {
        if (Random.value <= EventChancePerMinute)
        {
            var weights = GetWeights();
            float r = Random.value;
            if (r < weights.Gate)
                OnEvent?.Invoke("门禁", "进入小区需要等待 20 秒", 0.1f, 0.05f);
            else if (r < weights.Gate + weights.Rain)
                OnEvent?.Invoke("暴雨", "雨天路滑，配送速度下降", 0.15f, 0.02f);
            else
                OnEvent?.Invoke("管制", "临时交通管制，需要绕行", 0.2f, 0.04f);
        }
    }

    private ZoneEventWeights GetWeights()
    {
        switch (Zone)
        {
            case ZoneType.Commercial: return Commercial;
            case ZoneType.Industrial: return Industrial;
            default: return Residential;
        }
    }
}
