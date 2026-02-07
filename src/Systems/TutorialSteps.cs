using UnityEngine;

public class TutorialSteps : MonoBehaviour
{
    public TutorialOverlay Overlay;
    private int _step;

    public void StartSteps()
    {
        _step = 0;
        ShowStep();
    }

    public void NextStep()
    {
        _step++;
        ShowStep();
    }

    public void OnStartDeliveryClicked()
    {
        if (_step == 1) NextStep();
    }

    public void OnDeliverNextClicked()
    {
        if (_step == 2) NextStep();
    }

    private void ShowStep()
    {
        if (Overlay == null) return;
        if (_step == 0) Overlay.Show("拖拽排序订单");
        else if (_step == 1) Overlay.Show("点击开始配送");
        else if (_step == 2) Overlay.Show("点击送下一单");
        else Overlay.Hide();
    }
}
