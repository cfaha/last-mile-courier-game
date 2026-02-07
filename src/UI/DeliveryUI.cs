using UnityEngine;

public class DeliveryUI : MonoBehaviour
{
    public UnityEngine.UI.Text TimerText;
    public UnityEngine.UI.Text RemainingText;
    public EventPopupUI EventPopup;

    public void UpdateTimer(int secondsLeft)
    {
        if (TimerText != null) TimerText.text = $"{secondsLeft}s";
    }

    public void UpdateRemaining(int remaining)
    {
        if (RemainingText != null) RemainingText.text = $"剩余 {remaining} 单";
    }

    public void ShowEvent(string title, string desc, System.Action onWait, System.Action onDetour)
    {
        if (EventPopup != null)
        {
            EventPopup.Show(title, desc, onWait, onDetour);
        }
        Debug.Log($"Event: {title} - {desc}");
    }

    public FlowController FlowController;
    public TutorialSteps TutorialSteps;

    public void OnClickDeliverNext(DeliverySimulator simulator, DeliverySequence sequence)
    {
        TutorialSteps?.OnDeliverNextClicked();
        simulator?.DeliverNext();
        if (sequence != null) UpdateRemaining(sequence.Remaining);
    }

    public void OnClickFinish()
    {
        FlowController?.FinishDelivery();
    }
}
