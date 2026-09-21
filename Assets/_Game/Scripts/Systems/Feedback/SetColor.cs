using UnityEngine;

// SetColor：把 SpriteRenderer 改成指定颜色
public class SetColor : MonoBehaviour
{
    [Header("Target")]
    public SpriteRenderer targetRenderer; // 留空 = 当前物体
    public Color targetColor = Color.green;

    // 由 UnityEvent 调用
    public void Apply()
    {
        if (targetRenderer == null) targetRenderer = GetComponent<SpriteRenderer>();
        if (targetRenderer != null) targetRenderer.color = targetColor;
    }
}
