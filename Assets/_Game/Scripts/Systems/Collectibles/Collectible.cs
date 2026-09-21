using UnityEngine;
using UnityEngine.Events;

// Collectible：可收集物。Touch 模式玩家碰到自动收集；Interact 模式由其他系统调用 Collect()
public class Collectible : MonoBehaviour
{
    public enum CollectMode
    {
        Touch,    // 玩家碰到自动收集
        Interact  // 需要交互（例如对话按钮调用 Collect()）
    }

    [Header("Info")]
    public string itemName = "Collectible";

    [Header("Collect Mode")]
    public CollectMode collectMode = CollectMode.Touch;

    [Header("Events")]
    public UnityEvent onCollect; // 收集时触发（可改变量 / 显示消息 / 启用或禁用对象）

    [Header("After Collect")]
    [Tooltip("收集后隐藏自己")]
    public bool hideAfterCollect = true;

    private bool m_isCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collectMode != CollectMode.Touch || m_isCollected) return;
        if (!other.CompareTag("Player")) return;

        Collect();
    }

    // 开始收集（Touch 模式自动调用；Interact 模式由事件调用）
    public void Collect()
    {
        if (m_isCollected) return;

        m_isCollected = true;
        onCollect?.Invoke();

        if (hideAfterCollect)
        {
            gameObject.SetActive(false);
        }
    }
}
