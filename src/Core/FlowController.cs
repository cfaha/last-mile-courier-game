using UnityEngine;

public class FlowController : MonoBehaviour
{
    public OrderSystem OrderSystem;
    public RouteSystem RouteSystem;
    public EventSystem EventSystem;
    public ScoringSystem ScoringSystem;
    public UIController UIController;
    public TimerSystem TimerSystem;

    private void Start()
    {
        StartPlanning();
        HookEvents();
    }

    private void HookEvents()
    {
        if (TimerSystem != null)
        {
            TimerSystem.OnTick += (s) => UIController.DeliveryUI.UpdateTimer(s);
            TimerSystem.OnFinished += FinishDelivery;
        }
        if (EventSystem != null)
        {
            EventSystem.OnEvent += (title, desc) => UIController.DeliveryUI.ShowEvent(title, desc);
        }
    }

    public void StartPlanning()
    {
        OrderSystem.GenerateOrders(5);
        UIController.ShowRoutePlanning();
    }

    public void StartDelivery()
    {
        UIController.ShowDelivery();
        TimerSystem?.StartTimer(300);
    }

    public void FinishDelivery()
    {
        float score = ScoringSystem.CalculateScore();
        UIController.ShowResult();
        UIController.ResultUI.ShowResult(score);
    }
}
