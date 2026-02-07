using UnityEngine;

public class FlowController : MonoBehaviour
{
    public OrderSystem OrderSystem;
    public RouteSystem RouteSystem;
    public EventSystem EventSystem;
    public ScoringSystem ScoringSystem;
    public UIController UIController;

    private void Start()
    {
        StartPlanning();
    }

    public void StartPlanning()
    {
        OrderSystem.GenerateOrders(5);
        UIController.ShowRoutePlanning();
    }

    public void StartDelivery()
    {
        UIController.ShowDelivery();
        // TODO: start timer + events
    }

    public void FinishDelivery()
    {
        float score = ScoringSystem.CalculateScore();
        UIController.ShowResult();
        UIController.ResultUI.ShowResult(score);
    }
}
