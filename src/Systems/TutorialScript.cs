using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public TutorialUI UI;
    public TutorialOverlay Overlay;

    public void ShowForLevel(int level)
    {
        if (level == 1)
        {
            UI?.Show("拖拽排序订单，然后点击开始配送");
            Overlay?.Show("拖拽排序订单，然后点击开始配送");
        }
        else if (level == 2)
        {
            UI?.Show("遇到门禁可选择等待或绕行");
            Overlay?.Show("遇到门禁可选择等待或绕行");
        }
        else if (level == 3)
        {
            UI?.Show("暴雨会降低配送速度");
            Overlay?.Show("暴雨会降低配送速度");
        }
        else
        {
            UI?.Hide();
            Overlay?.Hide();
        }
    }

    public void ShowForForcedEvent(string type)
    {
        if (type == "Gate")
        {
            UI?.Show("门禁：等待或绕行");
            Overlay?.Show("门禁：等待或绕行");
        }
        else if (type == "Rain")
        {
            UI?.Show("暴雨：速度下降");
            Overlay?.Show("暴雨：速度下降");
        }
    }
}
