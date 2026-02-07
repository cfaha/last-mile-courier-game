using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public TutorialUI UI;

    public void ShowForLevel(int level)
    {
        if (UI == null) return;
        if (level == 1) UI.Show("拖拽排序订单，然后点击开始配送");
        else if (level == 2) UI.Show("遇到门禁可选择等待或绕行");
        else if (level == 3) UI.Show("暴雨会降低配送速度");
        else UI.Hide();
    }
}
