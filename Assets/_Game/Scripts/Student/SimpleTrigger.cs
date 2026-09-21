using UnityEngine;

// SimpleTrigger：玩家进入触发器 → 显示一个物体
// 教学概念：Collider2D + Is Trigger + Tag + if + OnTriggerEnter2D
public class SimpleTrigger : MonoBehaviour
{
    // 要显示的物体（在 Inspector 里拖进来）
    public GameObject objectToActivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 只有 Tag 是 Player 的物体进入时才会触发
        if (other.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
        }
    }
}
