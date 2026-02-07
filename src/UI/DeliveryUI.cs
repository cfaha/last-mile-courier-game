using UnityEngine;

public class DeliveryUI : MonoBehaviour
{
    public UnityEngine.UI.Text TimerText;
    public UnityEngine.UI.Text RemainingText;

    public void UpdateTimer(int secondsLeft)
    {
        if (TimerText != null) TimerText.text = $"{secondsLeft}s";
    }

    public void UpdateRemaining(int remaining)
    {
        if (RemainingText != null) RemainingText.text = $"剩余 {remaining} 单";
    }

    public void ShowEvent(string title, string desc)
    {
        // TODO: show event popup
        Debug.Log($"Event: {title} - {desc}");
    }

    public FlowController FlowController;

    public void OnClickDeliverNext(DeliverySimulator simulator, DeliverySequence sequence)
    {
        simulator?.DeliverNext();
        if (sequence != null) UpdateRemaining(sequence.Remaining);
    }

    public void OnClickFinish()
    {
        FlowController?.FinishDelivery();
    }
}
