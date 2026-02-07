using UnityEngine;

public class EventSystem : MonoBehaviour
{
    [Range(0f, 1f)] public float EventChancePerMinute = 0.2f;
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
            OnEvent?.Invoke("门禁", "进入小区需要等待 20 秒", 0.1f, 0.05f);
        }
    }
}
