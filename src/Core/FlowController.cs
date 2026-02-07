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
    public TaskRewardUI TaskRewardUI;
    public ToastUI ToastUI;
    public TutorialScript TutorialScript;
    public MainMenuUI MainMenuUI;
    public LevelResultPanel LevelResultPanel;
    public FailureUI FailureUI;
    public TimeStats TimeStats;
    public StatsReporter StatsReporter;
    public NewbieFlow NewbieFlow;

    public TextAsset LevelConfigJson;
    public TextAsset EventWeightsJson;
    public TextAsset OrderWeightsJson;
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
                MainMenuUI?.BindCoins(coins);
                SaveManager.SaveCoins(coins);
            };
            CurrencySystem.SetCoins(SaveManager.LoadCoins(0));
        }
        if (TaskSystem != null && TaskUI != null)
        {
            SaveManager.LoadTaskProgress(out var completed, out var dayStamp, out var claimed);
            TaskSystem.Load(completed, dayStamp, claimed);
            TaskSystem.ResetDailyIfNeeded(DayStamp.Today());
            TaskUI.Bind(TaskSystem.DailyCompleted, TaskSystem.DailyDeliveriesTarget);
            MainMenuUI?.BindTask(TaskSystem.DailyCompleted, TaskSystem.DailyDeliveriesTarget);
        }
        NewbieFlow?.StartFlow(CurrentLevelId);
        MainMenuUI?.BindProgress(CurrentLevelId);
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
            if (EventWeightsJson != null)
            {
                EventSystem.ApplyWeights(ConfigLoader.LoadEventWeights(EventWeightsJson));
            }
            EventSystem.EventChancePerMinute = eventChance;
            if (_currentLevel != null) EventSystem.Zone = _currentLevel.zone;
        }
        if (OrderSystem != null && OrderWeightsJson != null)
        {
            OrderSystem.Weights = ConfigLoader.LoadOrderWeights(OrderWeightsJson);
        }
        OrderSystem.GenerateOrders(orders, timeLimit);
        UIController.ShowRoutePlanning();
        FindObjectOfType<UIStateMachine>()?.ShowPlanning();
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
        FindObjectOfType<UIStateMachine>()?.ShowDelivery();
        DeliverySequence?.SetSequence(UIController.RoutePlanningUI.DragController.CurrentOrderIds);
        if (DeliverySimulator != null)
        {
            DeliverySimulator.Orders = OrderSystem.RuntimeOrders;
            DeliverySimulator.ResetNodeColors();
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
        if (TaskSystem != null && TaskSystem.CanClaimReward())
        {
            CurrencySystem?.AddCoins(200);
            TaskSystem.MarkRewardClaimed();
            ToastUI?.Show("每日任务完成 +200");
        }
        bool failed = DeliveryProcessor != null && DeliveryProcessor.IsFailed();
        UIController.ShowResult();
        FindObjectOfType<UIStateMachine>()?.ShowResult();
        UIController.ResultUI.ShowResult(score, coins, failed);
        StatsReporter?.ReportLevel(CurrentLevelId, score, coins, failed);
        if (DeliveryProcessor != null && LevelResultPanel != null)
        {
            int totalCoins = CurrencySystem != null ? CurrencySystem.Coins : -1;
            LevelResultPanel.Bind(DeliveryProcessor.State.DeliveredOrders, DeliveryProcessor.State.TotalOrders, score, CurrentLevelId, totalCoins);
            LevelResultPanel.BindDetail(ScoringSystem.OnTimeRate, ScoringSystem.RouteEfficiency);
        }
        if (TimeStats != null && TimerSystem != null)
        {
            TimeStats.SetUsed(TimerSystem.TotalSeconds - TimerSystem.RemainingSeconds);
            LevelResultPanel?.BindTime(TimeStats.UsedSeconds);
        }
        if (FailureUI != null && DeliveryProcessor != null && DeliveryProcessor.IsFailed())
        {
            FailureUI.Show("未完成所有订单");
        }
    }
}
