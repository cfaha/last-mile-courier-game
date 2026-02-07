using UnityEngine;

public class MapRouteAnimator : MonoBehaviour
{
    public LineRenderer Line;
    public float Speed = 1f;
    private float _t;

    public void Play()
    {
        _t = 0f;
        if (Line != null) Line.startColor = Line.endColor = Color.white;
    }

    private void Update()
    {
        if (Line == null || Line.positionCount <= 1) return;
        _t += Time.deltaTime * Speed;
        Line.widthMultiplier = 0.05f + 0.02f * Mathf.Sin(_t * 3f);
    }
}
