using UnityEngine;

// SetSprite：把 SpriteRenderer 换成指定 Sprite
public class SetSprite : MonoBehaviour
{
    [Header("Target")]
    public SpriteRenderer targetRenderer; // 留空 = 当前物体
    public Sprite newSprite;

    // 由 UnityEvent 调用
    public void Apply()
    {
        if (targetRenderer == null) targetRenderer = GetComponent<SpriteRenderer>();
        if (targetRenderer != null && newSprite != null) targetRenderer.sprite = newSprite;
    }
}
