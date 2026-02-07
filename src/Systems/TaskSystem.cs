using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    public int DailyDeliveriesTarget = 5;
    public int DailyCompleted;
    private int _dayStamp;
    private bool _rewardClaimed;

    public void Load(int completed, int dayStamp, bool claimed)
    {
        DailyCompleted = completed;
        _dayStamp = dayStamp;
        _rewardClaimed = claimed;
    }

    public void Save()
    {
        SaveManager.SaveTaskProgress(DailyCompleted, _dayStamp, _rewardClaimed);
    }

    public void OnDeliveryComplete()
    {
        DailyCompleted++;
        Save();
    }

    public bool IsDailyDone() => DailyCompleted >= DailyDeliveriesTarget;

    public bool CanClaimReward()
    {
        return IsDailyDone() && !_rewardClaimed;
    }

    public void MarkRewardClaimed()
    {
        _rewardClaimed = true;
        Save();
    }

    public void ResetDailyIfNeeded(int dayStamp)
    {
        if (_dayStamp != dayStamp)
        {
            _dayStamp = dayStamp;
            DailyCompleted = 0;
            _rewardClaimed = false;
            Save();
        }
    }
}
