using UnityEngine;

public class DeliveryUI : MonoBehaviour
{
    public void UpdateTimer(int secondsLeft)
    {
        // TODO: update timer text
    }

    public void ShowEvent(string title, string desc)
    {
        // TODO: show event popup
        Debug.Log($"Event: {title} - {desc}");
    }

    public void OnClickDeliverNext(DeliverySimulator simulator)
    {
        simulator?.DeliverNext();
    }
}
