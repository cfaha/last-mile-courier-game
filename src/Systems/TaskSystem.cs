using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    public int DailyDeliveriesTarget = 5;
    public int DailyCompleted;
    private int _dayStamp;
    private bool _rewardClaimed;

    public void OnDeliveryComplete()
    {
        DailyCompleted++;
    }

    public bool IsDailyDone() => DailyCompleted >= DailyDeliveriesTarget;

    public bool CanClaimReward()
    {
        return IsDailyDone() && !_rewardClaimed;
    }

    public void MarkRewardClaimed()
    {
        _rewardClaimed = true;
    }

    public void ResetDailyIfNeeded(int dayStamp)
    {
        if (_dayStamp != dayStamp)
        {
            _dayStamp = dayStamp;
            DailyCompleted = 0;
            _rewardClaimed = false;
        }
    }
}
