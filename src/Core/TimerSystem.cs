using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    public int TotalSeconds = 300;
    public int RemainingSeconds;
    public bool Running;
    public System.Action<int> OnTick;
    public System.Action OnFinished;

    private float _accum;

    public void StartTimer(int seconds)
    {
        TotalSeconds = seconds;
        RemainingSeconds = seconds;
        Running = true;
        _accum = 0f;
    }

    private void Update()
    {
        if (!Running) return;
        _accum += Time.deltaTime;
        if (_accum >= 1f)
        {
            _accum -= 1f;
            RemainingSeconds = Mathf.Max(0, RemainingSeconds - 1);
            OnTick?.Invoke(RemainingSeconds);
            if (RemainingSeconds <= 0)
            {
                Running = false;
                OnFinished?.Invoke();
            }
        }
    }
}
