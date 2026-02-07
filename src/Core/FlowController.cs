using UnityEngine;

public partial class FlowController : MonoBehaviour
{
    public OrderSystem OrderSystem;
    public RouteSystem RouteSystem;
    public EventSystem EventSystem;
    public ScoringSystem ScoringSystem;
    public UIController UIController;
    public TimerSystem TimerSystem;
    public DeliveryProcessor DeliveryProcessor;
    public DeliverySequence DeliverySequence;
    public DeliverySimulator DeliverySimulator;
    public MapPlaceholder MapPlaceholder;

    public TextAsset LevelConfigJson;
    public int CurrentLevelId = 1;

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
                UIController.DeliveryUI.ShowEvent(
                    title,
                    desc,
                    () => ApplyEvent(timePenalty, satisfactionPenalty),
                    () => ApplyEvent(timePenalty * 0.5f, satisfactionPenalty * 0.5f)
                );
            };
        }
        if (DeliverySimulator != null)
        {
            DeliverySimulator.OnAllDelivered += FinishDelivery;
            DeliverySimulator.OnDelivered += (_) =>
            {
                if (DeliverySequence != null)
                {
                    UIController.DeliveryUI.UpdateRemaining(DeliverySequence.Remaining);
                }
            };
        }
    }

    public void StartPlanning()
    {
        var level = LevelConfigJson != null ? LevelConfigLoader.GetLevel(LevelConfigJson, CurrentLevelId) : null;
        int orders = level != null ? level.orders : 5;
        int timeLimit = level != null ? level.time : 300;
        float eventChance = level != null ? level.eventChance : 0.2f;

        if (EventSystem != null) EventSystem.EventChancePerMinute = eventChance;
        OrderSystem.GenerateOrders(orders, timeLimit);
        UIController.ShowRoutePlanning();
        UIController.RoutePlanningUI.BindOrders(OrderSystem.ActiveOrders, OrderSystem.RuntimeOrders);
    }

    public void StartDelivery()
    {
        UIController.ShowDelivery();
        DeliveryProcessor?.Init(OrderSystem.ActiveOrders.Count, 500);
        DeliverySequence?.SetSequence(UIController.RoutePlanningUI.DragController.CurrentOrderIds);
        if (DeliverySimulator != null)
        {
            DeliverySimulator.Orders = OrderSystem.RuntimeOrders;
        }
        if (MapPlaceholder != null && DeliverySequence != null)
        {
            MapPlaceholder.DrawRoute(DeliverySequence.OrderIds.ToArray());
        }
        UIController.DeliveryUI.UpdateRemaining(DeliverySequence != null ? DeliverySequence.Remaining : 0);
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
