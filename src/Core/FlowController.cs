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
    public LevelInfoUI LevelInfoUI;
    public CompletionUI CompletionUI;
    public CurrencySystem CurrencySystem;
    public CurrencyUI CurrencyUI;
    public TaskSystem TaskSystem;
    public TaskUI TaskUI;

    public TextAsset LevelConfigJson;
    public int CurrentLevelId = 1;
    public int MaxLevelId = 20;
    private LevelConfig _currentLevel;

    private void Start()
    {
        CurrentLevelId = SaveManager.LoadLevel(1);
        StartPlanning();
        HookEvents();
        if (CurrencySystem != null && CurrencyUI != null)
        {
            CurrencySystem.OnChanged += (coins) =>
            {
                CurrencyUI.Bind(coins);
                SaveManager.SaveCoins(coins);
            };
            CurrencySystem.AddCoins(SaveManager.LoadCoins(0));
        }
        if (TaskSystem != null && TaskUI != null)
        {
            TaskSystem.ResetDailyIfNeeded(DayStamp.Today());
            TaskUI.Bind(TaskSystem.DailyCompleted, TaskSystem.DailyDeliveriesTarget);
        }
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
        _currentLevel = LevelConfigJson != null ? LevelConfigLoader.GetLevel(LevelConfigJson, CurrentLevelId) : null;
        int orders = _currentLevel != null ? _currentLevel.orders : 5;
        int timeLimit = _currentLevel != null ? _currentLevel.time : 300;
        float eventChance = _currentLevel != null ? _currentLevel.eventChance : 0.2f;

        if (EventSystem != null)
        {
            EventSystem.EventChancePerMinute = eventChance;
            if (_currentLevel != null) EventSystem.Zone = _currentLevel.zone;
        }
        OrderSystem.GenerateOrders(orders, timeLimit);
        UIController.ShowRoutePlanning();
        UIController.RoutePlanningUI.BindOrders(OrderSystem.ActiveOrders, OrderSystem.RuntimeOrders);
        if (LevelInfoUI != null)
        {
            LevelInfoUI.Bind(CurrentLevelId, _currentLevel != null ? _currentLevel.zone : ZoneType.Residential);
        }
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

        if (_currentLevel != null && !string.IsNullOrEmpty(_currentLevel.forcedEvent))
        {
            TriggerForcedEvent(_currentLevel.forcedEvent);
        }
    }

    public void FinishDelivery()
    {
        if (DeliveryProcessor != null)
        {
            ScoringSystem.SyncFromState(DeliveryProcessor.State);
        }
        float score = ScoringSystem.CalculateScore();
        int coins = RewardCalculator.CalculateCoins(score, DeliveryProcessor?.State.BaseRewardSum ?? 500);
        CurrencySystem?.AddCoins(coins);
        if (TaskSystem != null && TaskSystem.IsDailyDone())
        {
            CurrencySystem?.AddCoins(200);
        }
        UIController.ShowResult();
        UIController.ResultUI.ShowResult(score, coins);
    }
}
