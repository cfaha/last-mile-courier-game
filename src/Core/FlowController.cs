using UnityEngine;

public class FlowController : MonoBehaviour
{
    public OrderSystem OrderSystem;
    public RouteSystem RouteSystem;
    public EventSystem EventSystem;
    public ScoringSystem ScoringSystem;
    public UIController UIController;
    public TimerSystem TimerSystem;
    public DeliveryProcessor DeliveryProcessor;
    public DeliverySequence DeliverySequence;

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
            EventSystem.OnEvent += (title, desc, timePenalty, satisfactionPenalty) =>
            {
                UIController.DeliveryUI.ShowEvent(title, desc);
                DeliveryProcessor?.ApplyEventPenalty(timePenalty, satisfactionPenalty);
            };
        }
    }

    public void StartPlanning()
    {
        OrderSystem.GenerateOrders(5);
        UIController.ShowRoutePlanning();
        UIController.RoutePlanningUI.BindOrders(OrderSystem.ActiveOrders);
    }

    public void StartDelivery()
    {
        UIController.ShowDelivery();
        DeliveryProcessor?.Init(OrderSystem.ActiveOrders.Count, 500);
        DeliverySequence?.SetSequence(UIController.RoutePlanningUI.DragController.CurrentOrderIds);
        TimerSystem?.StartTimer(300);
    }

    public void FinishDelivery()
    {
        if (DeliveryProcessor != null)
        {
            ScoringSystem.SyncFromState(DeliveryProcessor.State);
        }
        float score = ScoringSystem.CalculateScore();
        int coins = RewardCalculator.CalculateCoins(score, DeliveryProcessor?.State.BaseRewardSum ?? 500);
        UIController.ShowResult();
        UIController.ResultUI.ShowResult(score, coins);
    }
}
