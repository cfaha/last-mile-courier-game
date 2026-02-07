using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public FlowController FlowController;

    public void OnStart()
    {
        FlowController?.StartPlanning();
    }
}
