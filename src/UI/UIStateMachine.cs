using UnityEngine;

public class UIStateMachine : MonoBehaviour
{
    public UIController UIController;

    public void ShowPlanning() => UIController?.ShowRoutePlanning();
    public void ShowDelivery() => UIController?.ShowDelivery();
    public void ShowResult() => UIController?.ShowResult();
}
