using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    public int DailyDeliveriesTarget = 5;
    public int DailyCompleted;

    public void OnDeliveryComplete()
    {
        DailyCompleted++;
    }

    public bool IsDailyDone() => DailyCompleted >= DailyDeliveriesTarget;
}
