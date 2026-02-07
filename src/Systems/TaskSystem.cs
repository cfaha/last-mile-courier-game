using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    public int DailyDeliveriesTarget = 5;
    public int DailyCompleted;
    private int _dayStamp;

    public void OnDeliveryComplete()
    {
        DailyCompleted++;
    }

    public bool IsDailyDone() => DailyCompleted >= DailyDeliveriesTarget;

    public void ResetDailyIfNeeded(int dayStamp)
    {
        if (_dayStamp != dayStamp)
        {
            _dayStamp = dayStamp;
            DailyCompleted = 0;
        }
    }
}
