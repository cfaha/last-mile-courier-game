using UnityEngine;

public class UIController : MonoBehaviour
{
    public RoutePlanningUI RoutePlanningUI;
    public DeliveryUI DeliveryUI;
    public ResultUI ResultUI;

    public void ShowRoutePlanning() => SetActive(RoutePlanningUI.gameObject);
    public void ShowDelivery() => SetActive(DeliveryUI.gameObject);
    public void ShowResult() => SetActive(ResultUI.gameObject);

    private void SetActive(GameObject go)
    {
        RoutePlanningUI.gameObject.SetActive(false);
        DeliveryUI.gameObject.SetActive(false);
        ResultUI.gameObject.SetActive(false);
        go.SetActive(true);
    }
}
