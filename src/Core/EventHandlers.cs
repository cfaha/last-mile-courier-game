using UnityEngine;

public partial class FlowController
{
    private void ApplyEvent(float timePenalty, float satisfactionPenalty)
    {
        DeliveryProcessor?.ApplyEventPenalty(timePenalty, satisfactionPenalty);
        if (DeliverySimulator != null && DeliveryProcessor != null)
        {
            DeliverySimulator.SpeedMultiplier = DeliveryProcessor.SpeedMultiplier;
        }
    }

    private void TriggerForcedEvent(string forced)
    {
        switch (forced)
        {
            case "Gate":
                TutorialScript?.ShowForForcedEvent("Gate");
                UIController?.DeliveryUI.ShowEvent(
                    "门禁",
                    "进入小区需要等待 20 秒",
                    () => ApplyEvent(0.1f, 0.05f),
                    () => ApplyEvent(0.05f, 0.02f)
                );
                break;
            case "Rain":
                TutorialScript?.ShowForForcedEvent("Rain");
                UIController?.DeliveryUI.ShowEvent(
                    "暴雨",
                    "雨天路滑，配送速度下降",
                    () => ApplyEvent(0.15f, 0.02f),
                    () => ApplyEvent(0.08f, 0.01f)
                );
                break;
        }
    }

    public void NextLevel()
    {
        if (CurrentLevelId >= MaxLevelId)
        {
            Debug.Log("All levels completed");
            if (CompletionUI != null) CompletionUI.Show();
            return;
        }
        CurrentLevelId += 1;
        SaveManager.SaveLevel(CurrentLevelId);
        StartPlanning();
    }
}
