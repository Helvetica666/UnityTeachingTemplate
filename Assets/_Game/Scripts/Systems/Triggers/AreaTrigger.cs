using UnityEngine;
using UnityEngine.Events;

// AreaTrigger：玩家（或指定标签对象）进入 / 离开区域时触发事件
public class AreaTrigger : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("只触发一次进入事件")]
    public bool triggerOnce = true;

    [Tooltip("只响应带此标签的对象；留空则响应所有对象")]
    public string requiredTag = "Player";

    [Header("Events")]
    public UnityEvent onEnter; // 进入区域时触发
    public UnityEvent onExit;  // 离开区域时触发

    [SerializeField]
    public bool lockOnEnter = true;

    private bool m_hasEntered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!MatchesTag(other)) return;
        if (triggerOnce && m_hasEntered) return;

        // 默认进入即锁定；组合条件场景下关闭 lockOnEnter，改由 MarkTriggered() 决定锁定时机
        if (triggerOnce && lockOnEnter)
        {
            m_hasEntered = true;
        }

        onEnter?.Invoke();
    }

    // 手动标记该区域已触发一次（由事件链调用，例如条件满足并完成动作之后）
    public void MarkTriggered()
    {
        m_hasEntered = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!MatchesTag(other)) return;

        onExit?.Invoke();
    }

    private bool MatchesTag(Collider2D other)
    {
        if (string.IsNullOrEmpty(requiredTag)) return true;
        return other.CompareTag(requiredTag);
    }
}
